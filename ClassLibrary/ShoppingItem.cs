using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual Client Client { get; set; }
    }
}
