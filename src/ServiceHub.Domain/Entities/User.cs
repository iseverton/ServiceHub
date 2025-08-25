using ServiceHub.Domain.Entities.Identity;
using ServiceHub.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public ICollection<CustomServiceRequest> CustomServices { get; set; }
    public ICollection<ServiceReview>? ServiceReviews { get; set; }
    

    public User(string firstName, string lastName, Address address)
    {
        CustomServices = new List<CustomServiceRequest>();
    }

    public User()
    {
    }
}
