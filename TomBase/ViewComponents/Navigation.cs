﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BasePackageModule2.Data;
using BasePackageModule2.Models;
using BasePackageModule2.Models.Menu;
using BasePackageModule2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasePackageModule2.ViewComponents
{
    [ViewComponent(Name = "Navigation")]
    public class Navigation :ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Navigation(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var logo = await _context.Logos.AnyAsync()
                ? await _context.Logos.FirstOrDefaultAsync()
                : new Logo();

            var coupon = await _context.Coupons.AnyAsync()
                ? await _context.Coupons.FirstOrDefaultAsync()
                : new Coupon();

            var themesetting = await _context.ThemeSettings.ToListAsync();
            var businessProfile = await _context.BusinessProfile.AnyAsync()
                ? await _context.BusinessProfile.FirstOrDefaultAsync()
                : new BusinessProfile();

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            List<Page> page = await _context.Pages.OrderBy(o => o.Order).ToListAsync();
           var cart = _context.Carts.ToList();

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
                SliderImages = images,
                Coupon = coupon,
                Carts = cart,
                User = user,
                ThemeSetting = themesetting

            };

            return View("Index", model);
        }
    }
}
