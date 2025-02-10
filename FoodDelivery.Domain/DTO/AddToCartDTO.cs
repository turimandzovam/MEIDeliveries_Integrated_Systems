using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.DTO
{
    public class AddToCartDTO
    {
        public Guid? SelectedDishId { get; set; }
        public string? SelectedDishName { get; set; }

        public int Quantity { get; set; }
    }
}
