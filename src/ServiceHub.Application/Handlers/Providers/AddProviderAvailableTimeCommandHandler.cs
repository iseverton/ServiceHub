using MediatR;
using Microsoft.Extensions.Logging;
using ServiceHub.Application.Commands.Providers;
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

public class AddProviderAvailableTimeCommandHandler : IRequestHandler<AddProviderAvailableTimeCommand, ApiResponse<Guid?>>
{
    private readonly ILogger<AddProviderAvailableTimeCommandHandler> _logger;
    private readonly IProviderScheduleRepository _providerScheduleRepository;
    private readonly IProviderRepository _providerRepository;
    public AddProviderAvailableTimeCommandHandler(ILogger<AddProviderAvailableTimeCommandHandler> logger, 
        IProviderScheduleRepository providerScheduleRepository,
        IProviderRepository providerRepository)
    {
        _logger = logger;
        _providerScheduleRepository = providerScheduleRepository;
        _providerRepository = providerRepository;
    }

    public async Task<ApiResponse<Guid?>> Handle(AddProviderAvailableTimeCommand request, CancellationToken cancellationToken)
    {
        var existingProvider = await _providerRepository.GetByIdAsync(request.ProviderId,cancellationToken);
        if (existingProvider == null)
        {
            _logger.LogWarning("Provider with ID {ProviderId} not found", request.ProviderId);
            return ApiResponseHelpers.NotFound<Guid?>("Provider not found");
        }

        var Schedule = new ProviderSchedule(request.DayOfWeek,request.StartTime,request.EndTime,request.ProviderId);
        
        await _providerScheduleRepository.AddAsync(Schedule,cancellationToken);
        return ApiResponseHelpers.Created<Guid?>(Schedule.Id);
    }
}
