using Contracts.RequestModel.BookTicket;
using Contracts.ResponseModel.BookTicket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Services.RequestHandler.BookedTicket
{
    public class UpdateBookedTicketHandler : IRequestHandler<UpdateBookTicketRequest, UpdateBookTicketResponse>
    {
        private readonly DBContext _db;

        public UpdateBookedTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<UpdateBookTicketResponse> Handle(UpdateBookTicketRequest request, CancellationToken cancellationToken)
        {
            var existingData = await _db.BookTickets.FindAsync(request.BookCode);
            if (existingData == null)
            {
                return new UpdateBookTicketResponse
                {
                    Success = false,
                    Message = "Data Not Found"
                };
            }

            existingData.Quantity = request.Quantity;
            existingData.TicketCode = request.TicketCode;
            string msgResult = "";

            if (existingData.Quantity == 0)
            {
                _db.BookTickets.Remove(existingData);
                msgResult = "Data Removed (Quantity = 0)";
            }
            else
            {
                _db.BookTickets.Update(existingData);
                msgResult = "Quantity Updated";
            }

            await _db.SaveChangesAsync(cancellationToken);

            return new UpdateBookTicketResponse
            {
                Success = true,
                Message = msgResult
            };
        }
    }
}

