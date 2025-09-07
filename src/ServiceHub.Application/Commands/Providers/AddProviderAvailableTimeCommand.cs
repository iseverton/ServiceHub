using MediatR;
using ServiceHub.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Commands.Providers;

public class AddProviderAvailableTimeCommand : IRequest<ApiResponse<Guid?>>
{
    public Guid ProviderId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public AddProviderAvailableTimeCommand(Guid providerId, DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
    {
        ProviderId = providerId;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
    }
}
