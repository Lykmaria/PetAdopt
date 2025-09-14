namespace PetAdopt.Application.Services;

public interface IAdoptionService
{
    Task<int> ApplyAsync(int petId, string userId, string? note);
    Task<bool> ReviewAsync(int applicationId, string adminUserId, bool approve, string? adminNote);
}
