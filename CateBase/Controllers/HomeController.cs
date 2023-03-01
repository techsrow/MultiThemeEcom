﻿using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using BasePackageModule1.Helpers;
using BasePackageModule2.Extensions;
using BasePackageModule2.Data;
using BasePackageModule2.Helpers;
using BasePackageModule2.Models;
using BasePackageModule2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BasePackageModule1.Helpers;

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

        public HomeController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITemplateHelper templateHelper, IEmailConfiguration emailConfiguration, IEmailService emailService, ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _templateHelper = templateHelper;
            _emailConfiguration = emailConfiguration;
            _emailService = emailService;
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //var products = await _context.Products.ToListAsync();
            //products.ForEach(product =>
            //{
            //    product.FreeShipping = true;
            //});

            //_context.UpdateRange(products);
            //await _context.SaveChangesAsync();
            

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

            var about = _context.AboutUs.Any() ? await _context.AboutUs.FirstOrDefaultAsync() : new AboutUs();
            
          

            var banner = await _context.Banners.FirstOrDefaultAsync();
            var categoryList = await _context.Categories.ToListAsync();
            var newProductList = await _context.Products.OrderByDescending(o => o.Id).Take(10).ToListAsync();

            var images = await _context.SliderImages.ToListAsync();
            var update = await _context.Posts.OrderByDescending(s => s.Id).Take(4).ToListAsync();
            var gallery = await _context.Images.OrderByDescending(s => s.Id).Take(8).ToListAsync();
            var supdate = await _context.Posts.FirstOrDefaultAsync();
            var businessProfile = await _context.BusinessProfile.FirstOrDefaultAsync();
            var items = await _context.Products.OrderByDescending(s => s.Id).Take(4).ToListAsync();
            var menus = await _context.Menus.Where(a => a.ShowOnHomeScreen).Include(a => a.MenuProducts).ThenInclude(a => a.Product).ToListAsync();
            var bestSellar = await _context.Products.OrderBy(o => o.Id).Take(10).ToListAsync();
            var categories = categoryList.ChunkBy(2);
            var products = newProductList.ChunkBy(2);
            var bestProducts = newProductList.ChunkBy(2);



            HomeViewModel model = new HomeViewModel
            {
                SliderImages = images,
                _AboutUs = about,
                Updates = update,
                _Image = gallery,
                _supdate =supdate,
                BusinessProfile = businessProfile,
                _items = items,
                Menus = menus,
                _ChunkCategory = categories,
                _ChunkNewProduct = products,
                _ChunkBestSellar = bestProducts

            };
           
            return View(model);
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
       
      
       

        public IActionResult Contact()
        {
            var footer = new BusinessProfile();


          
            if (_context.BusinessProfile.Any())
            {
                footer = _context.BusinessProfile.FirstOrDefault();
            }
            var item = _context.Contacts.FirstOrDefault();
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
                        Name = "Lead Walker",
                        Address = _emailConfiguration.SmtpUsername
                    },
                    Subject = "You received a Inquiry!",
                    Content = await _templateHelper.GetTemplateHtmlAsStringAsync("Templates/ContactMessage", contactMessage)
                };

                _emailService.Send(emailMessage);

                return RedirectToAction("Index").WithSuccess("Your message has been sent.", null); ;
            }

            var item = _context.Contacts.FirstOrDefault();
            var footer = _context.BusinessProfile.FirstOrDefault();

            ContactViewModel model = new ContactViewModel()
            {
                _contact = item,
                BusinessProfile = footer


            };
            return View(model).WithWarning("Please Try again.", null);
        }
    }

}


    

