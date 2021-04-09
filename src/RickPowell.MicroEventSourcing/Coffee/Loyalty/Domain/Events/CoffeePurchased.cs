using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain.Events
{
    public class CoffeePurchased
    {
        public DateTime PurchasedOn { get; protected set; }

        public CoffeePurchased(DateTime purchasedOn)
        {
            PurchasedOn = purchasedOn;
        }
    }
}
