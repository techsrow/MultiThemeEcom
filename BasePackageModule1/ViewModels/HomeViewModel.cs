using System.Collections.Generic;
using BasePackageModule1.Models;
using BasePackageModule1.Models.Menu;

namespace BasePackageModule1.ViewModels
{
    public class HomeViewModel
    {
        public List<SliderImage> SliderImages { get; set; }

        public AboutUs _AboutUs { get; set; }

      
        public HomeProgessBar  _HomeProgessBar { get; set; }
        public HomeBussinessGrowth _HomeBusinessGrowth { get; set; }
        public List<Faq> _Faq { get; set; }
        public List<Client> _Clients { get; set; }

        public List<Testimonial> Testimonials { get; set; }

        public CounterSection _CounterSection { get; set; }



        public string Banner { get; set; }
        public List<Post> Updates { get; set; }
        public List<Image> _Image { get; set; }
        public List<Product> _items { get; set; }

        public Post _supdate { get; set; }
        public BusinessProfile BusinessProfile { get; set; }
        public Product _sitem { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
