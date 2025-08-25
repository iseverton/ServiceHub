using ServiceHub.Domain.Entities.Identity;
using ServiceHub.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Entities;

public class User : ApplicationUser
{
    public ICollection<CustomServiceRequest> CustomServices { get; set; }
    
    public User(string firstName, string lastName, Address address)
        : base(firstName, lastName, address)
    {
        CustomServices = new List<CustomServiceRequest>();
    }
}
