using Contracts.RequestModel.GetAvailableTicket;
using Contracts.ResponseModel.GetAvailableTicket;
using Entity.Entity;
using MediatR;

    namespace Services.RequestHandler.GetAvailableTicket
    {
        public class CreateAvailableTicketHandler : IRequestHandler<CreateAvailableTicketRequest, CreateAvailableTicketResponse>
        {
            private readonly DBContext _db;

            public CreateAvailableTicketHandler(DBContext db)
            {
                _db = db;
            }

            public async Task<CreateAvailableTicketResponse> Handle(CreateAvailableTicketRequest request, CancellationToken cancellationToken)
            {

                var categoryWords = request.CategoryName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var firstWordFirstLetter = char.ToUpper(categoryWords.FirstOrDefault()?.FirstOrDefault() ?? default(char));

                var secondWordFirstLetter = char.ToUpper(categoryWords.Skip(1).FirstOrDefault()?.FirstOrDefault() ?? firstWordFirstLetter);

                var ticketCodePrefix = $"{firstWordFirstLetter}{secondWordFirstLetter}";

                var existingTicketCount = _db.AvailableTickets
                    .Count(t => t.TicketCode.StartsWith(ticketCodePrefix));

                var ticketCodeNumber = existingTicketCount + 1;

                var ticketCode = $"{ticketCodePrefix}{ticketCodeNumber:D3}";

                var availableTicket = new AvailableTicket
                {
                    EventDate = request.EventDate,
                    Quota = request.Quota,
                    TicketName = request.TicketName,
                    TicketCode = ticketCode,
                    CategoryName = request.CategoryName,
                    Price = request.Price
                };

                _db.AvailableTickets.Add(availableTicket);
                await _db.SaveChangesAsync(cancellationToken);

                var response = new CreateAvailableTicketResponse
                {
                    TicketCode = availableTicket.TicketCode,
                };

                return response;
            }
        }
    }
