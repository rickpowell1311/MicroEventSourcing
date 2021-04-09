using MediatR;
using MediatR.SimpleInjector;
using RickPowell.MicroEventSourcing.Coffee;
using RickPowell.MicroEventSourcing.Coffee.Loyalty.Requests;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
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
                    NumberOfCoffees = 10,
                    CustomerName = "Rick Powell"
                };

                await mediator.Send(buyCoffeeRequest);

                Console.WriteLine($"{buyCoffeeRequest.CustomerName} bought {buyCoffeeRequest.NumberOfCoffees} coffees");

                var loyaltyCard = await mediator.Send(new GetLoyaltyCard.Request
                {
                    CustomerName = buyCoffeeRequest.CustomerName
                });

                Console.WriteLine($"{buyCoffeeRequest.CustomerName} can get {loyaltyCard.FreeCoffeesDue} free coffee(s)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong :(. {ex.Message}.");
            }
        }
    }
}
