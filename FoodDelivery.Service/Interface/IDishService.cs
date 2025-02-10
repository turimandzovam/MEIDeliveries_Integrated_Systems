using FoodDelivery.Domain.Domain;
using FoodDelivery.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Service.Interface
{
    public interface IDishService
    {
        public List<Dish> GetAllDishes();
        public Dish GetDishById(Guid? id);
        public Dish CreateNewDish( Dish dish);
        public Dish UpdateDish(Dish dish);
        public Dish DeleteDish(Guid id);

        AddToCartDTO GetCartInfo(Guid? id);

        bool AddToCart(AddToCartDTO item, string userId);

        public List<Dish> GetAllDishesForRestaurant(Guid? id);

    }
}
