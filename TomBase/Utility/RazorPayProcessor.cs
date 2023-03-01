using BasePackageModule2.Data;
using BasePackageModule2.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using TomBase.Models.Razorpay;
namespace TomBase.Utility
{
    public  static class RazorPayProcessor
    {

        public static RayorPayOrderModel Processpayment(ApplicationUser user, double amount, string transaction_id , ApplicationDbContext context , IConfiguration configuration)
        {

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(configuration["Razorpay:key"], configuration["Razorpay:secret"]);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", amount * 100);  // Amount will in paise
            options.Add("receipt", transaction_id);
            options.Add("currency", "INR");
            options.Add("payment_capture", "1"); // 1 - automatic  , 0 - manual 
 
            Razorpay.Api.Order orderResponse = client.Order.Create(options);

          var value =   orderResponse["id"].ToString();
            var Orders = context.Orders.Where(a => a.TransactionId == transaction_id).FirstOrDefault();
            Orders.RazerpayOrderId = value;
            context.Update(Orders);
            context.SaveChanges();

            // Create order model for return on view
            RayorPayOrderModel RayorPayOrderModel = new RayorPayOrderModel
            {
                orderId = orderResponse.Attributes["id"],
                razorpayKey = configuration["Razorpay:key"],
                amount = amount * 100,
                currency = "INR",
                name = $"{user.FirstName } {user.LastName}",
                email = user.Email,
                contactNumber = user.PhoneNumber,
                address = user.Addresses.FirstOrDefault().MainAddress,
                description = "",
                transactionId = transaction_id

            };
            return RayorPayOrderModel;
        }


    }
}
