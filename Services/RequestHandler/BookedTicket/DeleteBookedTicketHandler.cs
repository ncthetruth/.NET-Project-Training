using Contracts.RequestModel.BookTicket;
using Contracts.ResponseModel.BookTicket;
using Contracts.ResponseModel.GetAvailableTicket;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandler.BookedTicket
{
    public class DeleteBookedTicketHandler : IRequestHandler<DeleteBookTicketRequest, DeleteBookTicketResponse>
    {
        private readonly DBContext _db;

        public DeleteBookedTicketHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteBookTicketResponse> Handle(DeleteBookTicketRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteBookTicketResponse();

            try
            {
                var bookedTicket = await _db.BookTickets
                    .FirstOrDefaultAsync(b => b.BookCode == request.BookCode && b.TicketCode == request.TicketCode, cancellationToken);

                if (bookedTicket != null)
                {
                    bookedTicket.Quantity -= request.qty;

                    if (bookedTicket.Quantity <= 0)
                    {
                        _db.BookTickets.Remove(bookedTicket);
                    }

                    await _db.SaveChangesAsync(cancellationToken);

                    response.Success = true;
                    response.Message = "Booked ticket updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Booked ticket not found.";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
    }
}
