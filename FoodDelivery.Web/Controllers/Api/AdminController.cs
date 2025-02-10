using FoodDelivery.Domain.Domain;
using FoodDelivery.Domain.Identity;
using FoodDelivery.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<FoodDeliveryApplicationUser> userManager;

        public AdminController(IOrderService orderService, UserManager<FoodDeliveryApplicationUser> userManager)
        {
            _orderService = orderService;
            this.userManager = userManager;
        }

        [HttpGet("[action]")]
        public List<Order> GetAllActiveOrders()
        {
            return _orderService.GetAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            return _orderService.GetOrderDetails(model);
        }

        [HttpPost("[action]")]
        public IActionResult DeleteOrder(BaseEntity model)
        {
            var order = _orderService.GetOrderDetails(model);
            if (order != null)
            {
                _orderService.DeleteOrder(order);
                return Ok(); 
            }
            return NotFound(); 
        }

    }
}
