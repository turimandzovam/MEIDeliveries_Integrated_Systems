using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.DTO
{
    public class OrderPaymentDTO
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal TotalPrice { get; set; }
    
    }
}
