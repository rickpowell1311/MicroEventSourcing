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

        public void Project(Domain.LoyaltyCard loyaltyCard)
        {
            loyaltyCard.LoyaltyCardCreated += OnLoyaltyCardCreated;
            loyaltyCard.CoffeePurchased += OnCoffeePurchased;
            loyaltyCard.FreeCoffeeAwarded += OnFreeCoffeeAwarded;
            loyaltyCard.FreeCoffeeClaimed += OnFreeCoffeeClaimed;
        }

        private void OnLoyaltyCardCreated(LoyaltyCardCreated evnt)
        {
            CustomerName = evnt.Customer.Name;
            CreatedOn = evnt.CreatedOn;
        }

        private void OnCoffeePurchased(CoffeePurchased evnt)
        {
            PurchasedCoffees++;
        }

        private void OnFreeCoffeeAwarded(FreeCoffeeAwarded evnt)
        {
            FreeCoffeesAwarded++;
        }

        private void OnFreeCoffeeClaimed(FreeCoffeeClaimed evnt)
        {
            FreeCoffeesClaimed++;
        }
    }
}
