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
    public class HomeProgessBarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeProgessBarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Unibase/HomeProgessBars
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeProgessBars.ToListAsync());
        }

        // GET: Unibase/HomeProgessBars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeProgessBar = await _context.HomeProgessBars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeProgessBar == null)
            {
                return NotFound();
            }

            return View(homeProgessBar);
        }

        // GET: Unibase/HomeProgessBars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Unibase/HomeProgessBars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TitleOne,TitleTwo")] HomeProgessBar homeProgessBar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homeProgessBar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeProgessBar);
        }

        // GET: Unibase/HomeProgessBars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeProgessBar = await _context.HomeProgessBars.FindAsync(id);
            if (homeProgessBar == null)
            {
                return NotFound();
            }
            return View(homeProgessBar);
        }

        // POST: Unibase/HomeProgessBars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TitleOne,TitleTwo")] HomeProgessBar homeProgessBar)
        {
            if (id != homeProgessBar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeProgessBar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeProgessBarExists(homeProgessBar.Id))
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
            return View(homeProgessBar);
        }

        // GET: Unibase/HomeProgessBars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeProgessBar = await _context.HomeProgessBars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeProgessBar == null)
            {
                return NotFound();
            }

            return View(homeProgessBar);
        }

        // POST: Unibase/HomeProgessBars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeProgessBar = await _context.HomeProgessBars.FindAsync(id);
            _context.HomeProgessBars.Remove(homeProgessBar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeProgessBarExists(int id)
        {
            return _context.HomeProgessBars.Any(e => e.Id == id);
        }
    }
}
