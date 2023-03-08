using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasePackageModule2.Data;
using BasePackageModule2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BasePackageModule2.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("blog")]
        public async Task<IActionResult> Index(int pageindex = 1, int PageSize = 12, string orderby = null)
        {
            IQueryable<Post> posts = from s in _context.Posts select s;

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="6", Text= "6" },
                new SelectListItem() { Value="12", Text= "12" },
                new SelectListItem() { Value="24", Text= "24" },
                new SelectListItem() { Value="48", Text= "48" },
                new SelectListItem() { Value="100", Text= "100" },
            };
            ViewBag.orderby = orderby;
            return View(
               await posts
                   .ToPagedListAsync(pageNumber: pageindex, pageSize: PageSize));
        }

        [Route("blog/{Id?}/{slug?}")]
        public async Task<IActionResult> Details(int? id, string? slug)
        {
          
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            ViewBag.Posts = await _context.Posts.OrderByDescending(a => a.CreatedAt).Where(m=>m.Id !=id).Take(4).ToListAsync();

            return View(post);
        }

    }
}