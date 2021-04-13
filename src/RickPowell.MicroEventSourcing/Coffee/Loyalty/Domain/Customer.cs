using System;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain
{
    public record Customer
    {
        public string Name { get; init; }

        public Customer(string name) => Name = name;
    }
}
