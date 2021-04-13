using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain.Events
{
    public record FreeCoffeeClaimed
    {
        public DateTime ClaimedOn { get; init; }

        public FreeCoffeeClaimed(DateTime claimedOn) => ClaimedOn = claimedOn;
    }
}
