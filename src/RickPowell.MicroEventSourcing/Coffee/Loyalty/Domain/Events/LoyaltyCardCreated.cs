using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain.Events
{
    public record LoyaltyCardCreated
    {
        public string CustomerName { get; init; }

        public DateTime CreatedOn { get; init; }

        public LoyaltyCardCreated(string customerName, DateTime createdOn)
        {
            CustomerName = customerName;
            CreatedOn = createdOn;
        }
    }
}
