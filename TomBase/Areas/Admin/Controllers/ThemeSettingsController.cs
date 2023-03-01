using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasePackageModule2.Data;
using TomBase.Models;

namespace TomBase.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThemeSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThemeSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ThemeSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.ThemeSettings.ToListAsync());
        }

        // GET: Admin/ThemeSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeSetting = await _context.ThemeSettings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (themeSetting == null)
            {
                return NotFound();
            }

            return View(themeSetting);
        }

        // GET: Admin/ThemeSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ThemeSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ThemeName,ThemeCategory,IsActive,SkinType")] ThemeSetting themeSetting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(themeSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(themeSetting);
        }

        // GET: Admin/ThemeSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeSetting = await _context.ThemeSettings.FindAsync(id);
            if (themeSetting == null)
            {
                return NotFound();
            }
            return View(themeSetting);
        }

        // POST: Admin/ThemeSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ThemeName,ThemeCategory,IsActive,SkinType")] ThemeSetting themeSetting)
        {
            if (id != themeSetting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(themeSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeSettingExists(themeSetting.Id))
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
            return View(themeSetting);
        }

        // GET: Admin/ThemeSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeSetting = await _context.ThemeSettings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (themeSetting == null)
            {
                return NotFound();
            }

            return View(themeSetting);
        }

        // POST: Admin/ThemeSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var themeSetting = await _context.ThemeSettings.FindAsync(id);
            _context.ThemeSettings.Remove(themeSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThemeSettingExists(int id)
        {
            return _context.ThemeSettings.Any(e => e.Id == id);
        }
    }
}
