using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain
{
    public class Purchase : IEquatable<Purchase>
    {
        public int NumberOfCoffees { get; set; }

        public DateTime PurhcasedOn { get; }

        public Purchase(int numberOfCoffees, DateTime purchasedOn)
        {
            NumberOfCoffees = numberOfCoffees;
            PurhcasedOn = purchasedOn;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Purchase);
        }

        public bool Equals(Purchase other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NumberOfCoffees, PurhcasedOn);
        }

        public static bool operator ==(Purchase first, Purchase second)
        {
            return first?.NumberOfCoffees == second?.NumberOfCoffees 
                && first?.PurhcasedOn == second?.PurhcasedOn;
        }

        public static bool operator !=(Purchase first, Purchase second)
        {
            return !(first == second);
        }
    }
}
