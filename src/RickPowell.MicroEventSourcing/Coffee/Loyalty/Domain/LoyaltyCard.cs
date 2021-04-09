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

        private event Action<CoffeePurchased> CoffeePurchased;

        private event Action<FreeCoffeeAwarded> FreeCoffeeAwarded;

        private event Action<FreeCoffeeClaimed> FreeCoffeeClaimed;

        protected LoyaltyCard()
        {
            CoffeePurchased += OnCoffeePurchased;
            FreeCoffeeAwarded += OnFreeCoffeeAwarded;
            FreeCoffeeClaimed += OnFreeCoffeeClaimed;

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
            CoffeePurchased.Invoke(new CoffeePurchased(DateTime.UtcNow));
        }

        public void ClaimFreeCoffee()
        {
            FreeCoffeeClaimed.Invoke(new FreeCoffeeClaimed(DateTime.UtcNow));
        }

        private void OnCoffeePurchased(CoffeePurchased evnt)
        {
            var existingTotalCoffeesPurchased = PurchasedCoffees.Count;
            var newTotalCoffeesPurchased = existingTotalCoffeesPurchased + 1;

            if (newTotalCoffeesPurchased / 10 != existingTotalCoffeesPurchased / 10)
            {
                FreeCoffeeAwarded(new FreeCoffeeAwarded(DateTime.UtcNow));
            }

            PurchasedCoffees.Add(evnt);
        }

        private void OnFreeCoffeeAwarded(FreeCoffeeAwarded freeCoffeeAwarded)
        {
            FreeCoffeesAwarded.Add(freeCoffeeAwarded);
        }

        private void OnFreeCoffeeClaimed(FreeCoffeeClaimed freeCoffeeClaimed)
        {
            if (FreeCoffeesClaimed.Count >= FreeCoffeesAwarded.Count)
            {
                throw new InvalidOperationException("No free coffees available");
            }
        }
    }
}
