using Microsoft.AspNetCore.Identity;

namespace PetAdopt.Data.Identity;

/// <summary>Application user with optional profile fields.</summary>
public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
