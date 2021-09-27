namespace PetMart.Models
{
    public class OrderViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool IsMainAddress { get; set; }
    }
}
