using FoodDelivery.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<FoodDeliveryApplicationUser> GetAll();
        FoodDeliveryApplicationUser Get(string id);
        void Insert(FoodDeliveryApplicationUser entity);
        void Update(FoodDeliveryApplicationUser entity);
        void Delete(FoodDeliveryApplicationUser entity);
    }
}
