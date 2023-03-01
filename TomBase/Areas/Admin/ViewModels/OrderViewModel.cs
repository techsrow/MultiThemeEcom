using BasePackageModule2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Areas.Admin.ViewModels
{
    public class OrderViewModel
    {
        public List<Order> _OrderCredit { get; set; }
        public List<Order> _OrderFailed { get; set; }

        public List<Order> _OrderPending { get; set; }

        
    }
}
