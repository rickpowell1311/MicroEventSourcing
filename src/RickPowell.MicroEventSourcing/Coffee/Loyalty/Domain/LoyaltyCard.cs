using RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain.Events;
using System;
using System.Collections.Generic;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain
{
    public class LoyaltyCard
    {
        public long Id { get; protected set; }

        public Customer Customer { get; protected set; }

        public ICollection<CoffeePurchased> PurchasedCoffees { get; protected set; }

        public ICollection<FreeCoffeeAwarded> FreeCoffeesAwarded { get; protected set; }

        public ICollection<FreeCoffeeClaimed> FreeCoffeesClaimed { get; protected set; }

        protected LoyaltyCard()
        {
            PurchasedCoffees = new List<CoffeePurchased>();
            FreeCoffeesAwarded = new List<FreeCoffeeAwarded>();
            FreeCoffeesClaimed = new List<FreeCoffeeClaimed>();
        }

        public static LoyaltyCard Create(Customer customer)
        {
            return new LoyaltyCard
            {
                Customer = customer
            };
        }

        public void PurchaseCoffee()
        {
            var existingTotalCoffeesPurchased = PurchasedCoffees.Count;
            var newTotalCoffeesPurchased = existingTotalCoffeesPurchased + 1;

            if (newTotalCoffeesPurchased / 10 != existingTotalCoffeesPurchased / 10)
            {
                FreeCoffeesAwarded.Add(new FreeCoffeeAwarded(DateTime.UtcNow));
            }

            PurchasedCoffees.Add(new CoffeePurchased(DateTime.UtcNow));
        }

        public void ClaimFreeCoffee()
        {
            if (FreeCoffeesClaimed.Count >= FreeCoffeesAwarded.Count)
            {
                throw new InvalidOperationException("No free coffees available");
            }

            FreeCoffeesClaimed.Add(new FreeCoffeeClaimed(DateTime.UtcNow));
        }
    }
}
