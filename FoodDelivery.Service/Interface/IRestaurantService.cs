using FoodDelivery.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Service.Interface
{
    public interface IRestaurantService
    {
        public List<Restaurant> GetAllRestaurants();
        public Restaurant GetRestaurantById(Guid? id);

        void CreateNewRestaurant(Restaurant r);

        void UpdateExistingRestaurant(Restaurant r);
        void DeleteRestaurant(Guid id);
    }
}
