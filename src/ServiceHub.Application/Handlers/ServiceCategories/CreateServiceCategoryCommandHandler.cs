using MediatR;
using ServiceHub.Application.Commands.ServiceCategory;
using ServiceHub.Domain.Entities;
using ServiceHub.Domain.Interfaces.Repositories;
using ServiceHub.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Handlers.ServiceCategories;

public class CreateServiceCategoryCommandHandler : IRequestHandler<CreateServiceCategoryCommand, ApiResponse<Guid>>
{
    private readonly IServiceCategoryRepository _serviceCategoryRepository;
    public CreateServiceCategoryCommandHandler(IServiceCategoryRepository serviceCategoryRepository)
    {
        _serviceCategoryRepository = serviceCategoryRepository;
    }
    public async Task<ApiResponse<Guid>> Handle(CreateServiceCategoryCommand request, CancellationToken cancellationToken)
    {
        var servicecategory = new ServiceCategory(request.Name, request.Description);
        await _serviceCategoryRepository.AddAsync(servicecategory,cancellationToken);
        
        return ApiResponseHelpers.Created<Guid>(servicecategory.Id);
    }
}
