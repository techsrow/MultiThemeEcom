using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasePackageModule2.Data;
using BasePackageModule2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TomBase.Areas.Identity.Pages.Account.Manage
{
    public class OrdersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrdersModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Order> Orders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Orders = await _context.Orders
               .Include(p => p.OrderProducts)
               .ThenInclude(p => p.Product)
               .ThenInclude(i => i.Images)
               //.Where(p => p.PaymentStatus.Any())
               .Where(o => o.UserId == user.Id)
               .OrderByDescending(a => a.Id).ToListAsync();

            

            return Page();
        }
        public async Task<ActionResult> OnGetDeleteAsync(int? id)
        {
            if (id != null)
            {
                var add = await _context.Orders.FindAsync(id);

                _context.Remove(add);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("Orders");

        }
    }
}
