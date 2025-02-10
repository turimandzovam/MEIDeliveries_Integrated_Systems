using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.Domain.Domain;
using Microsoft.EntityFrameworkCore.Internal;

namespace FoodDelivery.Domain.Identity
{
    public class FoodDeliveryApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public ShoppingCart? UserCart { get; set; }
        public virtual ICollection<Order>? MyOrders { get; set; }


    }
}
