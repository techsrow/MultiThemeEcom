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
    public class CustomerServicePagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerServicePagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CustomerServicePages
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerServicePages.ToListAsync());
        }

        // GET: Admin/CustomerServicePages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerServicePage = await _context.CustomerServicePages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerServicePage == null)
            {
                return NotFound();
            }

            return View(customerServicePage);
        }

        // GET: Admin/CustomerServicePages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CustomerServicePages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeadingName,PageName,PageUrl")] CustomerServicePage customerServicePage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerServicePage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerServicePage);
        }

        // GET: Admin/CustomerServicePages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerServicePage = await _context.CustomerServicePages.FindAsync(id);
            if (customerServicePage == null)
            {
                return NotFound();
            }
            return View(customerServicePage);
        }

        // POST: Admin/CustomerServicePages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeadingName,PageName,PageUrl")] CustomerServicePage customerServicePage)
        {
            if (id != customerServicePage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerServicePage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerServicePageExists(customerServicePage.Id))
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
            return View(customerServicePage);
        }

        // GET: Admin/CustomerServicePages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerServicePage = await _context.CustomerServicePages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerServicePage == null)
            {
                return NotFound();
            }

            return View(customerServicePage);
        }

        // POST: Admin/CustomerServicePages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerServicePage = await _context.CustomerServicePages.FindAsync(id);
            _context.CustomerServicePages.Remove(customerServicePage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerServicePageExists(int id)
        {
            return _context.CustomerServicePages.Any(e => e.Id == id);
        }
    }
}
