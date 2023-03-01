using BasePackageModule2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.ViewModels
{
    public class CartViewModel
    {
        public List<Cart> Carts { get; set; }
        public  ApplicationUser User { get; set; }
    }
}
