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
    public class InformationPagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InformationPagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/InformationPages
        public async Task<IActionResult> Index()
        {
            return View(await _context.InformationPages.ToListAsync());
        }

        // GET: Admin/InformationPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informationPage = await _context.InformationPages
                .FirstOrDefaultAsync(m => m.id == id);
            if (informationPage == null)
            {
                return NotFound();
            }

            return View(informationPage);
        }

        // GET: Admin/InformationPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/InformationPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,HeadingName,PageName,PageUrl")] InformationPage informationPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informationPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(informationPage);
        }

        // GET: Admin/InformationPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informationPage = await _context.InformationPages.FindAsync(id);
            if (informationPage == null)
            {
                return NotFound();
            }
            return View(informationPage);
        }

        // POST: Admin/InformationPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,HeadingName,PageName,PageUrl")] InformationPage informationPage)
        {
            if (id != informationPage.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informationPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationPageExists(informationPage.id))
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
            return View(informationPage);
        }

        // GET: Admin/InformationPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informationPage = await _context.InformationPages
                .FirstOrDefaultAsync(m => m.id == id);
            if (informationPage == null)
            {
                return NotFound();
            }

            return View(informationPage);
        }

        // POST: Admin/InformationPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var informationPage = await _context.InformationPages.FindAsync(id);
            _context.InformationPages.Remove(informationPage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformationPageExists(int id)
        {
            return _context.InformationPages.Any(e => e.id == id);
        }
    }
}
