using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Domain.Domain;
using FoodDelivery.Repository;
using FoodDelivery.Service.Interface;
using FoodDelivery.Domain.Payment;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Stripe;
using FoodDelivery.Domain.DTO;
using System.Net;

namespace FoodDelivery.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly StripeSettings _stripeSettings;

        public ShoppingCartsController(IShoppingCartService shoppingCartService, IOptions<StripeSettings> stripeSettings)
        {
            _shoppingCartService = shoppingCartService;
            _stripeSettings = stripeSettings.Value;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            Console.WriteLine("Index action method was called.");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            return View(_shoppingCartService.getShoppingCartInfo(userId));
        }

        public async Task<IActionResult> DeleteProductFromShoppingCart(Guid? dishId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            var result = _shoppingCartService.DeleteFromShoppingCart(userId, dishId);
            return RedirectToAction("Index");
        }

        public IActionResult OrderForm()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            var cart = _shoppingCartService.getShoppingCartInfo(userId);
            var payment = _shoppingCartService.onlinePayment(cart);
            var orderDetails = new OrderDetailsDTO
            {
                TotalPrice = (decimal)cart.TotalPrice + (decimal)cart.DeliveryPrice,
                onlinePayment = payment
            };
            return View(orderDetails);
        }

        [HttpPost]
        public IActionResult OrderDetails(OrderDetailsDTO orderDTO)
        {
            return View(orderDTO);
        }

        [HttpPost]
        public IActionResult Order(OrderDetailsDTO orderDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            var result = _shoppingCartService.order(userId, orderDTO.Address, orderDTO.Phone, orderDTO.TotalPrice);
            return RedirectToAction("SuccessOrder");
        }

        public bool OrderOnlinePayment(string address, string phone, decimal totalPrice)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            return _shoppingCartService.order(userId, address, phone, totalPrice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult PayOrder(string stripeEmail, string stripeToken, string address, string phone, decimal TotalPrice)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            var customerService = new CustomerService();
            var chargeService = new ChargeService();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            var cart = _shoppingCartService.getShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(cart.TotalPrice * 100),
                Description = "FoodDelivery Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                this.OrderOnlinePayment(address, phone, TotalPrice);
                return RedirectToAction("SuccessPayment");
            }
            else
            {
                return RedirectToAction("NotsuccessPayment");
            }
        }

        public IActionResult SuccessPayment()
        {
            return View();
        }

        public IActionResult NotsuccessPayment()
        {
            return View();
        }

        public IActionResult SuccessOrder()
        {
            return View();
        }
    }
}
