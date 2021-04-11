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

        public Projections.LoyaltyCard Projection { get; protected set; }

        public event Action<LoyaltyCardCreated> LoyaltyCardCreated;

        public event Action<CoffeePurchased> CoffeePurchased;

        public event Action<FreeCoffeeAwarded> FreeCoffeeAwarded;

        public event Action<FreeCoffeeClaimed> FreeCoffeeClaimed;

        protected LoyaltyCard()
        {
            PurchasedCoffees = new List<CoffeePurchased>();
            FreeCoffeesAwarded = new List<FreeCoffeeAwarded>();
            FreeCoffeesClaimed = new List<FreeCoffeeClaimed>();

            CoffeePurchased += OnCoffeePurchased;
            FreeCoffeeAwarded += OnFreeCoffeeAwarded;
            FreeCoffeeClaimed += OnFreeCoffeeClaimed;

            if (Projection == null)
            {
                Projection = new Projections.LoyaltyCard();
            }

            Projection.Project(this);
        }

        public static LoyaltyCard Create(Customer customer)
        {
            var loyaltyCard = new LoyaltyCard
            {
                Customer = customer
            };

            loyaltyCard.LoyaltyCardCreated.Invoke(new LoyaltyCardCreated(customer, DateTime.UtcNow));

            return loyaltyCard;
        }

        public void PurchaseCoffee()
        {
            var existingTotalCoffeesPurchased = PurchasedCoffees.Count;
            var newTotalCoffeesPurchased = existingTotalCoffeesPurchased + 1;

            if (newTotalCoffeesPurchased / 10 != existingTotalCoffeesPurchased / 10)
            {
                FreeCoffeeAwarded.Invoke(new FreeCoffeeAwarded(DateTime.UtcNow));
            }

            CoffeePurchased.Invoke(new CoffeePurchased(DateTime.UtcNow));
        }

        public void ClaimFreeCoffee()
        {
            if (FreeCoffeesClaimed.Count >= FreeCoffeesAwarded.Count)
            {
                throw new InvalidOperationException("No free coffees available");
            }

            FreeCoffeeClaimed.Invoke(new FreeCoffeeClaimed(DateTime.UtcNow));
        }

        private void OnCoffeePurchased(CoffeePurchased evnt)
        {
            PurchasedCoffees.Add(evnt);
        }

        private void OnFreeCoffeeAwarded(FreeCoffeeAwarded evnt)
        {
            FreeCoffeesAwarded.Add(evnt);
        }

        private void OnFreeCoffeeClaimed(FreeCoffeeClaimed evnt)
        {
            FreeCoffeesClaimed.Add(evnt);
        }
    }
}
