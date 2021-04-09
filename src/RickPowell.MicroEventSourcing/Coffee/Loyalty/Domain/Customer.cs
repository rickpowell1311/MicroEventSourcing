using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain
{
    public class Customer : IEquatable<Customer>
    {
        public string Name { get; protected set; }

        public Customer(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is Customer customer && Equals(customer);
        }

        public bool Equals(Customer other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public static bool operator ==(Customer first, Customer second)
        {
            return first?.Name == second?.Name;
        }

        public static bool operator !=(Customer first, Customer second)
        {
            return !(first == second);
        }
    }
}
