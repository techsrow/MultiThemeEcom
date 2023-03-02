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
    public class MyAccountPagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyAccountPagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MyAccountPages
        public async Task<IActionResult> Index()
        {
            return View(await _context.MyAccountPages.ToListAsync());
        }

        // GET: Admin/MyAccountPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myAccountPage = await _context.MyAccountPages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myAccountPage == null)
            {
                return NotFound();
            }

            return View(myAccountPage);
        }

        // GET: Admin/MyAccountPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MyAccountPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeadingName,PageName,PageUrl")] MyAccountPage myAccountPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myAccountPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myAccountPage);
        }

        // GET: Admin/MyAccountPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myAccountPage = await _context.MyAccountPages.FindAsync(id);
            if (myAccountPage == null)
            {
                return NotFound();
            }
            return View(myAccountPage);
        }

        // POST: Admin/MyAccountPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeadingName,PageName,PageUrl")] MyAccountPage myAccountPage)
        {
            if (id != myAccountPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myAccountPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyAccountPageExists(myAccountPage.Id))
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
            return View(myAccountPage);
        }

        // GET: Admin/MyAccountPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myAccountPage = await _context.MyAccountPages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myAccountPage == null)
            {
                return NotFound();
            }

            return View(myAccountPage);
        }

        // POST: Admin/MyAccountPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myAccountPage = await _context.MyAccountPages.FindAsync(id);
            _context.MyAccountPages.Remove(myAccountPage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyAccountPageExists(int id)
        {
            return _context.MyAccountPages.Any(e => e.Id == id);
        }
    }
}
