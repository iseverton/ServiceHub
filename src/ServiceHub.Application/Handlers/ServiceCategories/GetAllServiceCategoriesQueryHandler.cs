using MediatR;
using ServiceHub.Application.DTOS.ServiceCategoryDTOS;
using ServiceHub.Application.Queries.ServiceCategories;
using ServiceHub.Domain.Interfaces.Repositories;
using ServiceHub.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Handlers.ServiceCategories;

public class GetAllServiceCategoriesQueryHandler : IRequestHandler<GetAllServiceCategoriesQuery, ApiResponse<IEnumerable<ServiceCategoryDTO>>>
{
    private readonly IServiceCategoryRepository _serviceCategoryRepository;
    public GetAllServiceCategoriesQueryHandler(IServiceCategoryRepository serviceCategoryRepository)
    {
        _serviceCategoryRepository = serviceCategoryRepository;
    }
    public async Task<ApiResponse<IEnumerable<ServiceCategoryDTO>>> Handle(GetAllServiceCategoriesQuery request, CancellationToken cancellationToken)
    {
        var serviceCategories = await _serviceCategoryRepository.GetAllAsync(cancellationToken);
        if (serviceCategories == null || !serviceCategories.Any()) return ApiResponseHelpers.NotFound<IEnumerable<ServiceCategoryDTO>>("No service categories found.");
        var model = serviceCategories.Select(sc => new ServiceCategoryDTO
        {
            Id = sc.Id,
            Name = sc.Name,
            Description = sc.Description
        });
        return ApiResponseHelpers.Ok<IEnumerable<ServiceCategoryDTO>>(model);
    }
}
