using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool IsMainAddress { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual Client Client { get; set; }
    }
}
