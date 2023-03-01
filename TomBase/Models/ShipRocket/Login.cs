using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Models.ShipRocket
{
    public class Login
    {
        public string email { get; set; }
        public string password { get; set; }
    }



    public class LoginResponse
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public int company_id { get; set; }
        public DateTime created_at { get; set; }
        public string token { get; set; }
    }


}
