using BasePackageModule2.Models;
using System.Collections.Generic;

namespace BasePackageModule2.Areas.Admin.ViewModels
{
    public class AdminViewModel
    {
        public int Posts { get; set; }
        public int Messages { get; set; }
        public int Products { get; set; }
        public int Categories { get; set; }

        public int Users { get; set; }



        public int Order { get; set; }
        

        public BusinessProfile BusinessProfile { get; set; }

     
    }
}