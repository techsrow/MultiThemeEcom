using System.Collections.Generic;
using BasePackageModule2.Models;
using BasePackageModule2.Models.Menu;
using TomBase.Models;

namespace BasePackageModule2.ViewModels
{
    public class HomeViewModel
    {
        public List<SliderImage> SliderImages { get; set; }

        public AboutUs _AboutUs { get; set; }

        public List<Banner> _banner { get; set; }
        public List<ThemeSetting> _themeSettings { get; set; }


        public string Banner { get; set; }
        public List<Post> Updates { get; set; }
        public List<Image> _Image { get; set; }
        public List<Product> _items { get; set; }
        public List<Product> _dealofDay { get; set; }
        public List<Product> _youmayLike { get; set; }
        public List<Product> _newArrival { get; set; }
        public List<Product> _bestSaller { get; set; }
        public List<Product> _blended { get; set; }
        public List<Product> _topSalling { get; set; }
        public List<Testimonail> _feedback { get; set; }



        public List<Product> _bestProduct { get; set; }

        public Post _supdate { get; set; }
        public BusinessProfile BusinessProfile { get; set; }
        public Product _sitem { get; set; }
        public List<Category> _Category { get; set; }
        public List<Menu> Menus { get; set; }
        public List<List<Category>> _ChunkCategory { get; set; }
        public List<List<Product>> _ChunkProduct { get; set; }
        public List<List<Product>> _ChunkBest { get; set; }

        public List<Product> _hotselling { get; set; }
        public List<Product> _essential { get; set; }
        public List<Product> _premium { get; set; }
        public List<Product> _traditional { get; set; }
        public List<Product> _flours { get; set; }
        public List<Product> _chutney { get; set; }



        public ContactUs ContactUs { get; set; }
    }
}
