using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class Client : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NIF { get; set; }
        public virtual List<ShoppingItem> Items { get; set; }
        public virtual List<Address> Address { get; set; }
        public virtual List<Order> Order { get; set; }
    }
}
