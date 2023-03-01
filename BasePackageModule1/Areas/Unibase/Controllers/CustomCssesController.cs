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
    public class CustomCssesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomCssesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Unibase/CustomCsses
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomCsses.ToListAsync());
        }

        // GET: Unibase/CustomCsses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customCss = await _context.CustomCsses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customCss == null)
            {
                return NotFound();
            }

            return View(customCss);
        }

        // GET: Unibase/CustomCsses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Unibase/CustomCsses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MainColorOne,SecondaryColor,HeadingColor,ParagraphColor,HeadingFont,BodyFont")] CustomCss customCss)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customCss);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customCss);
        }

        // GET: Unibase/CustomCsses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customCss = await _context.CustomCsses.FindAsync(id);
            if (customCss == null)
            {
                return NotFound();
            }
            return View(customCss);
        }

        // POST: Unibase/CustomCsses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MainColorOne,SecondaryColor,HeadingColor,ParagraphColor,HeadingFont,BodyFont")] CustomCss customCss)
        {
            if (id != customCss.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customCss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomCssExists(customCss.Id))
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
            return View(customCss);
        }

        // GET: Unibase/CustomCsses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customCss = await _context.CustomCsses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customCss == null)
            {
                return NotFound();
            }

            return View(customCss);
        }

        // POST: Unibase/CustomCsses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customCss = await _context.CustomCsses.FindAsync(id);
            _context.CustomCsses.Remove(customCss);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomCssExists(int id)
        {
            return _context.CustomCsses.Any(e => e.Id == id);
        }
    }
}
