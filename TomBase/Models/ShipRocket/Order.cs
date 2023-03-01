using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Models.ShipRocket
{

    public class ShiprocketOrderItem
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string sku { get; set; }
        [Required]
        public int units { get; set; }
        [Required]
        public double selling_price { get; set; }
        public int discount { get; set; }
        public int tax { get; set; }
        public int hsn { get; set; }
    }

    public class ShiprocketOrder
    {

        [Required]
        [Display(Name = "Order ID")]
        public string order_id { get; set; }
        [Required]
        [Display(Name = "Order Date")]
        public DateTime order_date { get; set; }
        [Required]
        [Display(Name = "Pickup Location")]
        public string pickup_location { get; set; }
        [Display(Name = "Channel ID ")]
        public int channel_id { get; set; }

        [Display(Name = "Comment ")] 
        public string comment { get; set; }
        [Required]
        [Display(Name = "Billing Customer Name ")] 
        public string billing_customer_name { get; set; }
        [Display(Name = "Billing Last Name")]
        public string billing_last_name { get; set; }

        [Display(Name = "Billing Address ")] 
        public string billing_address { get; set; }
        [Required]
        [Display(Name = "Billing City")]
        public string billing_city { get; set; }
        [Required]
        [Display(Name = "Billing Pincode ")]
        public int billing_pincode { get; set; }
        [Required]
        [Display(Name = "Billing State")]
        public string billing_state { get; set; }
        [Required]
        [Display(Name = "Billing Country")]
        public string billing_country { get; set; }
        [Required]
        [Display(Name = "Billing Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string billing_email { get; set; }
        [Required]
        [Display(Name = "Billing Phone")]
        public string billing_phone { get; set; }
        [Display(Name = "Billing Alternate Phone")]
        public Int64 billing_alternate_phone { get; set; }
        [Required]
        [Display(Name = "Shipping is Billing")]
        public bool shipping_is_billing { get; set; }
        [Required]
        [Display(Name = "Shipping  Customer Name")]
        public string shipping_customer_name { get; set; }
        [Display(Name = "Shipping Last Name")]
        public string shipping_last_name { get; set; }
        [Required]
        [Display(Name = "Shipping Address")]
        public string shipping_address { get; set; }
        [Display(Name = "Shipping Address 2")]
        public string shipping_address_2 { get; set; }
        [Required]
        [Display(Name = "Shipping City")]
        public string shipping_city { get; set; }
        [Required]
        [Display(Name = "Shipping Pincode")]
        public int shipping_pincode { get; set; }
        [Required]
        [Display(Name = "Shipping Country")]
        public string shipping_country { get; set; }
        [Required]
        [Display(Name = "Shipping State")]
        public string shipping_state { get; set; }
        [Required]
        [Display(Name = "Shipping Email")]
        public string shipping_email { get; set; }
        [Required]
        [Display(Name = "Shipping Phone")]
        public Int64 shipping_phone { get; set; }
        [Required]
        public List<ShiprocketOrderItem> order_items { get; set; }
        [Required]
        [Display(Name = "Payment Method")]
        public string payment_method { get; set; }

        [Display(Name = "Shipping Charges")] 
        public int shipping_charges { get; set; }
        [Display(Name = "Giftwrap Charges")] 
        public int giftwrap_charges { get; set; }
        [Display(Name = "Transaction Charges")]
        public int transaction_charges { get; set; }
        [Display(Name = "Total Discount")] 
        public int total_discount { get; set; }
        [Required]
        [Display(Name = "Sub Total")]
        public double sub_total { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 1")]
        [Display(Name = "Length")]
        public int length { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 1")]
        [Display(Name = "Breadth")]
        public int breadth { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 1")]
        [Display(Name = "Height")] 
        public int height { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 1")]
        [Display(Name = "Weight")] 
        public double weight { get; set; }
    }




}
