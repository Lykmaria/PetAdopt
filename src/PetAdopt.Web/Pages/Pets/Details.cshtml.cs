using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetAdopt.Application.Repositories;
using PetAdopt.Application.Services;
using PetAdopt.Data.Identity;
using PetAdopt.Domain.Entities;

namespace PetAdopt.Web.Pages.Pets;

[Authorize]
public class DetailsModel(IAdoptionService adoptionSvc, UserManager<ApplicationUser> userMgr, IGenericRepository<Pet> petsRepo) : PageModel
{
    [BindProperty] public string? ApplicantNote { get; set; }
    public Pet Pet { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Pet = await petsRepo.GetByIdAsync(id) ?? throw new InvalidOperationException("Pet not found");
        return Page();
    }

    public async Task<IActionResult> OnPostApplyAsync(int id)
    {
        var userId = userMgr.GetUserId(User)!;
        await adoptionSvc.ApplyAsync(id, userId, ApplicantNote);
        TempData["Msg"] = "Application submitted!";
        return RedirectToPage("/Account/Applications");
    }
}
