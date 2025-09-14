namespace PetAdopt.Domain.Entities;

/// <summary>Back-office decision state of an adoption application.</summary>
public enum ApplicationStatus { Pending = 0, Approved = 1, Rejected = 2 }

/// <summary>Adoption application submitted by a user for a specific pet.</summary>
public class AdoptionApplication
{
    public int Id { get; set; }

    /// <summary>Target pet FK.</summary>
    public int PetId { get; set; }

    /// <summary>Target pet navigation.</summary>
    public Pet Pet { get; set; } = default!;

    /// <summary>Applicant (ASP.NET Identity) user id.</summary>
    public string ApplicantUserId { get; set; } = default!;

    /// <summary>Processing status.</summary>
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;

    /// <summary>UTC creation timestamp.</summary>
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    /// <summary>UTC review timestamp (if reviewed).</summary>
    public DateTime? ReviewedAtUtc { get; set; }

    /// <summary>Optional note entered by applicant.</summary>
    public string? ApplicantNote { get; set; }

    /// <summary>Optional note entered by admin upon decision.</summary>
    public string? AdminNote { get; set; }

    /// <summary>Admin (ASP.NET Identity) user id who reviewed.</summary>
    public string? ReviewedByUserId { get; set; }
}
