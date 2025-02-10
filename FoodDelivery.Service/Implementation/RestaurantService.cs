using FoodDelivery.Domain.Domain;
using FoodDelivery.Repository.Interface;
using FoodDelivery.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Service.Implementation
{
    public class RestaurantService : IRestaurantService
    {

        private readonly IRepository<Restaurant> _restaurantRepository;
      
        public RestaurantService(IRepository<Restaurant> restaurantRepository)
        {
           _restaurantRepository = restaurantRepository;
           
        }

        public void CreateNewRestaurant(Restaurant r)
        {
            this._restaurantRepository.Insert(r);
        }

        public void DeleteRestaurant(Guid id)
        {
            var restaurant = this.GetRestaurantById(id);
            this._restaurantRepository.Delete(restaurant);
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return this._restaurantRepository.GetAll().ToList();
        }

        public Restaurant GetRestaurantById(Guid? id)
        {
            return this._restaurantRepository.Get(id);
        }

        public void UpdateExistingRestaurant(Restaurant r)
        {
            this._restaurantRepository.Update(r);
        }
    }
}
