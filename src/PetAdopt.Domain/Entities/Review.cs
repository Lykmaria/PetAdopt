namespace PetAdopt.Domain.Entities
{
    /// <summary>
    /// Review and rating left for a shelter.
    /// </summary>
    public class Review
    {
        public int Id { get; set; }
        public int ShelterId { get; set; }
        public Shelter? Shelter { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        public int Rating { get; set; } // 1–5
        public string Comment { get; set; } = string.Empty;
    }
}
