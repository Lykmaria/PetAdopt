namespace PetAdopt.Domain.Entities;

public enum PetStatus { Available = 0, Pending = 1, Adopted = 2 }
public enum Species { Dog = 0, Cat = 1, Rabbit = 2, Bird = 3, Other = 4 }

/// <summary>Represents a pet that can be adopted.</summary>
public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public Species Species { get; set; }
    public string? Breed { get; set; }
    public int AgeYears { get; set; }
    public string? Gender { get; set; }
    public PetStatus Status { get; set; } = PetStatus.Available;

    public int ShelterId { get; set; }
    public Shelter Shelter { get; set; } = default!;

    public ICollection<AdoptionApplication> Applications { get; set; } = new List<AdoptionApplication>();
}
