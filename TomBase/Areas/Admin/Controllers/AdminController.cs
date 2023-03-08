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
            DateTime currentDate = DateTime.Now;
            DateTime CurrentDay = DateTime.Now;
           
            int currentMonth = currentDate.Month;
            int cuurentDayOrder = CurrentDay.Day;

            double result = orders.Where(d => d.Date.Month == currentMonth).Sum(s => s.Amount);

            double result1 = orders.OrderBy(s=>s.Date.Day == cuurentDayOrder).Sum(s => s.Amount);



            ViewBag.OrderMyMonth = result;

            ViewBag.OrderByDay = result1;

            var model = new AdminViewModel
            {
                Posts = await _context.Posts.CountAsync(),

                Categories = await _context.Categories.CountAsync(),
                Products = await _context.Products.CountAsync(),
                Order = await _context.Orders.CountAsync(),
                Users = await _context.Users.CountAsync()
                
                



                


            };

            return View(model);
        }


        
    }

}