using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain.Events
{
    public class LoyaltyCardCreated
    {
        public Customer Customer { get; }

        public DateTime CreatedOn { get; }

        public LoyaltyCardCreated(Customer customer, DateTime createdOn)
        {
            Customer = customer;
            CreatedOn = createdOn;
        }
    }
}
