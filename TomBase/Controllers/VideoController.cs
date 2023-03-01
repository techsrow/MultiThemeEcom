using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasePackageModule2.Data;
//using BasePackageModule2.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TomBase.Controllers
{
    public class VideoController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VideoController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task< IActionResult> Index()
        {
            return View(await _context.Testimonails.ToListAsync());
        }
    }
}
