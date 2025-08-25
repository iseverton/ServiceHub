using ServiceHub.Domain.Entities.Identity;
using ServiceHub.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Entities;

public class Provider 
{
    public Guid Id { get; set; }
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public string Description { get; set; }
    public ICollection<Service> Services { get; set; }
    public ICollection<ServiceReview> ServiceReviews { get; set; }

    public Provider(string firstName, string lastName, Address address, string description)
    {
        Description = description;
        Services = new List<Service>();
    }

    public Provider() 
    {
        Services = new List<Service>();
        ServiceReviews = new List<ServiceReview>();
    }


}
