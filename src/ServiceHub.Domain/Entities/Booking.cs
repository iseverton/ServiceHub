using ServiceHub.Domain.Entities.Identity;
using ServiceHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public Guid ServiceId { get; set; }
    public Guid ClientId { get; set; }
    public Guid ProviderId { get; set; }
    public DateTime ScheduledAt { get; set; }
    public EBookingStatus Status { get; set; } 
    public DateTime CreatedAt { get; set; }
    
    public Service Service { get; set; }
    public User Client { get; set; }
    public Provider Provider { get; set; }
    public ServiceReview ServiceReview { get; set; }

    public Booking(Guid serviceId, Guid clientId, Guid providerId, DateTime scheduledAt, EBookingStatus status)
    {
        Id = Guid.NewGuid();
        ServiceId = serviceId;
        ClientId = clientId;
        ProviderId = providerId;
        ScheduledAt = scheduledAt;
        Status = status;
        CreatedAt = DateTime.UtcNow;
    }

    public Booking()
    {
    }
}
