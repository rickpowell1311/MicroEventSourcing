using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain.Events
{
    public record CoffeePurchased
    {
        public DateTime PurchasedOn { get; init; }

        public CoffeePurchased(DateTime purchasedOn) => PurchasedOn = purchasedOn;
    }
}
