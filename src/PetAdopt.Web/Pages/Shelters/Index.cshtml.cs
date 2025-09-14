using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetAdopt.Application.Repositories;
using PetAdopt.Domain.Entities;

namespace PetAdopt.Web.Pages.Shelters;

[AllowAnonymous]
public class IndexModel(IGenericRepository<Shelter> sheltersRepo) : PageModel
{
    public List<Shelter> Shelters { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Shelters = await sheltersRepo.ListAsync();
    }
}
