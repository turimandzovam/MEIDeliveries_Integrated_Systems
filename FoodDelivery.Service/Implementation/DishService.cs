using FoodDelivery.Domain.Domain;
using FoodDelivery.Domain.DTO;
using FoodDelivery.Repository.implementation;
using FoodDelivery.Repository.Interface;
using FoodDelivery.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Service.Implementation
{
    public class DishService : IDishService
    {
        private readonly IRepository<Dish> _dishRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDishRepository _dishCustomRepository;
        public readonly IRepository<DishInShoppingCart> _dishInShoppingCartRepository;

        public DishService(IRepository<Dish> dishRepository, IUserRepository userRepository,IDishRepository dishRepository1, IRepository<DishInShoppingCart> dishInShoppingCartRepository)
        {
            _dishRepository = dishRepository;
            _userRepository = userRepository;
            _dishCustomRepository = dishRepository1;
            _dishInShoppingCartRepository = dishInShoppingCartRepository;
        }   

        public Dish CreateNewDish( Dish dish)
        {
            return _dishRepository.Insert(dish);
        }

        public Dish DeleteDish(Guid id)
        {
            var dish_to_delete = this.GetDishById(id);
            return _dishRepository.Delete(dish_to_delete);
        }

        public List<Dish> GetAllDishes()
        {
            return _dishRepository.GetAll().ToList();
          
        }

        public Dish GetDishById(Guid? id)
        {
            return _dishCustomRepository.Get((Guid)id);
        }

        public Dish UpdateDish(Dish dish)
        {
            return _dishRepository.Update(dish);
        }

        public AddToCartDTO GetCartInfo(Guid? id)
        {
            var selectedDish = this.GetDishById(id);

            var model = new AddToCartDTO
            {
                SelectedDishId = selectedDish.Id,
                SelectedDishName = selectedDish.DishName,
                Quantity = 1
            };

            return model;
        }

        public bool AddToCart(AddToCartDTO item, string userId)
        {
            var selectedDish = this.GetDishById(item.SelectedDishId);
            var user = this._userRepository.Get(userId);

            var userCart = user.UserCart;

            if (userCart != null && selectedDish != null)
            {
                DishInShoppingCart itemToAdd = new DishInShoppingCart
                {
                    DishId = selectedDish.Id,
                    Dish = selectedDish,
                    ShoppingCartId = userCart.Id,
                    ShoppingCart = userCart,
                    Quantity = item.Quantity
                };

                this._dishInShoppingCartRepository.Insert(itemToAdd);
                return true;
            }
            return false;
        }

        public List<Dish> GetAllDishesForRestaurant(Guid? id)
        {
            if (id == null)
            {
                return new List<Dish>();
            }

            var dishes = this.GetAllDishes();

         
            var filteredDishes = dishes.Where(dish => dish.Restaurant != null && dish.Restaurant.Id == id).ToList();

            return filteredDishes;
        }

    }
}
