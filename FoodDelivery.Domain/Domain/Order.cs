using FoodDelivery.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Domain
{
    public class Order:BaseEntity
    {
        public string? OwnerId { get; set; }
        public FoodDeliveryApplicationUser? Owner { get; set; }
        [Required]
        public string? address { get; set; }
        [Required]
        public string? contactPhone { get; set; }

        public int? totalPrice { get; set; }


        public virtual ICollection<DishInOrder>? DishInOrders { get; set; }


    }
}
