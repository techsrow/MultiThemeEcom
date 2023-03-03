using BasePackageModule2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Models
{
    public class DealOfTheDay: Product
    {
        
        public string ImagePath { get; set; }

        public string OfferDateStart { get; set; }

        public string OfferDateEnd { get; set; }

    }
}
