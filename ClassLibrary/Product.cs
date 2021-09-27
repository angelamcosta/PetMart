using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
        public virtual List<ShoppingItem> ShoppingItems { get; set; }
    }
}