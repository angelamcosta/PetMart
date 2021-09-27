using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary
{
    public class Order
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual Client Client { get; set; }
        public virtual List<OrderItem> Items { get; set; }
    }
}
