using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetAdopt.Application.Services;
using PetAdopt.Data.Identity;
using PetAdopt.Application.Repositories;
using PetAdopt.Domain.Entities;

namespace PetAdopt.Web.Pages.Admin.Applications;

[Authorize(Policy = "RequireAdmin")]
public class ReviewModel(
    IAdoptionService adoptionSvc,
    IGenericRepository<AdoptionApplication> appsRepo,
    UserManager<ApplicationUser> userMgr
) : PageModel
{
    [BindProperty] public string? AdminNote { get; set; }
    public AdoptionApplication App { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        App = await appsRepo.GetByIdAsync(id) ?? throw new InvalidOperationException("Application not found");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, bool approve)
    {
        var adminId = userMgr.GetUserId(User)!;
        await adoptionSvc.ReviewAsync(id, adminId, approve, AdminNote);
        TempData["Msg"] = approve ? "Approved." : "Rejected.";
        return RedirectToPage("Index");
    }
}
