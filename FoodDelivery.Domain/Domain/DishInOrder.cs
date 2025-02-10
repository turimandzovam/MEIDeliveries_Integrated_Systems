using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Domain
{
    public class DishInOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        public Guid DishId { get; set; }
        public Dish? OrderedDish { get; set; }

        public int Quantity { get; set; }
    }
}
