using FoodDelivery.Domain.Domain;
using FoodDelivery.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();

        Order GetOrderDetails(BaseEntity model);

        Order Delete(Order entity);
    }
}
