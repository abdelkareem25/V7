namespace V7.Domain.Entites.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Streat { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string AppUserId { get; set; } //Fk
        public AppUser User { get; set; }
    }
}