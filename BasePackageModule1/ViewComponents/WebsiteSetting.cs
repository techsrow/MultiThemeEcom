using BasePackageModule1.Data;
using BasePackageModule1.Models;
using BasePackageModule1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasePackageModule1.ViewComponents
{
    [ViewComponent(Name = "WebsiteSetting")]
    public class WebsiteSetting : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public WebsiteSetting(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var customcss = new CustomCss();

            if (_context.CustomCsses.Any())
            {
                customcss = await _context.CustomCsses.FirstOrDefaultAsync();
            }


            NavViewModel model = new NavViewModel
            {



                CustomCss = customcss,


            };

            return View("Index", model);
        }
    }
}
