using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain.Events
{
    public record LoyaltyCardCreated
    {
        public Customer Customer { get; init; }

        public DateTime CreatedOn { get; init; }

        public LoyaltyCardCreated(Customer customer, DateTime createdOn)
        {
            Customer = customer;
            CreatedOn = createdOn;
        }
    }
}
