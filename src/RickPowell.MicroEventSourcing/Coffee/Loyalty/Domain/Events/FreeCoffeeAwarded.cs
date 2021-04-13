using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain.Events
{
    public class FreeCoffeeAwarded
    {
        public DateTime AwardedOn { get; init; }

        public FreeCoffeeAwarded(DateTime awardedOn) => AwardedOn = awardedOn;
    }
}
