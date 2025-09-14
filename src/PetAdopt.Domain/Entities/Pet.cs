namespace PetAdopt.Domain.Entities;

/// <summary>Status of a pet in the adoption flow.</summary>
public enum PetStatus { Available = 0, Pending = 1, Adopted = 2 }

/// <summary>Basic species categories supported by the app.</summary>
public enum Species { Dog, Cat, Rabbit, Bird, Other }

/// <summary>Represents a pet that can be adopted.</summary>
public class Pet
{
    public int Id { get; set; }

    /// <summary>Pet's display name.</summary>
    public string Name { get; set; } = default!;

    /// <summary>Species classification.</summary>
    public Species Species { get; set; }

    /// <summary>Breed (optional, free text).</summary>
    public string? Breed { get; set; }

    /// <summary>Approximate age in years.</summary>
    public int AgeYears { get; set; }

    /// <summary>Gender (optional; free text keeps it simple).</summary>
    public string? Gender { get; set; }

    /// <summary>Adoption status (Available/Pending/Adopted).</summary>
    public PetStatus Status { get; set; } = PetStatus.Available;

    /// <summary>Owning shelter FK.</summary>
    public int ShelterId { get; set; }

    /// <summary>Owning shelter navigation.</summary>
    public Shelter Shelter { get; set; } = default!;

    /// <summary>Applications submitted for this pet.</summary>
    public ICollection<AdoptionApplication> Applications { get; set; } = new List<AdoptionApplication>();
}
