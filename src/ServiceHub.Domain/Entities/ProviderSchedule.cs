using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Entities;

public class ProviderSchedule
{
    public Guid Id { get; set; }
    public Guid ProviderId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsActive { get; set; }
    public Provider? Provider { get; set; }

    public ProviderSchedule(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime, Guid provider)
    {
        Id = Guid.NewGuid();
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        ProviderId = provider;
        IsActive = true;
    }

    protected ProviderSchedule() { }
}
