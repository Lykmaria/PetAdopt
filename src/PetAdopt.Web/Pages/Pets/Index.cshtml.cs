using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetAdopt.Application.Repositories;
using PetAdopt.Domain.Entities;

namespace PetAdopt.Web.Pages.Pets;

[AllowAnonymous]
public class IndexModel(IGenericRepository<Pet> petsRepo) : PageModel
{
    public List<Pet> Pets { get; private set; } = [];

    public async Task OnGetAsync(string? species, string? status)
    {
        Pets = await petsRepo.ListAsync(p =>
            (species == null || p.Species.ToString() == species) &&
            (status == null || p.Status.ToString() == status));
    }
}
