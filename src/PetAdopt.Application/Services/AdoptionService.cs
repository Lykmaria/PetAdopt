using PetAdopt.Application.Repositories;
using PetAdopt.Domain.Entities;

namespace PetAdopt.Application.Services;

public class AdoptionService(
    IGenericRepository<AdoptionApplication> appsRepo,
    IGenericRepository<Pet> petRepo
) : IAdoptionService
{
    public async Task<int> ApplyAsync(int petId, string userId, string? note)
    {
        var pet = await petRepo.GetByIdAsync(petId) ?? throw new InvalidOperationException("Pet not found.");
        if (pet.Status != PetStatus.Available) throw new InvalidOperationException("Pet not available.");

        var app = new AdoptionApplication
        {
            PetId = petId,
            ApplicantUserId = userId,
            ApplicantNote = note
        };

        await appsRepo.AddAsync(app);
        await appsRepo.SaveChangesAsync();

        pet.Status = PetStatus.Pending;
        await petRepo.UpdateAsync(pet);
        await petRepo.SaveChangesAsync();

        return app.Id;
    }

    public async Task<bool> ReviewAsync(int applicationId, string adminUserId, bool approve, string? adminNote)
    {
        var app = await appsRepo.GetByIdAsync(applicationId);
        if (app is null) return false;

        app.Status = approve ? ApplicationStatus.Approved : ApplicationStatus.Rejected;
        app.AdminNote = adminNote;
        app.ReviewedByUserId = adminUserId;
        app.ReviewedAtUtc = DateTime.UtcNow;

        await appsRepo.UpdateAsync(app);
        await appsRepo.SaveChangesAsync();
        return true;
    }
}
