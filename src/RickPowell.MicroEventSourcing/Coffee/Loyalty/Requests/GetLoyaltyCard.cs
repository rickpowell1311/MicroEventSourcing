using MediatR;
using Microsoft.EntityFrameworkCore;
using RickPowell.MicroEventSourcing.Coffee.Loyalty.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Requests
{
    public static class GetLoyaltyCard
    {
        public class Request : IRequest<Response>
        {
            public string CustomerName { get; set; }
        }

        public class Response
        {
            public int PurchasedCoffees { get; set; }

            public int FreeCoffeesAwarded { get; set; }

            public int FreeCoffeesClaimed { get; set; }

            public static Response Default => new();
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly LoyaltyContext _context;

            public Handler(LoyaltyContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var loyaltyCard = await _context.LoyaltyCardProjections
                    .SingleOrDefaultAsync(x => x.CustomerName == request.CustomerName, cancellationToken);

                if (loyaltyCard == null)
                {
                    return Response.Default;
                }

                return new Response
                {
                    FreeCoffeesClaimed = loyaltyCard.FreeCoffeesClaimed,
                    FreeCoffeesAwarded = loyaltyCard.FreeCoffeesAwarded,
                    PurchasedCoffees = loyaltyCard.PurchasedCoffees
                };
            }
        }
    }
}
