using FoodDelivery.Domain.Domain;
using FoodDelivery.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Repository.implementation
{
    public class DishRepository : IDishRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Dish> entities;

        public DishRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Dish>();
        }
        public Dish Get(Guid id)
        {
            return entities.Include(d => d.Restaurant).FirstOrDefault(d => d.Id == id);
        }
    }
}
