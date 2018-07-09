namespace ScaleModelsStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ScaleModelsStore.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ScaleModelsStoreEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ScaleModelsStoreEntities context)
        {

            context.Manufacturers.AddOrUpdate(x => x.ManufacturerId,
                new Manufacturer { ManufacturerId = 1, Name = "Manufacturer1", Country = "Country1" },
                new Manufacturer { ManufacturerId = 2, Name = "Manufacturer2", Country = "Country1"},
                new Manufacturer { ManufacturerId = 3, Name = "Manufacturer3", Country = "Country2"}
                );

            context.Categories.AddOrUpdate(x => x.CategoryId,
                new Category { CategoryId = 1, Name = "Category1", Description = "Category1"},
                new Category { CategoryId = 2, Name = "Category2", Description = "Category2" },
                new Category { CategoryId = 3, Name = "Category3", Description = "Category3" }
                );

            context.Products.AddOrUpdate(x => x.ProductId,
                new Product { ProductId = 1, ProductName = "Product1", CategoryId = 1, ManufacturerId = 1, Scale = "1/16", Material = "Metal", Price = 100, QuantityInStock = 1000, Description = "Product1", ImagePath = "\\Content\\Images\\"},
                new Product { ProductId = 2, ProductName = "Product2", CategoryId = 1, ManufacturerId = 1, Scale = "1/16", Material = "Plastic", Price = 80, QuantityInStock = 1000, Description = "Product2", ImagePath = "\\Content\\Images\\" },
                new Product { ProductId = 3, ProductName = "Product3", CategoryId = 1, ManufacturerId = 2, Scale = "1/6", Material = "Metal", Price = 250, QuantityInStock = 1000, Description = "Product3", ImagePath = "\\Content\\Images\\" },
                new Product { ProductId = 4, ProductName = "Product4", CategoryId = 3, ManufacturerId = 1, Scale = "1/32", Material = "Plastic", Price = 50, QuantityInStock = 1000, Description = "Product4", ImagePath = "\\Content\\Images\\" },
                new Product { ProductId = 5, ProductName = "Product5", CategoryId = 2, ManufacturerId = 3, Scale = "1/24", Material = "Metal", Price = 100, QuantityInStock = 1000, Description = "Product5", ImagePath = "\\Content\\Images\\" },
                new Product { ProductId = 6, ProductName = "Product6", CategoryId = 3, ManufacturerId = 3, Scale = "1/8", Material = "Plastic", Price = 180, QuantityInStock = 1000, Description = "Product6", ImagePath = "\\Content\\Images\\" },
                new Product { ProductId = 7, ProductName = "Product7", CategoryId = 2, ManufacturerId = 2, Scale = "1/16", Material = "Metal", Price = 150, QuantityInStock = 1000, Description = "Product7", ImagePath = "\\Content\\Images\\" },
                new Product { ProductId = 8, ProductName = "Product8", CategoryId = 2, ManufacturerId = 1, Scale = "1/24", Material = "Metal", Price = 90, QuantityInStock = 1000, Description = "Product8", ImagePath = "\\Content\\Images\\" }
                );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
