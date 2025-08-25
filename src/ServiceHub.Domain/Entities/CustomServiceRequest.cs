using ServiceHub.Domain.Entities.Identity;
using ServiceHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Entities;

public class CustomServiceRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Budget { get; set; }
    public string Location { get; set; }
    public ECustomRequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt  { get; set; }
    public User User { get; set; }

    public CustomServiceRequest(Guid userId, string title, string description, decimal budget, string location, ECustomRequestStatus status)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Title = title;
        Description = description;
        Budget = budget;
        Location = location;
        Status = status;
        CreatedAt = DateTime.UtcNow;
    }
    public CustomServiceRequest()
    {
    }
}
