﻿using System.Collections.Generic;
using BasePackageModule2.Models;
using BasePackageModule2.Models.Menu;

namespace BasePackageModule2.ViewModels
{
    public class HomeViewModel
    {
        public List<SliderImage> SliderImages { get; set; }

        public AboutUs _AboutUs { get; set; }
      
        public string Banner { get; set; }
        public List<Post> Updates { get; set; }
        public List<Image> _Image { get; set; }
        public List<Product> _items { get; set; }
        public List<List<Category>> _ChunkCategory { get; set; }
        public List<List<Product>> _ChunkNewProduct { get; set; }
        public List<List<Product>> _ChunkBestSellar { get; set; }
        public Post _supdate { get; set; }
        public BusinessProfile BusinessProfile { get; set; }
        public Product _sitem { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
