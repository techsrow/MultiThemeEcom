using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Models.Razorpay
{
    public class RayorPayOrderModel
    {
        public string orderId { get; set; }
        public string razorpayKey { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string transactionId { get; set; }
    }
}
