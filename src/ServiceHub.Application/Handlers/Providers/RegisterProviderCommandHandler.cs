using MediatR;
using ServiceHub.Application.Commands.Providers;
using ServiceHub.Application.Services.Interfaces;
using ServiceHub.Domain.Entities;
using ServiceHub.Domain.Interfaces.Repositories;
using ServiceHub.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Handlers.Providers;

public class RegisterProviderCommandHandler : IRequestHandler<RegisterProviderCommand, ApiResponse<Guid?>>
{
    private readonly IAuthService _authService;
    private readonly IProviderRepository _providerRepository;
    public RegisterProviderCommandHandler(IAuthService authService, IProviderRepository providerRepository)
    {
        _authService = authService;
        _providerRepository = providerRepository;
    }
    public async Task<ApiResponse<Guid?>> Handle(RegisterProviderCommand request, CancellationToken cancellationToken)
    {
        // criar o usuario de identidade
        var resultRegisterIdentity = await _authService.
            Register(request.Email, request.PhoneNumber, request.Password);

        if (!resultRegisterIdentity.Succeeded) return ApiResponseHelpers.
                BadRequest<Guid?>(string.Join(", ", resultRegisterIdentity.Errors));

        // criar o usuario do dominio
        Provider provider = new Provider(resultRegisterIdentity.ApplicationUser, request.Name, request.Description);
        await _providerRepository.AddAsync(provider, cancellationToken);

        return ApiResponseHelpers.Created<Guid?>(provider.Id);
    }
}
