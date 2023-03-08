using BasePackageModule1.Models.Menu;
using BasePackageModule2.Models;
using BasePackageModule2.Models.Menu;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TomBase.Models;
using TomBase.Models.ShipRocket;

namespace BasePackageModule2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductAskQuestion> ProductAskQuestions { get; set; }
        public DbSet<LoginResponse> LoginResponse { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
       
        public DbSet<SliderImage> SliderImages { get; set; }
      
        public DbSet<Banner> Banners { get; set; }
        
        public DbSet<Page> Pages { get; set; }
       
        public DbSet<Post> Posts { get; set; }

        public DbSet<Media> Medias { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }
        
      
       
        public DbSet<Logo> Logos { get; set; }
        public DbSet<Seo> Seo { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<News> News { get; set; }






        public DbSet<BusinessProfile> BusinessProfile { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<MenuProduct> MenuProducts { get; set; }

        public DbSet<MenuCategory> MenuCategories { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<NewsSubscriber> NewsSubscribers { get;  set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Testimonail> Testimonails { get; set; }
        public DbSet<WomenImpowerment> WomenImpowerments { get; set; }

        public DbSet<ContactUs> ContactUs  { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<ThemeSetting> ThemeSettings { get; set; }

        public DbSet<CustomerServicePage> CustomerServicePages { get; set; }

        public DbSet<MyAccountPage> MyAccountPages { get; set; }

        public DbSet<InformationPage> InformationPages { get; set; }

        public DbSet<BlogPage> BlogPages { get; set; }
        public DbSet<AboutUsPageHeader> AboutUsPageHeaders { get; set; }
        public DbSet<ShopPageHeader> ShopPageHeaders { get; set; }
        public DbSet<AllPageHeader> AllPageHeaders { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                .HasMany(p => p.SubCategories)
                .WithOne(t => t.Category)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Order>()
                .HasOne(a => a.User)
                .WithMany(a => a.Orders)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("getdate()");

            builder.Entity<Post>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("getdate()");


            builder.Entity<Order>()
                .Property(o => o.Date)
                .HasDefaultValueSql("getdate()");
        }

    }
}
