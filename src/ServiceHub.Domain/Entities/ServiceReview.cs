using ServiceHub.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Entities;

public class ServiceReview
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public Guid ServiceId { get; set; }
    public Guid UserId { get; set; }
    public Guid ProviderId { get; set; }
    public int Rating { get; set; } 
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Booking Booking { get; set; }
    public Service Service { get; set; }
    public User User { get; set; }
    public Provider Provider { get; set; }

    public ServiceReview(Guid bookingId, Guid serviceId, Guid userId, Guid providerId, int rating, string comment)
    {
        Id = Guid.NewGuid();
        BookingId = bookingId;
        ServiceId = serviceId;
        UserId = userId;
        ProviderId = providerId;
        Rating = rating;
        Comment = comment;
        CreatedAt = DateTime.UtcNow;
    }

    public ServiceReview()
    {
    }


}
