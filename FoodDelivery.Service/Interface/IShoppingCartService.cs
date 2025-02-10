using FoodDelivery.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDTO getShoppingCartInfo(string userId);

        bool DeleteFromShoppingCart(string userId, Guid? dishId);

        bool order(string userId,string? address,string? phone,decimal? totalPrice);

        bool onlinePayment(ShoppingCartDTO cart);
      
    }
}
