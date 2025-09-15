using MediatR;
using ServiceHub.Application.Commands.Providers;
using ServiceHub.Application.Services.Interfaces;
using ServiceHub.Domain.Common;
using ServiceHub.Domain.Entities;
using ServiceHub.Domain.Interfaces.Repositories;

namespace ServiceHub.Application.Handlers.Providers;

public class RegisterProviderCommandHandler : IRequestHandler<RegisterProviderCommand, Result<Guid?>>
{
    private readonly IAuthService _authService;
    private readonly IProviderRepository _providerRepository;
    public RegisterProviderCommandHandler(IAuthService authService, IProviderRepository providerRepository)
    {
        _authService = authService;
        _providerRepository = providerRepository;
    }
    public async Task<Result<Guid?>> Handle(RegisterProviderCommand request, CancellationToken cancellationToken)
    {
        // criar o usuario de identidade
        var resultRegisterIdentity = await _authService.
            Register(request.Email, request.PhoneNumber, request.Password);

        if (!resultRegisterIdentity.Succeeded) return Result<Guid?>.Fail(string.Join(", ", resultRegisterIdentity.Errors));

        // criar o usuario do dominio
        Provider provider = new Provider(resultRegisterIdentity.ApplicationUser, request.Name, request.Description);
        await _providerRepository.AddAsync(provider, cancellationToken);

        return Result<Guid?>.Success(provider.Id);
    }
}
