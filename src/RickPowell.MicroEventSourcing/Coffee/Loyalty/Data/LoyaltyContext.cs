using Microsoft.EntityFrameworkCore;
using RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Data
{
    public class LoyaltyContext : DbContext
    {
        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }

        public LoyaltyContext(DbContextOptions<LoyaltyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var purchaseEntity = modelBuilder.Entity<LoyaltyCard>();
            purchaseEntity.OwnsOne(x => x.Customer);
        }
    }
}
