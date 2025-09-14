namespace PetAdopt.Domain.Entities;

public enum ApplicationStatus { Pending = 0, Approved = 1, Rejected = 2 }

/// <summary>Adoption application submitted by a user for a pet.</summary>
public class AdoptionApplication
{
    public int Id { get; set; }

    public int PetId { get; set; }
    public Pet Pet { get; set; } = default!;

    public string ApplicantUserId { get; set; } = default!; // IdentityUser Id
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ReviewedAtUtc { get; set; }
    public string? ApplicantNote { get; set; }
    public string? AdminNote { get; set; }
    public string? ReviewedByUserId { get; set; } // Admin who took action
}
