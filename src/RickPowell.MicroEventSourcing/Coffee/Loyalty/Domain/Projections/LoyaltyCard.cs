using RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain.Events;
using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain.Projections
{
    public class LoyaltyCard
    {
        public long Id { get; protected set; }

        public DateTime CreatedOn { get; set; }

        public int PurchasedCoffees { get; set; }

        public int FreeCoffeesAwarded { get; set; }

        public int FreeCoffeesClaimed { get; set; }

        public string CustomerName { get; set; }

        private Domain.LoyaltyCard _loyaltyCard;

        public void Project(Domain.LoyaltyCard loyaltyCard)
        {
            _loyaltyCard = loyaltyCard;

            loyaltyCard.LoyaltyCardCreated += _ => Build();
            loyaltyCard.CoffeePurchased += _ => Build();
            loyaltyCard.FreeCoffeeAwarded += _ => Build();
            loyaltyCard.FreeCoffeeClaimed += _ => Build();
        }

        private void Build()
        {
            CustomerName = _loyaltyCard.CreatedLoyaltyCard.CustomerName;
            CreatedOn = _loyaltyCard.CreatedLoyaltyCard.CreatedOn;
            PurchasedCoffees = _loyaltyCard.PurchasedCoffees.Count;
            FreeCoffeesAwarded = _loyaltyCard.FreeCoffeesAwarded.Count;
            FreeCoffeesClaimed = _loyaltyCard.FreeCoffeesClaimed.Count;
        }
    }
}
