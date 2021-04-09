using RickPowell.MicroEventSourcing.Coffee.Loyalty;
using SimpleInjector;

namespace RickPowell.MicroEventSourcing.Coffee
{
    public static class SimpleInjectorExtensions
    {
        public static void RegisterCoffeeModule(this Container container)
        {
            container.RegisterLoyaltySubmodule();
        }
    }
}
