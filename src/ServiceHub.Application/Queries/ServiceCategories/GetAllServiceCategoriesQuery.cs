using MediatR;
using ServiceHub.Application.DTOS.ServiceCategoryDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Queries.ServiceCategories;

public class GetAllServiceCategoriesQuery : IRequest<ApiResponse<IEnumerable<ServiceCategoryDTO>>>
{
}
