using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasePackageModule2.Data;
using BasePackageModule2.Models;
using Microsoft.AspNetCore.Authorization;

namespace TomBase.Areas.TomBase.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class TestimonialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestimonialsController(ApplicationDbContext context)
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

        // GET: TomBase/Testimonails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TomBase/Testimonails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Content,Video")] Testimonail testimonail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testimonail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testimonail);
        }

        // GET: TomBase/Testimonails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonail = await _context.Testimonails.FindAsync(id);
            if (testimonail == null)
            {
                return NotFound();
            }
            return View(testimonail);
        }

        // POST: TomBase/Testimonails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Content,Video")] Testimonail testimonail)
        {
            if (id != testimonail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testimonail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonailExists(testimonail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(testimonail);
        }

        // GET: TomBase/Testimonails/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: TomBase/Testimonails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testimonail = await _context.Testimonails.FindAsync(id);
            _context.Testimonails.Remove(testimonail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonailExists(int id)
        {
            return _context.Testimonails.Any(e => e.Id == id);
        }
    }
}
