using BasePackageModule2.Data;
using BasePackageModule2.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TomBase.Models.ShipRocket;

namespace TomBase.Areas.TomBase.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ShipingController : Controller
    {
        #region Private Fields and methods 
        private string ShiprocketURl = "https://apiv2.shiprocket.in/v1/external/";
        private readonly ApplicationDbContext _context;

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task<LoginResponse> ShipRocketLogInData()
        {
            LoginResponse LoginResponse = null;

            LoginResponse = await _context.LoginResponse.Where(a => a.created_at.AddDays(9) >= DateTime.Now).FirstOrDefaultAsync();
            if (LoginResponse == null)
            {

                var TokenList = await _context.LoginResponse.ToListAsync();
                _context.LoginResponse.RemoveRange(TokenList);
                _context.SaveChanges();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ShiprocketURl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Login Login = new Login();
                    //Login.email = "kasif780780@gmail.com";
                    //Login.password = "1234@Asif";
                    Login.email = "techsrow@gmail.com";
                    Login.password = "2023@Asif";

                    //HTTP POST
                    HttpResponseMessage Res = await client.PostAsync("auth/login", CreateHttpContent<Login>(Login));
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        LoginResponse = JsonConvert.DeserializeObject<LoginResponse>(EmpResponse);
                    }
                }
            }
            return LoginResponse;
        }

        private async Task ShiprocketclientpickupAddress(LoginResponse LoginResponse)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ShiprocketURl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginResponse.token);
                //HTTP POST
              HttpResponseMessage Res = await client.GetAsync("settings/company/pickup");
               // HttpResponseMessage Res = await client.GetAsync("settings/company/Primary");
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    ListShippingAddressdata ListShippingAddressdata = JsonConvert.DeserializeObject<ListShippingAddressdata>(Response);
                    ViewBag.ListShippingAddressdata = ListShippingAddressdata;
                }
            }
        }
        #endregion

        private readonly IHostingEnvironment _hostingEnvironment;
        public ShipingController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        //[Authorize]

        public async Task<IActionResult> NewOrder()
        {
            var Orders = await _context.Orders.Include(a => a.User).Include(a => a.Address).Where(a => (a.PaymentStatus == "Credit" || a.PaymentStatus == "Cash on Delivery") && a.AddedToShiprocket == false).OrderByDescending(i=>i.Id).ToListAsync();
            LoginResponse LoginResponse = await ShipRocketLogInData();

            return View(Orders);
        }

        [HttpGet]
        public async Task<IActionResult> ShipRocketOrder(int id)
        {
            if (id == 0) return BadRequest("Enter Proper Order ID");

            var Orders = await _context.Orders.Include(o => o.OrderProducts).ThenInclude(o => o.Product)
                                            .Include(o => o.Address).ThenInclude(o => o.User)
                                            .Where(a => a.Id == id).FirstOrDefaultAsync();
            LoginResponse LoginResponse = await ShipRocketLogInData();

            await ShiprocketclientpickupAddress(LoginResponse);
            if (Orders.AddressId > 0)
            {
                Orders.Address = await _context.Addresses.Where(x => x.Id == Orders.AddressId).FirstOrDefaultAsync();

            }

            List<ShiprocketOrderItem> ListShiprocketOrderItem = new List<ShiprocketOrderItem>();
            ShiprocketOrderItem ShiprocketOrderItem = new ShiprocketOrderItem();
            double subtotal = 0;
            foreach (var item in Orders.OrderProducts)
            {
                ShiprocketOrderItem = new ShiprocketOrderItem();
                ShiprocketOrderItem.name = item.Product.Name;
                ShiprocketOrderItem.sku = item.Product.sku;
                ShiprocketOrderItem.units = item.Quantity;
                ShiprocketOrderItem.selling_price = Convert.ToDouble( item.Product.FinalPrice);
                subtotal = item.Order.Amount;
                ListShiprocketOrderItem.Add(ShiprocketOrderItem);
            }

            ShiprocketOrder ShiprocketOrder = new ShiprocketOrder();
            ShiprocketOrder.order_id = Convert.ToString(Orders.Id);
            ShiprocketOrder.sub_total = subtotal;
            ShiprocketOrder.order_date = Orders.Date;
            ShiprocketOrder.billing_address = Orders.Address.MainAddress;
            ShiprocketOrder.shipping_address = Orders.Address.MainAddress;
            ShiprocketOrder.billing_city = Orders.Address.City;
            ShiprocketOrder.shipping_city = Orders.Address.City;
            ShiprocketOrder.billing_pincode = Orders.Address.PinCode;
            ShiprocketOrder.billing_phone = Orders.Address.MobileNumber;
            ShiprocketOrder.shipping_phone = Convert.ToInt64(Orders.Address.MobileNumber);
            ShiprocketOrder.billing_alternate_phone = Convert.ToInt64(Orders.Address.MobileNumber);
            ShiprocketOrder.order_items = ListShiprocketOrderItem;
            ShiprocketOrder.billing_customer_name = Orders.Address.Name;
            ShiprocketOrder.billing_last_name = Orders.Address.Name;
            ShiprocketOrder.billing_email = Orders.Address.User.Email;
            ShiprocketOrder.billing_state = Orders.Address.State;
            ShiprocketOrder.billing_country = "India";
            ShiprocketOrder.shipping_customer_name = Orders.Address.Name;
            ShiprocketOrder.shipping_last_name = Orders.Address.Name;
            ShiprocketOrder.shipping_email = Orders.Address.User.Email;
            ShiprocketOrder.shipping_country = "India";
            ShiprocketOrder.shipping_state = Orders.Address.State;
            ShiprocketOrder.shipping_pincode = Orders.Address.PinCode;


            ViewBag.Shiprocketerror = "";
            return View(ShiprocketOrder);
        }

        [HttpPost]
        public async Task<IActionResult> ShipRocketOrder(ShiprocketOrder ShiprocketOrder)
        {
            LoginResponse LoginResponse = await ShipRocketLogInData();
            if (ModelState.IsValid)
            {
                string token = LoginResponse.token;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ShiprocketURl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginResponse.token);
                    //HTTP POST
                    HttpResponseMessage Res = await client.PostAsync("orders/create/adhoc", CreateHttpContent<ShiprocketOrder>(ShiprocketOrder));
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        ListShippingAddressdata ListShippingAddressdata = JsonConvert.DeserializeObject<ListShippingAddressdata>(Response);
                        ViewBag.ListShippingAddressdata = ListShippingAddressdata;
                        var Orders = await _context.Orders.Where(X => X.Id == Convert.ToInt64(ShiprocketOrder.order_id)).FirstOrDefaultAsync();

                        Orders.AddedToShiprocket = true;
                        _context.Update(Orders);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("NewOrder");
                    }
                    else
                    {
                        string result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<dynamic>(Res.Content.ReadAsStringAsync().Result));
                        await ShiprocketclientpickupAddress(LoginResponse);
                        ViewBag.Shiprocketerror = result;
                        return View(ShiprocketOrder).WithError("Error occur", result);
                    }
                }
            }
            await ShiprocketclientpickupAddress(LoginResponse);

            return View(ShiprocketOrder);
        }


        public async Task<IActionResult> OrderHistory()
        {
            var Orders = await _context.Orders.Include(a => a.User).Include(a => a.Address).Where(a => a.AddedToShiprocket == true).ToListAsync();
            return View(Orders);
        }

        public async Task<IActionResult> OrderInfo(int id)
        {
            if (id == 0) return BadRequest();
            LoginResponse LoginResponse = await ShipRocketLogInData();
            ShipRocketTracking[] ShipRocketTracking = null;
            ShipRocketTracking ShipRocketTrackingData = new ShipRocketTracking();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ShiprocketURl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginResponse.token);
                //HTTP POST
                HttpResponseMessage Res = await client.GetAsync("courier/track?order_id=" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    FileStream fileStream = null;
                    Response = ReadFile(_hostingEnvironment.WebRootPath + @"\EmailTemplate\dummy.json", out fileStream, out Response);
                    ShipRocketTracking = JsonConvert.DeserializeObject<ShipRocketTracking[]>(Response);
                }

            }
            ViewBag.orderid = id;
            ShipRocketTrackingData = ShipRocketTracking[0];
            var Orders = await _context.Orders.Include(o => o.OrderProducts).ThenInclude(o => o.Product)
                                            .Include(o => o.Address).ThenInclude(o => o.User)
                                            .Where(a => a.Id == id).FirstOrDefaultAsync();
            OrderInfoData OrderInfoData = new OrderInfoData();
            OrderInfoData.order = Orders;
            OrderInfoData.ShipRocketTracking = ShipRocketTrackingData;

            return View(OrderInfoData);
        }
        private string ReadFile(string Url, out FileStream fileStream, out string json)
        {
            fileStream = new FileStream(Url, FileMode.Open);
            json = null;
            using (StreamReader sr = new StreamReader(fileStream))
            {
                string line;
                //read the line by line and print each line
                while ((line = sr.ReadLine()) != null)
                {
                    json += line;
                }

            }
            return json;
        }
    }
}
