using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasePackageModule2.Data;
using TomBase.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TomBase.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AllPageHeadersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllPageHeadersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AllPageHeaders
        public async Task<IActionResult> Index()
        {
            return View(await _context.AllPageHeaders.ToListAsync());
        }

        // GET: Admin/AllPageHeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allPageHeader = await _context.AllPageHeaders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allPageHeader == null)
            {
                return NotFound();
            }

            return View(allPageHeader);
        }

        // GET: Admin/AllPageHeaders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AllPageHeaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile formImage, AllPageHeader category)
        {
            if (ModelState.IsValid)
            {
                if (formImage == null)
                {
                    ModelState.AddModelError("Image", "Image is required.");
                    return View(category);
                }
                var imageName = Guid.NewGuid() + Path.GetExtension(formImage.FileName);

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Category")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Category"));
                }

                //Get url To Save
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Category", imageName);

                await using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    formImage.CopyTo(stream);
                }

                category.HeaderImage = $"/img/Category/{imageName}";


                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        // GET: Admin/AllPageHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allPageHeader = await _context.AllPageHeaders.FindAsync(id);
            if (allPageHeader == null)
            {
                return NotFound();
            }
            return View(allPageHeader);
        }

        // POST: Admin/AllPageHeaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeaderImage")] AllPageHeader allPageHeader, IFormFile bannerImage)
        {
            if (id != allPageHeader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (bannerImage == null)
                {
                    ModelState.AddModelError("Image", "Image is required.");
                    return View(allPageHeader);
                }

                var bannerImageName = Guid.NewGuid() + Path.GetExtension(bannerImage.FileName);

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Abouts")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Abouts"));
                }

                //Get url To Save
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Abouts", bannerImageName);




                await using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    bannerImage.CopyTo(stream);
                }
                try
                {
                    _context.Update(allPageHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllPageHeaderExists(allPageHeader.Id))
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
            return View(allPageHeader);
        }

        // GET: Admin/AllPageHeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allPageHeader = await _context.AllPageHeaders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allPageHeader == null)
            {
                return NotFound();
            }

            return View(allPageHeader);
        }

        // POST: Admin/AllPageHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allPageHeader = await _context.AllPageHeaders.FindAsync(id);
            _context.AllPageHeaders.Remove(allPageHeader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllPageHeaderExists(int id)
        {
            return _context.AllPageHeaders.Any(e => e.Id == id);
        }
    }
}
