using Contracts.RequestModel.BookTicket;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Services.Validator.BookedTicket
{
    public class GetBookTicketValidator : AbstractValidator<GetBookTicketRequest>
    {
        private readonly DBContext _db;

        public GetBookTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(q => q.BookId)
                .NotEmpty().WithMessage("Book cannot be empty.")
                .MustAsync(BeAvailableBook).WithMessage("Book not found.")
                .WithErrorCode(HttpStatusCode.NotFound.ToString());
        }

        public async Task<bool> BeAvailableBook(string bookCode, CancellationToken cancellationToken)
        {
            var isBookExist = await _db.BookTickets
                .Where(q => q.BookCode == bookCode)
                .AsNoTracking()
                .AnyAsync(cancellationToken);

            return isBookExist;
        }
    }
}
