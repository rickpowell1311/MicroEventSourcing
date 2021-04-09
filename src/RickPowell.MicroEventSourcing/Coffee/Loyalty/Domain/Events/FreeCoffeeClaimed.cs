using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain.Events
{
    public class FreeCoffeeClaimed
    {
        public DateTime ClaimedOn { get; protected set; }

        public FreeCoffeeClaimed(DateTime claimedOn)
        {
            ClaimedOn = claimedOn;
        }
    }
}
