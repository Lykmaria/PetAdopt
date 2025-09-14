namespace PetAdopt.Domain.Entities
{
    /// <summary>
    /// A shelter that hosts pets.
    /// </summary>
    public class Shelter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public ICollection<Pet>? Pets { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
