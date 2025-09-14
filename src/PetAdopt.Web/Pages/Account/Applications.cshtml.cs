using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetAdopt.Application.Repositories;
using PetAdopt.Data.Identity;
using PetAdopt.Domain.Entities;

namespace PetAdopt.Web.Pages.Account;

[Authorize]
public class ApplicationsModel(
    UserManager<ApplicationUser> userMgr,
    IGenericRepository<AdoptionApplication> appsRepo,
    IGenericRepository<Pet> petsRepo
) : PageModel
{
    public record Row(int Id, string PetName, ApplicationStatus Status, DateTime CreatedAtUtc);

    public List<Row> Items { get; private set; } = [];

    public async Task OnGetAsync()
    {
        var userId = userMgr.GetUserId(User)!;

        var apps = await appsRepo.ListAsync(a => a.ApplicantUserId == userId);

        var petNames = new Dictionary<int, string>();
        foreach (var petId in apps.Select(a => a.PetId).Distinct())
        {
            var pet = await petsRepo.GetByIdAsync(petId);
            petNames[petId] = pet?.Name ?? $"Pet #{petId}";
        }

        Items = apps
            .OrderByDescending(a => a.CreatedAtUtc)
            .Select(a => new Row(a.Id, petNames[a.PetId], a.Status, a.CreatedAtUtc))
            .ToList();
    }
}
