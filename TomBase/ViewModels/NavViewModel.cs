using System.Collections.Generic;
using BasePackageModule2.Models;
using BasePackageModule2.Models.Menu;
using TomBase.Models;

namespace BasePackageModule2.ViewModels
{
    public class NavViewModel
    {
        public List<SliderImage> SliderImages { get; set; }
        public List<Page> More { get; set; }
        public Logo Logo { get; set; }
        public Coupon Coupon { get; set; }

        public List<ThemeSetting> ThemeSetting { get; set; }

        public BusinessProfile BusinessProfile { get; set; }

        public List<Category> Categories { get; set; }

        public List<Product> Products { get; set; }

        public List<Menu> Menus { get; set; }

        public List<Cart> Carts { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
