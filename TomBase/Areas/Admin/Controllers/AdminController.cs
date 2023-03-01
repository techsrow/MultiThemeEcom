using System.Linq;
using System.Threading.Tasks;
using BasePackageModule2.Areas.Admin.ViewModels;
using BasePackageModule2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasePackageModule2.Areas.TomBase.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new AdminViewModel
            {
                Posts = await _context.Posts.CountAsync(),

                Categories = await _context.Categories.CountAsync(),
                Products = await _context.Products.CountAsync(),
                Order = await _context.Orders.CountAsync()
            };

            return View(model);
        }


        
    }

}