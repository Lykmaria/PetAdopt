namespace PetAdopt.Domain.Entities;

/// <summary>Represents an animal shelter.</summary>
public class Shelter
{
    public int Id { get; set; }

    /// <summary>Shelter's display name.</summary>
    public string Name { get; set; } = default!;

    /// <summary>Short description / about text.</summary>
    public string? Description { get; set; }

    /// <summary>Postal address (optional).</summary>
    public string? Address { get; set; }

    /// <summary>Contact phone (optional).</summary>
    public string? Phone { get; set; }

    /// <summary>Contact email (optional).</summary>
    public string? Email { get; set; }

    /// <summary>Pets hosted by this shelter.</summary>
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();

    /// <summary>User reviews for this shelter.</summary>
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
