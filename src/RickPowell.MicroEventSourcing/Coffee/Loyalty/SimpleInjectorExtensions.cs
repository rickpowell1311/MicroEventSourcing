using Microsoft.EntityFrameworkCore;
using RickPowell.MicroEventSourcing.Coffee.Loyalty.Data;
using SimpleInjector;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty
{
    public static class SimpleInjectorExtensions
    {
        public static void RegisterLoyaltySubmodule(this Container container)
        {
            var contextBuilder = new DbContextOptionsBuilder<LoyaltyContext>();
            contextBuilder.UseInMemoryDatabase("LoyaltyContext");

            container.Register(() => new LoyaltyContext(contextBuilder.Options), Lifestyle.Scoped);
        }
    }
}
