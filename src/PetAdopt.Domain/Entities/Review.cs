namespace PetAdopt.Domain.Entities;

/// <summary>User review for a shelter.</summary>
public class Review
{
    public int Id { get; set; }

    public int ShelterId { get; set; }
    public Shelter Shelter { get; set; } = default!;

    public string UserId { get; set; } = default!; // IdentityUser Id
    public int Rating { get; set; } // 1..5
    public string? Comment { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
