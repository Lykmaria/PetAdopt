namespace PetAdopt.Domain.Entities;

/// <summary>Represents an animal shelter.</summary>
public class Shelter
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
