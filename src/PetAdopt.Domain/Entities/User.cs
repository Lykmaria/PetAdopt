namespace PetAdopt.Domain.Entities
{
    /// <summary>
    /// Application user.
    /// </summary>
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; set; } = string.Empty;

        public ICollection<AdoptionRequest>? AdoptionRequests { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
