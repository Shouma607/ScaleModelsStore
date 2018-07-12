namespace ScaleModelsStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        ManufacturerId = c.Int(nullable: false),
                        ProductName = c.String(maxLength: 150),
                        Scale = c.String(maxLength: 5),
                        Material = c.String(maxLength: 15),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(maxLength: 300),
                        ImagePath = c.String(maxLength: 1024),
                        QuantityInStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        ManufacturerId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.OrderToProducts",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.DeliveryTypesDictionaries",
                c => new
                    {
                        DeliveryTypeId = c.Int(nullable: false, identity: true),
                        DeliveryTypeDrescription = c.String(),
                    })
                .PrimaryKey(t => t.DeliveryTypeId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(),
                        DeliveryTypeId = c.Int(nullable: false),
                        OrderOpenDate = c.DateTime(nullable: false),
                        OrderStatusId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        PostalCode = c.String(maxLength: 10),
                        Country = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        Address = c.String(maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.DeliveryTypesDictionaries", t => t.DeliveryTypeId, cascadeDelete: true)
                .ForeignKey("dbo.OrderStatusesDictionaries", t => t.OrderStatusId, cascadeDelete: true)
                .Index(t => t.DeliveryTypeId)
                .Index(t => t.OrderStatusId);
            
            CreateTable(
                "dbo.OrderStatusesDictionaries",
                c => new
                    {
                        OrderStatusId = c.Int(nullable: false, identity: true),
                        StatusDescription = c.String(),
                    })
                .PrimaryKey(t => t.OrderStatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderToProducts", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatusesDictionaries");
            DropForeignKey("dbo.Orders", "DeliveryTypeId", "dbo.DeliveryTypesDictionaries");
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderToProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Orders", new[] { "OrderStatusId" });
            DropIndex("dbo.Orders", new[] { "DeliveryTypeId" });
            DropIndex("dbo.OrderToProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderToProducts", new[] { "OrderId" });
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropTable("dbo.OrderStatusesDictionaries");
            DropTable("dbo.Orders");
            DropTable("dbo.DeliveryTypesDictionaries");
            DropTable("dbo.OrderToProducts");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
