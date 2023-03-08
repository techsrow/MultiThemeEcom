using BasePackageModule2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomBase.Models;
using TomBase.ViewModels;

namespace TomBase.ViewComponents
{
    [ViewComponent(Name = "PageBanner")]
    public class PageBanner : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public PageBanner(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerimage = await _context.AllPageHeaders.AnyAsync()
               ? await _context.AllPageHeaders.FirstOrDefaultAsync()
               : new AllPageHeader();
 
            TitleHeaderViewModel model = new TitleHeaderViewModel
            {
                _allPageHeader = headerimage
            };

            return View("Index", model);
        }

    }
}
