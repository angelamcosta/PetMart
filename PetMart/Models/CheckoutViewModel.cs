using ClassLibrary;
using System.Collections.Generic;

namespace PetMart.Models
{
    public class CheckoutViewModel
    {
        public IEnumerable<ShoppingItem> Items { get; set; }
        public decimal CartTotal { get; set; }
    }
}
