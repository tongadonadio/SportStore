using System.Data.Entity;

using SportStore.Model;

namespace SportStore.Repository.Entity
{
    internal class SportStoreDbContext : DbContext
    {
        public DbSet<Cart> CartDbSet { get; set; }
        public DbSet<Category> CategoryDbSet { get; set; }
        public DbSet<Manufacturer> ManufacturerDbSet { get; set; }
        public DbSet<PaymentMethod> PaymentMethodDbSet { get; set; }
        public DbSet<Product> ProductDbSet { get; set; }
        public DbSet<Purchase> PurchaseDbSet { get; set; }
        public DbSet<PurchasedProduct> PurchasedProductDbSet { get; set; }
        public DbSet<Review> ReviewDbSet { get; set; }
        public DbSet<Role> RoleDbSet { get; set; }
        public DbSet<Session> SessionDbSet { get; set; }
        public DbSet<ShippingAddress> ShippingAddressDbSet { get; set; }
        public DbSet<User> UserDbSet { get; set; }

        public DbSet<ConfigEntry> ConfigDbSet { get; set; }

        public SportStoreDbContext()
        {
            this.CartDbSet.Load();
            this.CategoryDbSet.Load();
            this.ManufacturerDbSet.Load();
            this.PaymentMethodDbSet.Load();
            this.ProductDbSet.Load();
            this.PurchaseDbSet.Load();
            this.PurchasedProductDbSet.Load();
            this.ReviewDbSet.Load();
            this.RoleDbSet.Load();
            this.SessionDbSet.Load();
            this.ShippingAddressDbSet.Load();
            this.UserDbSet.Load();
        }
    }
}
