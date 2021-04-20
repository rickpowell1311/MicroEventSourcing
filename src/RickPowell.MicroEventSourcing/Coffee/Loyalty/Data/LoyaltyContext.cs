using Microsoft.EntityFrameworkCore;
using RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain;
using System.Linq;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Data
{
    public class LoyaltyContext : DbContext
    {
        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }

        public IQueryable<Domain.Projections.LoyaltyCard> LoyaltyCardProjections => LoyaltyCards.Select(x => x.Projection);

        public LoyaltyContext(DbContextOptions<LoyaltyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var loyaltyCardEntity = modelBuilder.Entity<LoyaltyCard>();
            loyaltyCardEntity.OwnsOne(x => x.CreatedLoyaltyCard);
            loyaltyCardEntity.OwnsMany(x => x.PurchasedCoffees);
            loyaltyCardEntity.OwnsMany(x => x.FreeCoffeesAwarded);
            loyaltyCardEntity.OwnsMany(x => x.FreeCoffeesClaimed);

            loyaltyCardEntity.HasOne(x => x.Projection);
        }
    }
}
