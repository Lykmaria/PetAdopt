using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetAdopt.Application.Repositories;
using PetAdopt.Data.Identity;
using PetAdopt.Domain.Entities;

namespace PetAdopt.Web.Pages.Shelters;

[Authorize] // default: protected
public class DetailsModel(
    IGenericRepository<Shelter> sheltersRepo,
    IGenericRepository<Review> reviewsRepo,
    UserManager<ApplicationUser> userMgr
) : PageModel
{
    public Shelter Shelter { get; private set; } = default!;
    public List<Review> Reviews { get; private set; } = [];

    public double AverageRating => Reviews.Count == 0 ? 0 : Math.Round(Reviews.Average(r => r.Rating), 1);

    [BindProperty, Range(1, 5)]
    public int Rating { get; set; } = 5;

    [BindProperty, StringLength(500)]
    public string? Comment { get; set; }

    [AllowAnonymous]
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Shelter = await sheltersRepo.GetByIdAsync(id) ?? throw new InvalidOperationException("Shelter not found");
        Reviews = await reviewsRepo.ListAsync(r => r.ShelterId == id);
        return Page();
    }

    public async Task<IActionResult> OnPostAddReviewAsync(int id)
    {
        if (!User.Identity?.IsAuthenticated ?? true) return Challenge();
        if (!ModelState.IsValid)
        {
            Shelter = await sheltersRepo.GetByIdAsync(id) ?? throw new InvalidOperationException("Shelter not found");
            Reviews = await reviewsRepo.ListAsync(r => r.ShelterId == id);
            return Page();
        }

        var userId = userMgr.GetUserId(User)!;

        var review = new Review
        {
            ShelterId = id,
            UserId = userId,
            Rating = Rating,
            Comment = Comment,
            CreatedAtUtc = DateTime.UtcNow
        };

        await reviewsRepo.AddAsync(review);
        await reviewsRepo.SaveChangesAsync();

        TempData["Msg"] = "Review added.";
        return RedirectToPage("Details", new { id });
    }
}
