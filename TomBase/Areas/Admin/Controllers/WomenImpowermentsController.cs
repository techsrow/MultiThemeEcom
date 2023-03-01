using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasePackageModule2.Data;
using TomBase.Models;

namespace TomBase.Areas.TomBase.Controllers
{
    [Area("Admin")]
    public class WomenImpowermentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WomenImpowermentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TomBase/WomenImpowerments
        public async Task<IActionResult> Index()
        {
            return View(await _context.WomenImpowerments.ToListAsync());
        }

        // GET: TomBase/WomenImpowerments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var womenImpowerment = await _context.WomenImpowerments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (womenImpowerment == null)
            {
                return NotFound();
            }

            return View(womenImpowerment);
        }

        // GET: TomBase/WomenImpowerments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TomBase/WomenImpowerments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Description")] WomenImpowerment womenImpowerment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(womenImpowerment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(womenImpowerment);
        }

        // GET: TomBase/WomenImpowerments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var womenImpowerment = await _context.WomenImpowerments.FindAsync(id);
            if (womenImpowerment == null)
            {
                return NotFound();
            }
            return View(womenImpowerment);
        }

        // POST: TomBase/WomenImpowerments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Description")] WomenImpowerment womenImpowerment)
        {
            if (id != womenImpowerment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(womenImpowerment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WomenImpowermentExists(womenImpowerment.Id))
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
            return View(womenImpowerment);
        }

        // GET: TomBase/WomenImpowerments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var womenImpowerment = await _context.WomenImpowerments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (womenImpowerment == null)
            {
                return NotFound();
            }

            return View(womenImpowerment);
        }

        // POST: TomBase/WomenImpowerments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var womenImpowerment = await _context.WomenImpowerments.FindAsync(id);
            _context.WomenImpowerments.Remove(womenImpowerment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WomenImpowermentExists(int id)
        {
            return _context.WomenImpowerments.Any(e => e.Id == id);
        }
    }
}
