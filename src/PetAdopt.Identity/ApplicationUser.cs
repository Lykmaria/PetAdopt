using Microsoft.AspNetCore.Identity;

namespace PetAdopt.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
