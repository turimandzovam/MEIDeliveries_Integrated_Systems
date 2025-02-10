using FoodDelivery.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Repository.Interface
{
    public interface IDishRepository
    {
        
            Dish Get(Guid id);

        
    }
}
