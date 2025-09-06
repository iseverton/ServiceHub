using MediatR;
using ServiceHub.Domain.Enums;
using ServiceHub.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Commands.Services;

public class CreateServiceCommand : IRequest<ApiResponse<Guid?>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ICollection<Guid> CategoryIds { get; set; }
    public Guid ProviderId { get; set; }
}
