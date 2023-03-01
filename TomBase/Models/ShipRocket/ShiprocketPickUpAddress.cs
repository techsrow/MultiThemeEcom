using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Models.ShipRocket
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ShippingAddress
    {
        public int id { get; set; }
        public string pickup_location { get; set; }
        public object address_type { get; set; }
        public string address { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pin_code { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public int company_id { get; set; }
        public int status { get; set; }
        public int phone_verified { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }
        public object warehouse_code { get; set; }
        public object alternate_phone { get; set; }
        public int lat_long_status { get; set; }
        public int @new { get; set; }
    }

    public class ListShippingAddress
    {
        public List<ShippingAddress> shipping_address { get; set; }
        public string allow_more { get; set; }
        public bool is_blackbox_seller { get; set; }
        public string company_name { get; set; }
    }
    public class ListShippingAddressdata
    {
        public ListShippingAddress data { get; set; }
    }



}
