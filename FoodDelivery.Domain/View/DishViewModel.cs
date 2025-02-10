using FoodDelivery.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.View
{
    public class DishViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public Category? DishCategory { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public string? DishName { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public string? DishImage { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public string? DishIngredients { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public int? Price { get; set; }

        [Required(ErrorMessage = "The field is required.")]
        public Guid? RestaurantId { get; set; }
    }
}
