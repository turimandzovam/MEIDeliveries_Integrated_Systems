using FoodDelivery.Domain.Domain;
using FoodDelivery.Domain.Identity;
using FoodDelivery.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Repository.implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }

        public Order Delete(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
            return entity;
        }

        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.DishInOrders)
                .Include(z => z.Owner)
                .Include("DishInOrders.OrderedDish")
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
               .Include(z => z.DishInOrders)
               .Include(z => z.Owner)
               .Include("DishInOrders.OrderedDish")
               .Include("DishInOrders.OrderedDish.Restaurant")
               .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}
