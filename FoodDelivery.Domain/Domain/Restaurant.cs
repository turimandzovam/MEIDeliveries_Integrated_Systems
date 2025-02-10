using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Domain
{
    public class Restaurant:BaseEntity
    {
        [Required(ErrorMessage = "The field is required.")]
        public string? RestaurantName {  get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public string? RestaurantLocation { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public string? RestaurantImage { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        [RegularExpression(@"^\d{2}-\d{2}$", ErrorMessage = "The field must be in the format 'NN-NN', where N is a digit.Time is in minutes")]
        public string? DeliveryTime { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public int? MinPriceForOrder { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public bool OnlinePayment { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public bool CashPayment { get; set; }

        public virtual ICollection<Dish>? DishInRestaurant { get; set; }

    }
}
