using BasePackageModule2.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomBase.Models;

namespace TomBase.Helpers
{
    public class EmailTemplateData
    {
        public string Subject { get; set; }
        public string Content { get; set; }
    }

    public enum EnumContactUsEmailTemplate
    {
        ContactUs = 1,
        ContactUsRetail = 2,
        ContactUsWholesale = 3
    }
    public enum EnumOrdersEmailTemplate
    {
        Consumer = 1,
        Retailer = 2,
    }

}
