namespace PetAdopt.Domain.Entities
{
    /// <summary>
    /// A pet that can be adopted.
    /// </summary>
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // e.g. Dog, Cat
        public int Age { get; set; }
        public int ShelterId { get; set; }
        public Shelter? Shelter { get; set; }
    }
}
