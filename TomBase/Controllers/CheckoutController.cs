using BasePackageModule2.Data;
using BasePackageModule2.Extensions;
using BasePackageModule2.Models;
using InstaSharp.Exceptions;
using InstaSharp.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Razorpay.Api;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TomBase.Enums;
using TomBase.Utility;
using TomBase.Models.Razorpay;
using TomBase.Helpers;
using Microsoft.AspNetCore.Hosting;
using BasePackageModule2.Helpers;
using Microsoft.Extensions.Configuration;
using BasePackageModule2.Utility;
using Microsoft.Extensions.Logging;

namespace BasePackageModule1.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmailService _emailService;

        private readonly IConfiguration configuration;
        

        public CheckoutController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IEmailService emailService, IHostingEnvironment hostingEnvironment, IConfiguration Configuration)
        {
            _userManager = userManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            configuration = Configuration;
            
            _emailService = emailService;
        }



        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var addresses = await _context.Addresses.Where(u => u.UserId == user.Id).ToListAsync();

            ViewData["Addresses"] = addresses;

            return View();
        }

        public async Task<IActionResult> Address(Address address)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), address);
            }
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            address.UserId = user.Id;
            _context.Add(address);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(PaymentOptions), new { addressId = address.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Complete(string rzp_paymentid, string rzp_orderid, string signature)
        {
            if (CompareSignatures(rzp_orderid, rzp_paymentid, signature))
            {
                var order = await _context.Orders
                                    .Include(x => x.User)
                                    .Include(x => x.Address)
                                    .Include(x => x.OrderProducts)
                                          .ThenInclude(o => o.Product)
                                    .Where(o => o.RazerpayOrderId == rzp_orderid)
                                    .FirstOrDefaultAsync();

                if (order != null)
                {
                    order.PaymentId = rzp_paymentid;
                    order.PaymentStatus = "Credit";
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                    // Create these action method

                }
                 return RedirectToAction(nameof(ThankYou));
                
            }
            else
            {
                return View("PaymentFailed");
            }
        }

        public async Task<IActionResult> ProcessPayment(string? payment_id, string? payment_status, string? id, string? transaction_id)
        {
            if (transaction_id == null)
            {
                return View("PaymentFailed").WithError("Payment Failed", null);
            }

            if (payment_status == "Failed")
            {
                BasePackageModule2.Models.Order order = await _context.Orders.Where(o => o.TransactionId == transaction_id).FirstOrDefaultAsync();

                if (order == null)
                {
                    return BadRequest();
                }

                order.PaymentId = payment_id;
                order.PaymentStatus = payment_status;

                _context.Update(order);
                await _context.SaveChangesAsync();

                return View("PaymentFailed").WithError("Payment Failed", null);
            }

            //PaymentOrderDetailsResponse objPaymentRequestDetailsResponse = objClass.GetPaymentOrderDetailsByTransactionId(transaction_id);
            PaymentOrderDetailsResponse objPaymentRequestDetailsResponse = null;
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

                var carts = await _context.Carts.Where(u => u.UserId == user.Id).ToListAsync();
                _context.RemoveRange(carts);
                await _context.SaveChangesAsync();

                if (objPaymentRequestDetailsResponse != null)
                {
                    BasePackageModule2.Models.Order order = await _context.Orders.Where(o => o.TransactionId == transaction_id).FirstOrDefaultAsync();

                    if (order == null)
                    {
                        return BadRequest();
                    }

                    order.PaymentId = payment_id;
                    order.PaymentStatus = payment_status;

                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(ThankYou)).WithSuccess("Your Order has been placed.", null);
        }

        public async Task<IActionResult> CheckOutOptions()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            double subtotal = 0;

            foreach (var item in await _context.Carts.Include(a => a.Product).Where(u => u.UserId == user.Id).ToListAsync())
            {
                if (item.Product.FinalPrice != null) subtotal += (double)item.Product.FinalPrice * item.Qty;
            }
            ViewData["Items"] = subtotal;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOutOptions(string PaymentMode)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            double subtotal = 0;

            foreach (var item in await _context.Carts.Include(a => a.Product).Where(u => u.UserId == user.Id).ToListAsync())
            {
                if (item.Product.FinalPrice != null) subtotal += (double)item.Product.FinalPrice * item.Qty;
            }


            ViewData["Items"] = subtotal;

            if (PaymentMode == null)
            {
                ModelState.AddModelError("PaymentMode", "Please Select Payment Mode.");
                return View();
            }

            if (PaymentMode == PaymentOptionsType.payOnline.ToString())
            {
                if (!await _context.Carts.AnyAsync(u => u.UserId == user.Id))
                {
                    return RedirectToAction("Index", "Cart");
                }

                var transactionId = Guid.NewGuid().ToString();

                #region 1. Create Payment Order

                var order = new BasePackageModule2.Models.Order
                {
                    UserId = user.Id,
                    PaymentStatus = "Pending",
                    TransactionId = transactionId,
                    Amount = subtotal,
                };
                _context.Add(order);
                await _context.SaveChangesAsync();

                foreach (var cart in await _context.Carts.Include(p => p.Product).Where(u => u.UserId == user.Id).ToListAsync())
                {
                    OrderProduct orderProduct = new OrderProduct
                    {
                        ProductId = cart.Product.Id,
                        OrderId = order.Id,
                        Quantity = cart.Qty
                    };
                    _context.Add(orderProduct);
                }

                await _context.SaveChangesAsync();

                var orderModel = RazorPayProcessor.Processpayment(user, subtotal, transactionId, _context, configuration);
                return View("PaymentPage", orderModel);

                #endregion 1. Create Payment Order
            }
            else if (PaymentMode == PaymentOptionsType.payAtShop.ToString())
            {
                var order = new BasePackageModule2.Models.Order
                {
                    UserId = user.Id,
                    AddressId = null,
                    PaymentStatus = "Pay At Shop",
                    //TransactionId = transactionId,
                    Amount = subtotal,
                };
                _context.Add(order);
                await _context.SaveChangesAsync();

                foreach (var cart in await _context.Carts.Include(p => p.Product).Where(u => u.UserId == user.Id).ToListAsync())
                {
                    OrderProduct orderProduct = new OrderProduct
                    {
                        ProductId = cart.Product.Id,
                        OrderId = order.Id,
                        Quantity = cart.Qty
                    };
                    _context.Add(orderProduct);
                }

                await _context.SaveChangesAsync();

                var carts = await _context.Carts.Where(u => u.UserId == user.Id).ToListAsync();
                _context.RemoveRange(carts);
                await _context.SaveChangesAsync();

                return View(nameof(ThankYou));
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> PaymentOptions(int addressId)
        {
            if (addressId == 0)
            {
                return BadRequest();
            }

            var aPaymentOptions = new APaymentOptions
            {
                AddressId = addressId
            };

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            double subtotal = 0;

            foreach (var item in await _context.Carts.Include(a => a.Product).Where(u => u.UserId == user.Id).ToListAsync())
            {
                if (item.Product.FinalPrice != null) subtotal += (double)item.Product.FinalPrice * item.Qty;
            }
            ViewData["Items"] = subtotal;

            return View(aPaymentOptions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaymentOptions(int AddressId, string PaymentOption)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            double subtotal = 0;

            foreach (var item in await _context.Carts.Include(a => a.Product).Where(u => u.UserId == user.Id).ToListAsync())
            {
                if (item.Product.FinalPrice != null) subtotal += (double)item.Product.FinalPrice * item.Qty;
            }
            if (subtotal < 500)
            {
                subtotal += 50;
            }
            ViewData["Items"] = subtotal;

            if (PaymentOption == null || AddressId == 0)
            {
                ModelState.AddModelError("PaymentOption", "Please Select Payment option.");
                return View();
            }

            var add = await _context.Addresses.FirstOrDefaultAsync(i => i.Id == AddressId);

            if (add == null)
            {
                return BadRequest();
            }

            if (PaymentOption == PaymentOptionsType.payOnline.ToString())
            {
                if (!await _context.Carts.AnyAsync(u => u.UserId == user.Id))
                {
                    return RedirectToAction("Index", "Cart");
                }

                try
                {
                    var transactionId = Guid.NewGuid().ToString();

                    #region 1. Create Payment Order

                    //  Create Payment Order

                    var order = new BasePackageModule2.Models.Order
                    {
                        UserId = user.Id,
                        AddressId = add.Id,
                        PaymentStatus = "Pending",
                        TransactionId = transactionId,
                        Amount = subtotal,
                    };
                    _context.Add(order);
                    await _context.SaveChangesAsync();

                    foreach (var cart in await _context.Carts.Include(p => p.Product).Where(u => u.UserId == user.Id).ToListAsync())
                    {
                        OrderProduct orderProduct = new OrderProduct
                        {
                            ProductId = cart.Product.Id,
                            OrderId = order.Id,
                            Quantity = cart.Qty
                        };
                        _context.Add(orderProduct);
                    }

                    await _context.SaveChangesAsync();

                    var orderModel = RazorPayProcessor.Processpayment(user, subtotal, transactionId, _context, configuration);
                    return View("PaymentPage", orderModel);

                    #endregion 1. Create Payment Order
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else if (PaymentOption == PaymentOptionsType.cashOnDelivery.ToString())
            {
                var transactionId = Guid.NewGuid().ToString();
                var order = new BasePackageModule2.Models.Order
                {
                    UserId = user.Id,
                    AddressId = add.Id,
                    PaymentStatus = "Cash on Delivery",
                    TransactionId = transactionId,
                    Amount = subtotal,
                };
                _context.Add(order);
                await _context.SaveChangesAsync();

                foreach (var cart in await _context.Carts.Include(p => p.Product).Where(u => u.UserId == user.Id).ToListAsync())
                {
                    OrderProduct orderProduct = new OrderProduct
                    {
                        ProductId = cart.Product.Id,
                        OrderId = order.Id,
                        Quantity = cart.Qty
                    };
                    _context.Add(orderProduct);
                }

                await _context.SaveChangesAsync();

                var carts = await _context.Carts.Where(u => u.UserId == user.Id).ToListAsync();
                _context.RemoveRange(carts);
                await _context.SaveChangesAsync();

                return View(nameof(ThankYou));
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<JsonResult> Couponcode(string CouponsCode, double Total)
        {
            bool valid = false;
            var result = await _context.Coupons.Where(x => x.Name == CouponsCode && x.IsActive == true).FirstOrDefaultAsync();
            if (result != null)
            {
                valid = true;
            }
            bool IsCouponsUsed = false;
            var a = SD.discountedPrice(result, Total, out IsCouponsUsed);
            string[] Output = { valid.ToString(), a.ToString() };

            return new JsonResult(Output);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> WebHook()
        {
            try
            {
               
                string json = GetHookbody().Result.ToString();
                var Razorpayhooks = JsonConvert.DeserializeObject<Razorpayhooks>(json);
                var order = await _context.Orders
                                            .Include(x => x.User)
                                            .Include(x => x.Address)
                                            .Include(x => x.OrderProducts)
                                                  .ThenInclude(o => o.Product)
                                            .Where(o => o.RazerpayOrderId == Razorpayhooks.payload.payment.entity.order_id)
                                            .FirstOrDefaultAsync();

                if (order != null)
                {
                    order.PaymentId = Razorpayhooks.payload.payment.entity.id;
                    order.PaymentStatus = "Credit";
                    _context.Update(order);
                    await _context.SaveChangesAsync();

                    try
                    {
                        #region Retailer

                        var EmailOrderManager = new EmailOrderManager();
                        var val = EmailOrderManager.GetOrderTemplate(_hostingEnvironment.WebRootPath, EnumOrdersEmailTemplate.Retailer, order, _hostingEnvironment.WebRootPath, Razorpayhooks.payload.payment.entity.method);
                        var EmailMessage = new EmailService.EmailMessage();
                        EmailMessage.Subject = val.Subject;
                        EmailMessage.Content = val.Content;
                        EmailMessage.FromAddress.Name = _emailService.GetDefaultEmail();
                        EmailMessage.FromAddress.Address = _emailService.GetDefaultEmail();
                        EmailMessage.ToAddress.Address = _emailService.GetDefaultEmail();
                        EmailMessage.ToAddress.Name = _emailService.GetDefaultEmail();
                        _emailService.Send(EmailMessage);

                        #endregion

                        #region Consumer
                        EmailOrderManager = new EmailOrderManager();
                        val = EmailOrderManager.GetOrderTemplate(_hostingEnvironment.WebRootPath, EnumOrdersEmailTemplate.Consumer,
                                                                                               order, _hostingEnvironment.WebRootPath, Razorpayhooks.payload.payment.entity.method);
                        EmailMessage = new EmailService.EmailMessage();
                        EmailMessage.Subject = val.Subject;
                        EmailMessage.Content = val.Content;
                        EmailMessage.FromAddress.Name = "khamkarspices";
                        EmailMessage.FromAddress.Address = _emailService.GetDefaultEmail();
                        EmailMessage.ToAddress.Address = order.User.Email;
                        EmailMessage.ToAddress.Name = order.User.FirstName;
                        _emailService.Send(EmailMessage);

                        #endregion
                    }
                    catch (Exception ex)
                    {
                    }

                }

               

            }
            catch (Exception)
            {

                
            }
            
            return Ok();
        }

        public IActionResult ThankYou()
        {
            return View();
        }


        #region private
        private async Task<string> GetHookbody()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                return await reader.ReadToEndAsync();
            }
        }
        private bool CompareSignatures(string orderId, string paymentId, string razorPaySignature)
        {
            var text = orderId + "|" + paymentId;
            var secret = configuration["Razorpay:secret"];
            var generatedSignature = CalculateSHA256(text, secret);
            if (generatedSignature == razorPaySignature)
                return true;
            else
                return false;
        }
        private string CalculateSHA256(string text, string secret)
        {
            string result = "";
            var enc = Encoding.Default;
            byte[]
            baText2BeHashed = enc.GetBytes(text),
            baSalt = enc.GetBytes(secret);
            System.Security.Cryptography.HMACSHA256 hasher = new HMACSHA256(baSalt);
            byte[] baHashedText = hasher.ComputeHash(baText2BeHashed);
            result = string.Join("", baHashedText.ToList().Select(b => b.ToString("x2")).ToArray());
            return result;
        }
        #endregion private

        public class APaymentOptions
        {
            public int AddressId { get; set; }

            [Required]
            public string PaymentOption { get; set; }
        }

        public class PaymentModes
        {
            [Required]
            public string PaymentMode { get; set; }
        }
    }
}