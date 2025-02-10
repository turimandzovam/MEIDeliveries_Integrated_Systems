using FoodDelivery.Domain.Domain;
using FoodDelivery.Domain.DTO;
using FoodDelivery.Repository.Interface;
using FoodDelivery.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Service.Implementation
{
    public class ShoppingCartService:IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<DishInOrder> _dishInOrderRepository;
        private readonly IDishRepository dishRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<DishInOrder> dishInOrderRepository,IDishRepository dish)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _dishInOrderRepository = dishInOrderRepository;
            dishRepository = dish;
        }

        public bool DeleteFromShoppingCart(string userId, Guid ?dishId)
        {

            if (!string.IsNullOrEmpty(userId) && dishId != null)
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCart = loggedInUser?.UserCart;

                var dish_to_delete = userCart.DishInShoppingCarts.FirstOrDefault(x => x.DishId == dishId);

                userCart.DishInShoppingCarts.Remove(dish_to_delete);

                this._shoppingCartRepository.Update(userCart);

                return true;

            }
            return false;

        }

        public double totalDelivery(List<DishInShoppingCart> dishesInOrder)

        {
            double delivery = 0.0;
            var dishesId = dishesInOrder.Select(d => d.Dish.Id).Where(d => d != null).ToList();
            var dishes = dishesId.Select(d => dishRepository.Get(d)).Where(dish => dish != null).ToList();
            var uniqueRestaurants = dishes.Select(dish => dish.Restaurant) .Where(restaurant => restaurant != null)
            .Distinct().ToList();
            foreach(var restaurant in uniqueRestaurants)
            {
                if (restaurant.MinPriceForOrder != null)
                {
                    delivery +=(double) restaurant.MinPriceForOrder;
                }
               
            }
            return delivery;

        }
        public ShoppingCartDTO getShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

          
            var allDishes = loggedInUser?.UserCart?.DishInShoppingCarts?.ToList() ?? new List<DishInShoppingCart>();

            double totalPrice = 0.0;
            double delivery = totalDelivery(allDishes);
          
            foreach (var item in allDishes)
            {
                totalPrice += item.Quantity * (double)item.Dish.Price;
            }

            var model = new ShoppingCartDTO
            {
                AllDishes = allDishes,
                TotalPrice = totalPrice,
                DeliveryPrice = delivery
               
            };

            return model;
        }

        public bool order(string userId, string? address, string? phone, decimal? totalPrice)
        {
            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCart = loggedInUser?.UserCart;


                var userOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    Owner = loggedInUser,
                    OwnerId = loggedInUser.Id,
                    address=address,
                    contactPhone=phone,
                    totalPrice= (int?)totalPrice

                };

                this._orderRepository.Insert(userOrder);
                

                var dishInOrder = userCart?.DishInShoppingCarts.Select(x => new DishInOrder
                {
                   
                    DishId = x.DishId,
                    OrderedDish = x.Dish,
                    Order = userOrder,
                    OrderId = userOrder.Id,
                    Quantity = x.Quantity
                }).ToList();

         
                foreach (var dish in dishInOrder)
                {
                    _dishInOrderRepository.Insert(dish);
                }
  

                userCart?.DishInShoppingCarts.Clear();

                _shoppingCartRepository.Update(userCart);

                return true;
            }
            return false;
        }

        public bool onlinePayment(ShoppingCartDTO cart)
        {

            var dishesId = cart.AllDishes.Select(d => d.Dish.Id).Where(d => d != null).ToList();
            var dishes = dishesId.Select(d => dishRepository.Get(d)).Where(dish => dish != null).ToList();
            var uniqueRestaurants = dishes.Select(dish => dish.Restaurant).Where(restaurant => restaurant != null)
           .Distinct().ToList();
            foreach (var restaurant in uniqueRestaurants)
            {
                if(restaurant != null && !restaurant.OnlinePayment)
        {
                    return false;
                }
            }
            return true;

        }
    }
}
