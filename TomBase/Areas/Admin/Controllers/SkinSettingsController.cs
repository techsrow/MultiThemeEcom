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
    public class SkinSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkinSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SkinSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.SkinSettings.ToListAsync());
        }

        // GET: Admin/SkinSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skinSetting = await _context.SkinSettings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skinSetting == null)
            {
                return NotFound();
            }

            return View(skinSetting);
        }

        // GET: Admin/SkinSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SkinSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PrimaryColor,SecondryColor,FontFamily")] SkinSetting skinSetting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skinSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skinSetting);
        }

        // GET: Admin/SkinSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skinSetting = await _context.SkinSettings.FindAsync(id);
            if (skinSetting == null)
            {
                return NotFound();
            }
            return View(skinSetting);
        }

        // POST: Admin/SkinSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PrimaryColor,SecondryColor,FontFamily")] SkinSetting skinSetting)
        {
            if (id != skinSetting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skinSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkinSettingExists(skinSetting.Id))
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
            return View(skinSetting);
        }

        // GET: Admin/SkinSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skinSetting = await _context.SkinSettings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skinSetting == null)
            {
                return NotFound();
            }

            return View(skinSetting);
        }

        // POST: Admin/SkinSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skinSetting = await _context.SkinSettings.FindAsync(id);
            _context.SkinSettings.Remove(skinSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkinSettingExists(int id)
        {
            return _context.SkinSettings.Any(e => e.Id == id);
        }
    }
}
