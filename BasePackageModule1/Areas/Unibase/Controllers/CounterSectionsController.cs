using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasePackageModule1.Data;
using BasePackageModule1.Models;

namespace BasePackageModule1.Areas.Unibase.Controllers
{
    [Area("Unibase")]
    public class CounterSectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CounterSectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Unibase/CounterSections
        public async Task<IActionResult> Index()
        {
            return View(await _context.CounterSections.ToListAsync());
        }

        // GET: Unibase/CounterSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var counterSection = await _context.CounterSections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counterSection == null)
            {
                return NotFound();
            }

            return View(counterSection);
        }

        // GET: Unibase/CounterSections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Unibase/CounterSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Heading,Subheading,BtnText,BtnLink,ShortDescription,ContTitle,CountDescription")] CounterSection counterSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(counterSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(counterSection);
        }

        // GET: Unibase/CounterSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var counterSection = await _context.CounterSections.FindAsync(id);
            if (counterSection == null)
            {
                return NotFound();
            }
            return View(counterSection);
        }

        // POST: Unibase/CounterSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Heading,Subheading,BtnText,BtnLink,ShortDescription,ContTitle,CountDescription")] CounterSection counterSection)
        {
            if (id != counterSection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(counterSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CounterSectionExists(counterSection.Id))
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
            return View(counterSection);
        }

        // GET: Unibase/CounterSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var counterSection = await _context.CounterSections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counterSection == null)
            {
                return NotFound();
            }

            return View(counterSection);
        }

        // POST: Unibase/CounterSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var counterSection = await _context.CounterSections.FindAsync(id);
            _context.CounterSections.Remove(counterSection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CounterSectionExists(int id)
        {
            return _context.CounterSections.Any(e => e.Id == id);
        }
    }
}
