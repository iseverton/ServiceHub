using MediatR;
using ServiceHub.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Commands.ServiceCategory;

public class CreateServiceCategoryCommand : IRequest<ApiResponse<Guid>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public CreateServiceCategoryCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
