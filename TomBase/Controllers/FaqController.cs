using BasePackageModule2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomBase.Controllers
{
    public class FaqController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FaqController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View( await _context.Faqs.ToListAsync());
        }
    }
}
