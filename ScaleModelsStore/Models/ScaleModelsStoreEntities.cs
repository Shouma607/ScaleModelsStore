namespace ScaleModelsStore.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using static ScaleModelsStore.Models.User;

    public partial class ScaleModelsStoreEntities : IdentityDbContext<User>
    {
        public ScaleModelsStoreEntities()
            : base("ScaleModelsStoreEntities", throwIfV1Schema:false)
        {
        }

        public static ScaleModelsStoreEntities Create()
        {
            return new ScaleModelsStoreEntities();
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderToProduct> OrderToProducts { get; set; }
        public virtual DbSet<DeliveryTypesDictionary> DeliveryTypes { get; set; }
        public virtual DbSet<OrderStatusesDictionary> OrderStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IdentityLoginConfig());
            modelBuilder.Configurations.Add(new IdentityRoleConfig());
        }
    }
}
