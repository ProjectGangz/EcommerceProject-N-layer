using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectEcommerce.Models;

namespace ProjectEcommerce.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating (modelBuilder);
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { CategoryId = 1, CategoryName = "Dress", CategoryDescription = "All of Clothing", DisplayOrder = 1},
                new CategoryModel { CategoryId = 2, CategoryName = "Skirt", CategoryDescription = "All of Clothing", DisplayOrder = 2 },
                new CategoryModel { CategoryId = 3, CategoryName = "Cost", CategoryDescription = "All of Clothing", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Title1",
                    Description = "Description one",
                    ListPrice = 100,
                    Price = 90,
                    ListPrice50 = 80,
                    ListPrice100 = 60,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "Title2",
                    Description = "Description two",
                    ListPrice = 200,
                    Price = 180,
                    ListPrice50 = 160,
                    ListPrice100 = 120,
                    CategoryId = 1,
                    ImageUrl = ""
                }
                );
        }
    }
}
