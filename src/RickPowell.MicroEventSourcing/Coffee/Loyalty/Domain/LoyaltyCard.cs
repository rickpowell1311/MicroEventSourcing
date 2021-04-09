using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain
{
    public class LoyaltyCard
    {
        public long Id { get; protected set; }

        public Customer Customer { get; protected set; }

        public int TotalCoffeesPurchased { get; protected set; }

        public int FreeCoffeesDue { get; protected set; }

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

        public void Stamp(Purchase purchase)
        {
            var newTotalCoffeesPurchased = TotalCoffeesPurchased + purchase.NumberOfCoffees;

            if (newTotalCoffeesPurchased / 10 != TotalCoffeesPurchased / 10)
            {
                FreeCoffeesDue += 1;
            }

            TotalCoffeesPurchased = newTotalCoffeesPurchased;
        }

        public void Claim()
        {
            if (FreeCoffeesDue < 1)
            {
                throw new ArgumentException("Cannot claim a free coffee yet :(");
            }

            FreeCoffeesDue -= 1;
            FreeCoffeesClaimed += 1;
        }
    }
}
