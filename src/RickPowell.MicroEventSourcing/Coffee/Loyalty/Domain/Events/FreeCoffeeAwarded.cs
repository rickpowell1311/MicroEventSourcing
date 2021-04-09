using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain.Events
{
    public class FreeCoffeeAwarded
    {
        public DateTime AwardedOn { get; protected set; }

        public FreeCoffeeAwarded(DateTime awardedOn)
        {
            AwardedOn = awardedOn;
        }
    }
}
