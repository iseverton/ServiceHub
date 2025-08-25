using ServiceHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Entities;

public class Service
{
    public Guid Id { get; set; }
    public Guid ProviderId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ICollection<ServiceCategory> Categories { get; set; }
    public ICollection<ServiceReview>? ServiceReviews  { get; set; }
    public EServiceStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    public Provider Provider { get; set; }
    public ICollection<Booking> Bookings { get; set; }

    public Service(Guid providerId, string title, string description, decimal price, EServiceStatus status)
    {
        ProviderId = providerId;
        Title = title;
        Description = description;
        Price = price;
        Categories = new List<ServiceCategory>();
        Status = status;
    }
}
