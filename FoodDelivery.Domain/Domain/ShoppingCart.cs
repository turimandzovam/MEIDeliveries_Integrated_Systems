using FoodDelivery.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }
        public FoodDeliveryApplicationUser? Owner { get; set; }
        public virtual ICollection<DishInShoppingCart>? DishInShoppingCarts { get; set; }

    }
}
