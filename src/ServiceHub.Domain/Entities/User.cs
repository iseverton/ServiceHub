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
    public string Name { get; set; }
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public ICollection<CustomServiceRequest> CustomServices { get; set; }
    public ICollection<ServiceReview>? ServiceReviews { get; set; }
    public User(ApplicationUser applicationUser,string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        ApplicationUserId = applicationUser.Id;
        ApplicationUser = applicationUser;
        CustomServices = new List<CustomServiceRequest>();
        ServiceReviews = new List<ServiceReview>();
    }



    public User()
    {
    }
}
