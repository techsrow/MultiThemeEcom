﻿using BasePackageModule2.Data;
using BasePackageModule2.Models;
using BasePackageModule2.Models.Menu;
using BasePackageModule2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.ViewComponents
{
    [ViewComponent(Name = "NavigationHome")]
    public class NavigationHome : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public NavigationHome(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var logo = await _context.Logos.AnyAsync()
                ? await _context.Logos.FirstOrDefaultAsync()
                : new Logo();


            var businessProfile = await _context.BusinessProfile.AnyAsync()
                ? await _context.BusinessProfile.FirstOrDefaultAsync()
                : new BusinessProfile();


            List<Page> page = await _context.Pages.OrderBy(o => o.Order).ToListAsync();
            var categories = await _context.Categories.Include(s => s.SubCategories).OrderByDescending(k => k.SubCategories.Count).ToListAsync();
            var products = await _context.Products.ToListAsync();

            List<Menu> menus = await _context.Menus.Include(m => m.MenuProducts).ThenInclude(p => p.Product).Include(c => c.MenuCategories).ThenInclude(c => c.Category).OrderBy(o => o.Order).ToListAsync();

            var images = await _context.SliderImages.ToListAsync();
            NavViewModel model = new NavViewModel
            {
                Menus = menus,
                Logo = logo,
                More = page,
                Categories = categories,
                BusinessProfile = businessProfile,
                Products = products,
                SliderImages = images
            };

            return View("Index", model);
        }
    }
}
