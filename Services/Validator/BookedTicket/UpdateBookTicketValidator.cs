using Contracts.RequestModel.BookTicket;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Validator.BookedTicket
{
    public class UpdateBookTicketValidator : AbstractValidator<UpdateBookTicketRequest>
    {
        private readonly DBContext _db;

        public UpdateBookTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(q => q.BookCode)
                .NotEmpty().WithMessage("Book code cannot be empty.")
                .MustAsync(BeAvailableBook).WithMessage("Book not found.")
                .WithErrorCode(HttpStatusCode.NotFound.ToString());
        }

        public async Task<bool> BeAvailableBook(string bookCode, CancellationToken cancellationToken)
        {
            var isBookExist = await _db.BookTickets
                .AnyAsync(q => q.BookCode == bookCode, cancellationToken);

            return isBookExist;
        }
    }
}
