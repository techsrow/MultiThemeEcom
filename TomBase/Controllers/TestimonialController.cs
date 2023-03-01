using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasePackageModule2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TomBase.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TestimonialController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: TomBase/Testimonails
        public async Task<IActionResult> Index()
        {
            return View(await _context.Testimonails.ToListAsync());
        }

        // GET: TomBase/Testimonails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonail = await _context.Testimonails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonail == null)
            {
                return NotFound();
            }

            return View(testimonail);
        }
    }
}
