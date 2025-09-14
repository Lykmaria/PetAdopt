namespace PetAdopt.Domain.Entities
{
    /// <summary>
    /// Request submitted by a user to adopt a pet.
    /// </summary>
    public class AdoptionRequest
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public Pet? Pet { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // Pending/Approved/Rejected
    }
}
