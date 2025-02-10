using FoodDelivery.Domain.Domain;
using FoodDelivery.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repository
{
    public class ApplicationDbContext : IdentityDbContext<FoodDeliveryApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<DishInOrder> DishInOrders { get; set; }
        public virtual DbSet<DishInShoppingCart> DishInShoppingCarts { get; set; }







    }
}
