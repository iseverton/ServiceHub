using MediatR;
using Microsoft.Extensions.Logging;
using ServiceHub.Application.Commands.Services;
using ServiceHub.Domain.Entities;
using ServiceHub.Domain.Interfaces.Repositories;
using ServiceHub.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Handlers.Services;

internal class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ApiResponse<Guid?>>
{
    private readonly ILogger<CreateServiceCommandHandler> _logger;
    private readonly IServiceRepository _serviceRepository;
    private readonly IServiceCategoryRepository _serviceCategoryRepository;
    private readonly IProviderRepository _providerRepository;
    public CreateServiceCommandHandler(ILogger<CreateServiceCommandHandler> logger, 
        IServiceRepository serviceRepository, IServiceCategoryRepository serviceCategoryRepository, 
        IProviderRepository repository)
    {
        _logger = logger;
        _serviceRepository = serviceRepository;
        _serviceCategoryRepository = serviceCategoryRepository;
        _providerRepository = repository;

    }

    public async Task<ApiResponse<Guid?>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        // depois adicionar validacoes
        _logger.LogInformation("Creating a new service with title: {@Title}", request.Title);

        var existingProvider = await _providerRepository.GetByIdAsync(request.ProviderId, cancellationToken);
        if (existingProvider == null)
        {
            _logger.LogWarning("Provider not found for the service with title: {@Title}", request.Title);
            return NotFound<Guid?>("Provider not found.");
        }

        var existingcategories = await _serviceCategoryRepository.GetAllById(request.CategoryIds,cancellationToken);
        if (existingcategories.Count() != request.CategoryIds.Count)
        {
            _logger.LogWarning("One or more categories not found for the service with title: {@Title}", request.Title);
            return NotFound<Guid?>("One or more categories not found.");
        }

        var service = new Service(request.Title,request.Description,request.Price,existingcategories,request.ProviderId);

        await _serviceRepository.AddAsync(service,cancellationToken);

        return ApiResponseHelpers.Created<Guid?>(service.Id);
    }
}
