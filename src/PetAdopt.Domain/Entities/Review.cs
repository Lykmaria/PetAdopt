namespace PetAdopt.Domain.Entities;

/// <summary>User review for a shelter.</summary>
public class Review
{
    public int Id { get; set; }

    /// <summary>Reviewed shelter FK.</summary>
    public int ShelterId { get; set; }

    /// <summary>Reviewed shelter navigation.</summary>
    public Shelter Shelter { get; set; } = default!;

    /// <summary>Author (ASP.NET Identity) user id.</summary>
    public string UserId { get; set; } = default!;

    /// <summary>Rating 1..5 (enforced in validation/DB config).</summary>
    public int Rating { get; set; }

    /// <summary>Optional textual comment.</summary>
    public string? Comment { get; set; }

    /// <summary>UTC creation timestamp.</summary>
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
