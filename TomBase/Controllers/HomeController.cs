using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BasePackageModule2.Helpers;
using BasePackageModule2.Extensions;
using BasePackageModule2.Models.Menu;
using BasePackageModule2.Data;
using BasePackageModule2.Models;
using BasePackageModule2.Utility;
using BasePackageModule2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TomBase.Helpers;
using TomBase.Models;
using Microsoft.AspNetCore.Hosting;


namespace BasePackageModule2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly ITemplateHelper _templateHelper;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITemplateHelper templateHelper, IEmailConfiguration emailConfiguration, IEmailService emailService, ApplicationDbContext context, ILogger<HomeController> logger , IHostingEnvironment hostingEnvironment )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _templateHelper = templateHelper;
            _emailConfiguration = emailConfiguration;
            _emailService = emailService;
            _context = context;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
      

        }

        [NonAction]
        private async Task<HomeViewModel> Home()
        {
            var about = _context.AboutUs.Any() ? await _context.AboutUs.FirstOrDefaultAsync() : new AboutUs();
            var banner = await _context.Banners.Take(3).ToListAsync();
            var sitem = await _context.Products.FirstOrDefaultAsync();
            var images = await _context.SliderImages.ToListAsync();
            var update = await _context.Posts.OrderByDescending(s => s.Id).Take(3).ToListAsync();
            var gallery = await _context.Images.OrderByDescending(s => s.Id).Take(8).ToListAsync();
            var supdate = await _context.Posts.FirstOrDefaultAsync();
            var businessProfile = await _context.BusinessProfile.FirstOrDefaultAsync();
            var items = await _context.Products.OrderByDescending(s => s.Id).Take(8).ToListAsync();
            var menus = await _context.Menus.Where(a => a.ShowOnHomeScreen).Include(a => a.MenuProducts).ThenInclude(a => a.Product).ToListAsync();
            var categoryList = await _context.Categories.ToListAsync();
            var newProductList = await _context.Products.OrderByDescending(o => o.Id).Take(10).ToListAsync();
            var bestSellar = await _context.Products.OrderBy(o => o.Id).ToListAsync();
            var youmayLike = await _context.Products.Where(e=>e.Essential==true).OrderBy(o => o.Id).ToListAsync();
            var newArrival = await _context.Products.OrderBy(o => o.Id).ToListAsync();
            var dealofDay = await _context.Products.Where(t=>t.DealOfTheDay==true).Take(1).ToListAsync();
            var topratedProduct = await _context.Products.Where(t => t.premium == true).ToListAsync();

            var featureProduct = await _context.Products.Where(h=>h.Featured==true).OrderBy(d => d.Id).ToListAsync();
            
            var feedback = await _context.Testimonails.ToListAsync();
            var hotselling = await _context.Products.Where(h => h.hotselling == true).OrderBy(d => d.Id).Take(20).ToListAsync();
            
          

            var Themesetting  = await _context.ThemeSettings.ToListAsync();





            HomeViewModel model = new HomeViewModel
            {
                SliderImages = images,
                _AboutUs = about,
                Updates = update,
                _Image = gallery,
                _supdate = supdate,
                BusinessProfile = businessProfile,
                _items = items,
                Menus = menus,
                _Category = categoryList,
                _bestProduct = bestSellar,
                _dealofDay = dealofDay,
                _newArrival = newArrival,
                
                _topratedProduct = topratedProduct,
                _youmayLike = youmayLike,
                _bestSaller = bestSellar,
                _banner = banner,
                _sitem = sitem,
                _feedback = feedback,
                _hotselling = hotselling,
               
                _themeSettings = Themesetting,
                _featureproducts = featureProduct



            };

            return model;

        }
        public async Task<IActionResult> Index()
        {
            //ApplicationUser user = new ApplicationUser
            //{
            //    Email = "manage@morbiwala.com",
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    UserName = "manage@safesword.in",
            //    Name = "MorbiWala"
            //};


            //var adminUser = await _userManager.CreateAsync(user, "123@Morbiwala");

            //if (adminUser.Succeeded)
            //{

            //await _roleManager.CreateAsync(new IdentityRole
            //{
            //    Name = "Admin"
            //});
            //await _roleManager.CreateAsync(new IdentityRole
            //{
            //    Name = "User"
            //});

            //await _userManager.AddToRoleAsync(user, "Admin");
            //}

            //var user = await _userManager.Users.FirstOrDefaultAsync(a => a.UserName == "manage@safesword.in");
            //await _userManager.AddToRoleAsync(user, "Admin");
            //await _context.SaveChangesAsync();


            return View(await Home());
        }

        //[HttpPost]
        //public async Task<JsonResult> SaveContactUs(ContactUs ContactUs, int FormType )
        //{
        //    int value = 0;
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(ContactUs);
        //        await _context.SaveChangesAsync();
        //        var  EmailTemplate = new EmailContactUsManager();
        //        var val = EmailTemplate.GetContactUsTemplate(_hostingEnvironment.WebRootPath, FormType, ContactUs);
        //        var EmailMessage = new EmailService.EmailMessage();
        //        EmailMessage.FromAddress = new EmailService.EmailAddress();
        //        EmailMessage.ToAddress = new EmailService.EmailAddress();
        //        EmailMessage.Subject = val.Subject;
        //        EmailMessage.Content = val.Content;
        //        EmailMessage.FromAddress.Name = ContactUs.FullName;
        //        EmailMessage.FromAddress.Address = _emailService.GetDefaultEmail();
        //        EmailMessage.ToAddress.Address = _emailService.GetDefaultEmail();
        //        EmailMessage.ToAddress.Name = _emailService.GetDefaultEmail();
        //        _emailService.Send(EmailMessage);
        //        value = 1;
        //    }
        //    return new JsonResult(value);
        //}
        public IActionResult Contact()
        {
            var footer = new BusinessProfile();



            if (_context.BusinessProfile.Any())
            {
                footer = _context.BusinessProfile.FirstOrDefault();
            }
            var item = _context.ContactUs.FirstOrDefault();
            ContactViewModel model = new ContactViewModel()
            {
                _contact = item,
                BusinessProfile = footer


            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMessage contactMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactMessage);
                await _context.SaveChangesAsync();

                EmailService.EmailMessage emailMessage = new EmailService.EmailMessage
                {
                    FromAddress = new EmailService.EmailAddress
                    {
                        Name = contactMessage.Name,
                        Address = contactMessage.Email
                    },
                    ToAddress = new EmailService.EmailAddress
                    {
                        Name = "Spinners",
                        Address = _emailConfiguration.SmtpUsername
                    },
                    Subject = "You received a Inquiry!",
                    Content = await _templateHelper.GetTemplateHtmlAsStringAsync("Templates/ContactMessage", contactMessage)
                };

                _emailService.Send(emailMessage);

                return RedirectToAction("Index").WithSuccess("Your message has been sent.", null); ;
            }

            var item = _context.ContactUs.FirstOrDefault();
            var footer = _context.BusinessProfile.FirstOrDefault();

            ContactViewModel model = new ContactViewModel()
            {
                _contact = item,
                BusinessProfile = footer


            };
            return View(model).WithWarning("Please Try again.", null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email")] Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subscriber);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
                return RedirectToAction(actionName: "Index", controllerName: "Home");

            }
            return View(subscriber);
        }

        public IActionResult Thankyou()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> AboutUs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footer = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.AboutUsId == id);
            if (footer == null)
            {
                return NotFound();
            }

            return View(footer);
        }







    }

}




