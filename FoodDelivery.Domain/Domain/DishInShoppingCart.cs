using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Domain
{
    public class DishInShoppingCart :BaseEntity
    {
        public Guid DishId { get; set; }
        public Dish? Dish { get; set; }

        public Guid ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }

        public int Quantity { get; set; }
    }
}
