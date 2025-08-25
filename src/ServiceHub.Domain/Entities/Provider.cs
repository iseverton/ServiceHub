using ServiceHub.Domain.Entities.Identity;
using ServiceHub.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Entities;

public class Provider : ApplicationUser
{
    public string Description { get; set; }
    public ICollection<Service> Services { get; set; }
    public ICollection<ServiceReview> ServiceReviews { get; set; }

    public Provider(string firstName, string lastName, Address address, string description)
        : base(firstName, lastName, address)
    {
        Description = description;
        Services = new List<Service>();
    }


}
