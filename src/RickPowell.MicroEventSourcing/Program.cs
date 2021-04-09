using MediatR;
using MediatR.SimpleInjector;
using RickPowell.MicroEventSourcing.Coffee;
using RickPowell.MicroEventSourcing.Coffee.Loyalty.Requests;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RickPowell.MicroEventSourcing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.BuildMediator(typeof(Program).Assembly);
            container.RegisterCoffeeModule();
            container.Verify();

            using var scope = AsyncScopedLifestyle.BeginScope(container);
            var mediator = scope.GetInstance<IMediator>();

            try
            {
                var buyCoffeeRequest = new StampLoyaltyCard.Request
                {
                    CustomerName = "Rick Powell"
                };

                for (int i = 0; i < 10; i++)
                {
                    // buy 10 coffees
                    await mediator.Send(buyCoffeeRequest);
                }

                var loyaltyCard = await mediator.Send(new GetLoyaltyCard.Request
                {
                    CustomerName = buyCoffeeRequest.CustomerName
                });

                Console.WriteLine($"{buyCoffeeRequest.CustomerName} bought {loyaltyCard.PurchasedCoffees} coffee(s) and has been awarded {loyaltyCard.FreeCoffeesAwarded} free coffee(s) and has claimed {loyaltyCard.FreeCoffeesClaimed} coffee(s)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong :(. {ex.Message}.");
            }
        }
    }
}
