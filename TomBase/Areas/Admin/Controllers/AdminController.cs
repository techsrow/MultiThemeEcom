using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasePackageModule2.Areas.Admin.ViewModels;
using BasePackageModule2.Data;
using BasePackageModule2.Models;
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
            List<Order> orders = _context.Orders.ToList();

            var  ordersByMonth = from order in _context.Orders
                                group order by new { order.Date.Year, order.Date.Month } into monthGroup
                                select new 
                                {
                                    Month = new DateTime(monthGroup.Key.Year, monthGroup.Key.Month, 1),
                                    TotalOrders = monthGroup.Sum(o => o.Amount)
                                };


            ViewBag.OrderMyMonth = ordersByMonth;

            var model = new AdminViewModel
            {
                Posts = await _context.Posts.CountAsync(),

                Categories = await _context.Categories.CountAsync(),
                Products = await _context.Products.CountAsync(),
                Order = await _context.Orders.CountAsync(),
                
                



                


            };

            return View(model);
        }


        
    }

}