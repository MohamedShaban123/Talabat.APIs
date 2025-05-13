namespace Talabat.Core.Entities.Identity
{
    public class Address
    {
        public int  Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }


        // Navigation Property
        public string AppUserId { get; set; } // Foreign Key : Users
    }
}