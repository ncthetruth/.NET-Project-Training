using Contracts.RequestModel.BookTicket;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Services.Validator.BookedTicket
{
    public class DeleteBookTicketValidator :  AbstractValidator<DeleteBookTicketRequest>
    {
        private readonly DBContext _db;

        public DeleteBookTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(q => q.qty)
                .NotEmpty().WithMessage("Quantity cannot be empty.")
                .MustAsync(BeAvailableQuantity).WithMessage("Quantity exceeds available quantity.")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());
        }

        public async Task<bool> BeAvailableQuantity(DeleteBookTicketRequest request, int qty, CancellationToken cancellationToken)
        {
            var bookedTicket = await _db.BookTickets
                .FirstOrDefaultAsync(b => b.BookCode == request.BookCode && b.TicketCode == request.TicketCode, cancellationToken);

            if (bookedTicket != null)
            {
                return bookedTicket.Quantity >= qty;
            }

            return false;
        }
    }
}
