using Contracts.RequestModel.BookTicket;
using Contracts.ResponseModel.BookTicket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandler.BookedTicket
{
    public class GetBookedTicketHandler : IRequestHandler<GetBookTicketRequest, GetBookTicketResponse>
    {
        private readonly DBContext _db;

        public GetBookedTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<GetBookTicketResponse> Handle(GetBookTicketRequest request, CancellationToken cancellationToken)
        {
            var bookedTickets = await _db.BookTickets
                .Include(bt => bt.availableTicket)
                .Where(bt => bt.BookCode == request.BookId)
                .ToListAsync(cancellationToken);

            var response = new GetBookTicketResponse();

            foreach (var bookedTicket in bookedTickets)
            {
                var availableTicket = await _db.AvailableTickets
                    .FirstOrDefaultAsync(t => t.TicketCode == bookedTicket.TicketCode, cancellationToken);

                if (availableTicket != null)
                {
                    var ticketCategory = response.TicketCategories.FirstOrDefault(tc => tc.CategoryName == availableTicket.CategoryName);
                    if (ticketCategory == null)
                    {
                        ticketCategory = new TicketCategory
                        {
                            CategoryName = availableTicket.CategoryName,
                            QtyPerCategory = bookedTicket.Quantity,
                            Tickets = new List<Ticket>()
                        };
                        response.TicketCategories.Add(ticketCategory);
                    }

                    ticketCategory.Tickets.Add(new Ticket
                    {
                        TicketCode = availableTicket.TicketCode,
                        TicketName = availableTicket.TicketName,
                        EventDate = availableTicket.EventDate
                    });
                }
            }

            return response;
        }
    }
}
