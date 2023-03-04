using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BasePackageModule2.Utility;
using BasePackageModule2.Data;
using BasePackageModule2.Models;
using BasePackageModule2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Microsoft.AspNetCore.Identity;
using TomBase.Models;

namespace TomBase.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string category, string query, int minPrice, int maxPrice, int pageindex = 1, int PageSize = 12, string orderby = null)
        {

            IQueryable<Product> products = from s in _context.Products.Include(c=>c.Category).ThenInclude(s=>s.SubCategories)
                                           select s;
         
            //products = products.Where(s => s.Status == true);

            if (category != null)
            {
                products = products.Where(c => c.Category.Name == category);
            }

            if (query != null)
            {
                products = products.Where(s => s.Name.Contains(query));
            }

            switch (orderby)
            {
                //case "Date":
                //    products = products.OrderBy(s => s.CreatedAt);
                //    break;
                case "price":
                    products = products.OrderBy(s => s.FinalPrice);
                    break;
                case "price-desc":
                    products = products.OrderByDescending(s => s.FinalPrice);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="6", Text= "6" },
                new SelectListItem() { Value="12", Text= "12" },
                new SelectListItem() { Value="24", Text= "24" },
                new SelectListItem() { Value="48", Text= "48" },
                new SelectListItem() { Value="100", Text= "100" },
            };
            if (minPrice > 0)
            {
                products = products.Where(p => p.FinalPrice >= minPrice);
                ViewBag.maxPrice = minPrice;
            }

            if (maxPrice > 0)
            {
                products = products.Where(p => p.FinalPrice <= maxPrice);
                ViewBag.minPrice = maxPrice;
            }

          
            ViewBag.orderby = orderby;
            ViewBag.category = category;

            return View(
                await products
                    .ToPagedListAsync(pageNumber: pageindex, pageSize: PageSize));


        }

        [HttpGet]
        [Route("search/{min}/{max}")]
        public IActionResult Search( double min, double max)
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Item =  _context.Products.Include(m => m.Category).Include(m => m.SubCategory).ToList(),
                Category =  _context.Categories.ToList(),
                Coupon = _context.Coupons.Where(c => c.IsActive == true).ToList()
            };
            return new JsonResult(productViewModel);
        }

        
       
        [Route("Products/{Id?}/{slug}")]
        public async Task<IActionResult> Details(int? id, string slug)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var product = await _context.Products
                .Include(m => m.Category)
                .Include(m => m.SubCategory)
                .Include(c => c.Images)
                .Where(m => m.Id == id).FirstOrDefaultAsync();
            List<Product> fundList = _context.Products.Where(c => c.Id != id).Take(6).ToList();
            ViewBag.Funds = fundList;
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            ViewBag.ReviewRatingData = await _context.ProductReviews.Include(x => x.User).Where(x => x.ProductId == id).ToListAsync();
            ViewBag.AskQuestionData = await _context.ProductAskQuestions.Include(x => x.User).Where(x => x.ProductId == id).ToListAsync();

            ViewBag.slug = slug;
            return View(product);
        }

        [HttpPost]
        public async Task<JsonResult> Rating(int id  ,  string ReviewRatingcomment , int Rating)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var ProductReview = new ProductReview();
            ProductReview.Id = 0;
            ProductReview.ProductId= id;
            ProductReview.Rating = Rating;
            ProductReview.Comment= ReviewRatingcomment;
            ProductReview.User = user;
            _context.Add(ProductReview);
            await _context.SaveChangesAsync();

            return new JsonResult(1);
        }

        [HttpPost]
        public async Task<JsonResult> AskQuestion(int id, string AskQuestion )
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var ProductAskQuestion = new ProductAskQuestion();
            ProductAskQuestion.Id = 0;
            ProductAskQuestion.ProductId = id;
            ProductAskQuestion.Question= AskQuestion;
            ProductAskQuestion.User = user;
            _context.Add(ProductAskQuestion);
            await _context.SaveChangesAsync();

            return new JsonResult(1);
        }



        [Route("Show/{Id?}/{menuName?}")]
        public async Task<IActionResult> Show(int? id, string? menuName)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.Include(a => a.MenuProducts).ThenInclude(a => a.Product).FirstOrDefaultAsync(a => a.Id == id);
            if (menu != null)
            {
                var products = await menu.MenuProducts.Where(a => a.MenuId == menu.Id).Select(a => a.Product).ToListAsync();
                return View(products);
            }

            return NotFound();


        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Subscribers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       

    }
}