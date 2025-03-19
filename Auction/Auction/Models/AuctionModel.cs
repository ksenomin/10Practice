using System.Data.Entity;

namespace Auction.Models
{
    public partial class AuctionModel : DbContext
    {
        public AuctionModel()
            : base("name=AuctionModel")
        {
        }

        public virtual DbSet<AuctionSale> AuctionSales { get; set; }
        public virtual DbSet<ObjectSell> ObjectSells { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TypeObject> TypeObjects { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObjectSell>()
                .HasMany(e => e.AuctionSales)
                .WithRequired(e => e.ObjectSell)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeObject>()
                .HasMany(e => e.ObjectSells)
                .WithRequired(e => e.TypeObject)
                .WillCascadeOnDelete(false);
        }
    }
}
