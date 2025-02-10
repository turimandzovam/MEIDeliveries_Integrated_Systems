using FoodDelivery.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.DTO
{
    public class ShoppingCartDTO
    {
        public List<DishInShoppingCart> AllDishes { get; set; }
        public double TotalPrice { get; set; }
        public double DeliveryPrice { get; set; }
    }
}
