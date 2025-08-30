using Microsoft.AspNetCore.Identity;
using ServiceHub.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    public ApplicationUser(string email,string? phone)
    {
        Id = Guid.NewGuid();
        Email = email;
        UserName = email;
        PhoneNumber = phone;
        CreatedAt = DateTime.UtcNow;
        LastUpdatedAt = DateTime.UtcNow;
    }
    public ApplicationUser()
    {
        CreatedAt = DateTime.UtcNow;
        LastUpdatedAt = DateTime.UtcNow;
    }
}
