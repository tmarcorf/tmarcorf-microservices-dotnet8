using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {
        }

        public MySQLContext(DbContextOptions<MySQLContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            },
            new Product
            {
                Id = 3,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            },
            new Product
            {
                Id = 4,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            },
            new Product
            {
                Id = 5,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            },
            new Product
            {
                Id = 6,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            },
            new Product
            {
                Id = 7,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            },
            new Product
            {
                Id = 8,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            },
            new Product
            {
                Id = 9,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            },
            new Product
            {
                Id = 10,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            }, 
            new Product
            {
                Id = 11,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            },
            new Product
            {
                Id = 12,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            },
            new Product
            {
                Id = 13,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            },
            new Product
            {
                Id = 14,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            },
            new Product
            {
                Id = 15,
                Name = "Name",
                Price = 69.9M,
                Description = "Description teste",
                ImageUrl = "https://www.google.com",
                CategoryName = "Category",
            });
        }
    }
}
