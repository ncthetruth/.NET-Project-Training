using Contracts.RequestModel.BookTicket;
using Contracts.ResponseModel.BookTicket;
using Contracts.ResponseModel.GetAvailableTicket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandler.BookedTicket
{
    public class CreateBookedTicketHandler : IRequestHandler<CreateBookTicketRequest, CreateBookTicketResponse>
    {
        private readonly DBContext _db;

        public CreateBookedTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CreateBookTicketResponse> Handle(CreateBookTicketRequest request, CancellationToken cancellationToken)
        {
            var availableTicket = await _db.AvailableTickets
                                .FirstOrDefaultAsync(t => t.TicketCode == request.TicketCode);

            var bookedItem = new BookTicket
            {
                BookCode = request.TicketCode + "Book",
                TicketCode = request.TicketCode,
                Quantity = request.Quantity
            };

            _db.BookTickets.Add(bookedItem);
            await _db.SaveChangesAsync(cancellationToken);

            decimal summaryPrice = availableTicket.Price * request.Quantity;

            var response = new CreateBookTicketResponse
            {
                AvailableTickets = new List<AvailableTicketData>
        {
            new AvailableTicketData
            {
                TicketCode = availableTicket.TicketCode,
                EventDate = availableTicket.EventDate,
                Quota = availableTicket.Quota,
                TicketName = availableTicket.TicketName,
                CategoryName = availableTicket.CategoryName,
                Price = availableTicket.Price
            }
        },
                SummaryPrice = summaryPrice,
                BookCode = bookedItem.BookCode
            };

            return response;
        }

    }
}
