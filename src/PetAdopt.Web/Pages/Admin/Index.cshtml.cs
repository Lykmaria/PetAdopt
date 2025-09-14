using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetAdopt.Application.Repositories;
using PetAdopt.Domain.Entities;

namespace PetAdopt.Web.Pages.Admin.Applications;

[Authorize(Policy = "RequireAdmin")]
public class IndexModel(IGenericRepository<AdoptionApplication> appsRepo, IGenericRepository<Pet> petsRepo) : PageModel
{
    public record Row(int Id, string PetName, string ApplicantUserId, ApplicationStatus Status, DateTime CreatedAtUtc);

    public List<Row> Items { get; private set; } = [];

    public async Task OnGetAsync()
    {
        var apps = await appsRepo.ListAsync();

        var petNames = new Dictionary<int, string>();
        foreach (var petId in apps.Select(a => a.PetId).Distinct())
        {
            var pet = await petsRepo.GetByIdAsync(petId);
            petNames[petId] = pet?.Name ?? $"Pet #{petId}";
        }

        Items = apps
            .OrderBy(a => a.Status) // Pending first
            .ThenByDescending(a => a.CreatedAtUtc)
            .Select(a => new Row(a.Id, petNames[a.PetId], a.ApplicantUserId, a.Status, a.CreatedAtUtc))
            .ToList();
    }
}
