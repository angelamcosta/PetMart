using ClassLibrary;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PetMart.Data
{
    public class PetMartContext : IdentityDbContext<Client>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public PetMartContext(DbContextOptions<PetMartContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Dog" },
                new Category { Id = 2, Name = "Cat" },
                new Category { Id = 3, Name = "Fish" },
                new Category { Id = 4, Name = "Bird" },
                new Category { Id = 5, Name = "Reptile" }
                );
            builder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Dry Food", CategoryId = 1, Price = 15.48m, Stock = 200, Description = "This formula, with nutrient-rich salmon as the first ingredient, ensures that each bite packs a powerfully tasty flavor backed by essential nutrition to help support your dog's skin and stomach, as well as his entire well-being.", Url = "017-dog food.svg" },
                new Product { Id = 2, Name = "Wet Food", CategoryId = 2, Price = 0.61m, Stock = 200, Description = "Every serving offers 100% complete and balanced nutrition for the growth of kittens and maintenance of adult cats, so this wet cat food can follow your darling cat through every stage of her happy life with you.", Url = "002-cat food.svg" },
                new Product { Id = 3, Name = "Aquarium Starter Kit", CategoryId = 3, Price = 139.99m, Stock = 200, Description = "This aquarium has a capacity of 55 gallons, and comes with a variety of components including a glass tank, LED Lighting, heater, power filter and more. Your fish will love this spacious and beautiful home, and you will love the incredible aesthetic it brings to your home.", Url = "005-aquarium.svg" },
                new Product { Id = 4, Name = "Knots & Blocks Bird Toy", CategoryId = 4, Price = 17.99m, Stock = 200, Description = "This fun toy is perfect for birds who enjoy complex interaction, and rewards exploring while keeping your bird active.", Url = "010-scratcher.svg" },
                new Product { Id = 5, Name = "Natural Terrarium Reptile Habitat", CategoryId = 5, Price = 169.99m, Stock = 200, Description = "This habitat is specially designed for desert setups, since most desert species are ground dwellers and do not move vertically.", Url = "012-crates.svg" }
                );
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }

}
