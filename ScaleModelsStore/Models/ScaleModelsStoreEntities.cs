namespace ScaleModelsStore.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ScaleModelsStoreEntities : DbContext
    {
        public ScaleModelsStoreEntities()
            : base("name=ScaleModelsStoreEntities")
        {
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
        }
    }
}
