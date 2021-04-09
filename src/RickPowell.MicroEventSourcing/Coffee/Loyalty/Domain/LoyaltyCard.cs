using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain
{
    public class LoyaltyCard
    {
        public long Id { get; protected set; }

        public Customer Customer { get; protected set; }

        public int PurchasedCoffees { get; protected set; }

        public int FreeCoffeesAwarded { get; protected set; }

        public int FreeCoffeesClaimed { get; protected set; }

        protected LoyaltyCard()
        {
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
            var newTotalCoffeesPurchased = PurchasedCoffees + 1;

            if (newTotalCoffeesPurchased / 10 != PurchasedCoffees / 10)
            {
                FreeCoffeesAwarded += 1;
            }

            PurchasedCoffees = newTotalCoffeesPurchased;
        }

        public void Claim()
        {
            if (FreeCoffeesAwarded <= FreeCoffeesClaimed)
            {
                throw new ArgumentException("Cannot claim a free coffee yet :(");
            }

            FreeCoffeesClaimed += 1;
        }
    }
}
