using BasePackageModule2.Data;
using BasePackageModule2.Models;
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
    [ViewComponent(Name = "ThemeSkinColor")]
    public class ThemeSkinColor : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ThemeSkinColor(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            var themeskincolor = await _context.SkinSettings.FirstOrDefaultAsync();

            NavViewModel model = new NavViewModel
            {

                SkinSetting = themeskincolor

            };

            return View("Index", model);
        }
    }
}