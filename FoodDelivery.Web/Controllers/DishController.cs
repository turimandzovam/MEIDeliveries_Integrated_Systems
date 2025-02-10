using FoodDelivery.Domain.Domain;
using FoodDelivery.Domain.DTO;
using FoodDelivery.Domain.Enums;
using FoodDelivery.Domain.View;
using FoodDelivery.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FoodDelivery.Web.Controllers
{
    public class DishController : Controller
    {

        private readonly IDishService _dishService;
        private readonly IRestaurantService _restaurantService;

        public DishController(IDishService dishService,IRestaurantService restaurantService)
        {
            _dishService = dishService;
            _restaurantService= restaurantService;
        }

        // DET: Index
        public IActionResult Index()
        {
            Console.WriteLine("Index action method was called.");
            return View(_dishService.GetAllDishes());
        }

        public IActionResult ListAllMealsForRestaurant(Guid? id)
        {
          
            var restaurant = _restaurantService.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            ViewBag.Restaurant = restaurant;

            var dishes = _dishService.GetAllDishesForRestaurant(id);
            ViewBag.Dishes = dishes;
            return View(dishes);
        }


        // GET: Dish/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = _dishService.GetDishById(id);

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dish/Create
    
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var restaurants = _restaurantService.GetAllRestaurants();
            var categories = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();

     
            ViewBag.Restaurants = new SelectList(restaurants, "Id", "RestaurantName");
            ViewBag.Categories = new SelectList(categories);
            return View();
        }

        // POST: Dish/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("Id,DishCategory,DishName,DishIngredients,DishImage,Price,RestaurantId")] DishViewModel dishViewModel)
        {
            if (ModelState.IsValid)
            {
 
                var dish = new Dish
                {
                    DishCategory = dishViewModel.DishCategory,
                    DishName = dishViewModel.DishName,
                    DishIngredients = dishViewModel.DishIngredients,
                    DishImage = dishViewModel.DishImage,
                    Price = dishViewModel.Price,
                    Restaurant = _restaurantService.GetRestaurantById(dishViewModel.RestaurantId)
                };

                _dishService.CreateNewDish(dish);
                return RedirectToAction(nameof(Index));
            }


            ViewBag.Restaurants = new SelectList(_restaurantService.GetAllRestaurants(), "Id", "RestaurantName", dishViewModel.RestaurantId);
            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(Category)).Cast<Category>().ToList(), dishViewModel.DishCategory);

            return View(dishViewModel);
        }

        // GET: Dish/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = _dishService.GetDishById(id);

            if (dish == null)
            {
                return NotFound();
            }

            var dishViewModel = new DishViewModel
            {
                Id = dish.Id,
                DishCategory = dish.DishCategory,
                DishName = dish.DishName,
                DishImage = dish.DishImage,
                DishIngredients = dish.DishIngredients,
                Price = dish.Price,
                RestaurantId = dish.Restaurant?.Id 
            };

            ViewBag.Restaurants = new SelectList(_restaurantService.GetAllRestaurants(), "Id", "RestaurantName", dish.Restaurant?.Id);
            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(Category)).Cast<Category>().ToList(), dish.DishCategory);

            return View(dishViewModel);
        }

        // POST: Dish/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id, [Bind("Id,DishCategory,DishName,DishIngredients,DishImage,Price,RestaurantId")] DishViewModel dishViewModel)
        {
            if (id != dishViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dish = _dishService.GetDishById(id);

                if (dish == null)
                {
                    return NotFound();
                }

                dish.DishCategory = dishViewModel.DishCategory;
                dish.DishName = dishViewModel.DishName;
                dish.DishIngredients = dishViewModel.DishIngredients;
                dish.DishImage = dishViewModel.DishImage;
                dish.Price = dishViewModel.Price;
                dish.Restaurant = _restaurantService.GetRestaurantById(dishViewModel.RestaurantId);

                _dishService.UpdateDish(dish);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Restaurants = new SelectList(_restaurantService.GetAllRestaurants(), "Id", "RestaurantName", dishViewModel.RestaurantId);
            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(Category)).Cast<Category>().ToList(), dishViewModel.DishCategory);

            return View(dishViewModel);
        }

        // GET: Dish/AddToCart/5
     
        [Authorize]
        public IActionResult AddToCart(Guid? id)
        {
            var model = this._dishService.GetCartInfo(id);
            return View(model);
        }

        // POST: Dish/AddToCart/5
        [HttpPost]
        public IActionResult AddToCart(AddToCartDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._dishService.AddToCart(model, userId);

            if (result)
            {
                return RedirectToAction("Index", "Restaurants");
            }

            return View(model);
        }


        // GET: Dish/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = _dishService.GetDishById(id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _dishService.DeleteDish(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
