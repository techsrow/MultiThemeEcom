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
    public class HomeBussinessGrowthsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeBussinessGrowthsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Unibase/HomeBussinessGrowths
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeBussinessGrowths.ToListAsync());
        }

        // GET: Unibase/HomeBussinessGrowths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeBussinessGrowth = await _context.HomeBussinessGrowths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeBussinessGrowth == null)
            {
                return NotFound();
            }

            return View(homeBussinessGrowth);
        }

        // GET: Unibase/HomeBussinessGrowths/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Unibase/HomeBussinessGrowths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BussinessGrowthTitle,ShortDescription,VideoLink,IconName,IconTitle,IconShortDescription")] HomeBussinessGrowth homeBussinessGrowth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homeBussinessGrowth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeBussinessGrowth);
        }

        // GET: Unibase/HomeBussinessGrowths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeBussinessGrowth = await _context.HomeBussinessGrowths.FindAsync(id);
            if (homeBussinessGrowth == null)
            {
                return NotFound();
            }
            return View(homeBussinessGrowth);
        }

        // POST: Unibase/HomeBussinessGrowths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BussinessGrowthTitle,ShortDescription,VideoLink,IconName,IconTitle,IconShortDescription")] HomeBussinessGrowth homeBussinessGrowth)
        {
            if (id != homeBussinessGrowth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeBussinessGrowth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeBussinessGrowthExists(homeBussinessGrowth.Id))
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
            return View(homeBussinessGrowth);
        }

        // GET: Unibase/HomeBussinessGrowths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeBussinessGrowth = await _context.HomeBussinessGrowths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeBussinessGrowth == null)
            {
                return NotFound();
            }

            return View(homeBussinessGrowth);
        }

        // POST: Unibase/HomeBussinessGrowths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeBussinessGrowth = await _context.HomeBussinessGrowths.FindAsync(id);
            _context.HomeBussinessGrowths.Remove(homeBussinessGrowth);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeBussinessGrowthExists(int id)
        {
            return _context.HomeBussinessGrowths.Any(e => e.Id == id);
        }
    }
}
