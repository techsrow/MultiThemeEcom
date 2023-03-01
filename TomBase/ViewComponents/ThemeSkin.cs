using BasePackageModule2.Data;
using BasePackageModule2.Models;
using BasePackageModule2.Models.Menu;
using BasePackageModule2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.ViewComponents
{
    [ViewComponent(Name = "ThemeSkin")]
    public class ThemeSkin : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ThemeSkin(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
          

            var themesetting = await _context.ThemeSettings.ToListAsync();
           
            NavViewModel model = new NavViewModel
            {
               
                ThemeSetting = themesetting

            };

            return View("Index", model);
        }
    }
}
