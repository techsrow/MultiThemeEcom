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
    public class SeosController : Controller
    {
        private readonly BasePackageModule1Context _context;

        public SeosController(BasePackageModule1Context context)
        {
            _context = context;
        }

        // GET: Unibase/Seos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Seo.ToListAsync());
        }

        // GET: Unibase/Seos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seo = await _context.Seo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seo == null)
            {
                return NotFound();
            }

            return View(seo);
        }

        // GET: Unibase/Seos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Unibase/Seos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MetaTitle,MetaDiscription,CanonicalTag,OgTitle,OgDescription,OgUrl,OgSiteName,MetaTwitter,MetaTwitterDescription,TwitterTitle,MsValidate,GSiteVerivation")] Seo seo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seo);
        }

        // GET: Unibase/Seos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seo = await _context.Seo.FindAsync(id);
            if (seo == null)
            {
                return NotFound();
            }
            return View(seo);
        }

        // POST: Unibase/Seos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MetaTitle,MetaDiscription,CanonicalTag,OgTitle,OgDescription,OgUrl,OgSiteName,MetaTwitter,MetaTwitterDescription,TwitterTitle,MsValidate,GSiteVerivation")] Seo seo)
        {
            if (id != seo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeoExists(seo.Id))
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
            return View(seo);
        }

        // GET: Unibase/Seos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seo = await _context.Seo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seo == null)
            {
                return NotFound();
            }

            return View(seo);
        }

        // POST: Unibase/Seos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seo = await _context.Seo.FindAsync(id);
            _context.Seo.Remove(seo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeoExists(int id)
        {
            return _context.Seo.Any(e => e.Id == id);
        }
    }
}
