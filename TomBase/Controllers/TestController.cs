using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using TomBase.Models.ShipRocket;
using Newtonsoft.Json.Linq;


namespace TomBase.Controllers
{



    public class TestController : Controller
    {
        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public async Task<IActionResult> IndexAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://apiv2.shiprocket.in/v1/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Login Login = new Login();
                Login.email = "kasif780780@gmail.com";
                Login.password = "1234@Asif";
                LoginResponse LoginResponse = new LoginResponse();
                //HTTP POST
                HttpResponseMessage Res = await client.PostAsync("external/auth/login", CreateHttpContent<Login>(Login));
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    LoginResponse = JsonConvert.DeserializeObject<LoginResponse>(EmpResponse);
                }

                string token = LoginResponse.token;

                var Json = new ShiprocketOrder();

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var a = new StringContent(JsonConvert.SerializeObject(Json), Encoding.UTF8, "application/json");
                var postTask1 = await client.PostAsync("external/orders/create/adhoc", a);
                dynamic data1 = await postTask1.Content.ReadAsStringAsync();




            }



            return View();
        }
    }
}
