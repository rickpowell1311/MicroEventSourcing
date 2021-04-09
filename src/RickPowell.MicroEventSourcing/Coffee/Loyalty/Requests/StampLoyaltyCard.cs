using MediatR;
using Microsoft.EntityFrameworkCore;
using RickPowell.MicroEventSourcing.Coffee.Loyalty.Data;
using RickPowell.MicroEventSourcing.Coffee.Loyalty.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RickPowell.MicroEventSourcing.Coffee.Loyalty.Requests
{
    public static class StampLoyaltyCard
    {
        public class Request : IRequest
        {
            public string CustomerName { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly LoyaltyContext _context;

            public Handler(LoyaltyContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var customer = new Customer(request.CustomerName);

                var loyaltyCard = await _context.LoyaltyCards
                    .SingleOrDefaultAsync(x => x.Customer.Name == request.CustomerName, cancellationToken);

                if (loyaltyCard == null)
                {
                    loyaltyCard = LoyaltyCard.Create(customer);
                    _context.LoyaltyCards.Add(loyaltyCard);
                }

                loyaltyCard.PurchaseCoffee();

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
