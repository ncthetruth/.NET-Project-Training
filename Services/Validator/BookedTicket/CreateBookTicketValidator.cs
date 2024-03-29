using Contracts.RequestModel.BookTicket;
using Entity.Entity;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Validator.BookedTicket
{
    public class CreateBookTicketValidator : AbstractValidator<CreateBookTicketRequest>
    {
        private readonly DBContext _db;

        public CreateBookTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(q => q.Quantity).NotEmpty().WithMessage("Quantity cannot be empty.");

            RuleFor(q => q.TicketCode)
                .NotEmpty().WithMessage("Ticket code cannot be empty.")
                .MustAsync(BeAvailableTicket).WithMessage("Ticket not found.")
                .WithErrorCode(HttpStatusCode.NotFound.ToString());
        }

        public async Task<bool> BeAvailableTicket(string ticketCode, CancellationToken cancellationToken)
        {
            var isTicketExist = await _db.AvailableTickets
                .AnyAsync(q => q.TicketCode == ticketCode, cancellationToken);

            return isTicketExist;
        }
        public override async Task<ValidationResult> ValidateAsync(ValidationContext<CreateBookTicketRequest> context, CancellationToken cancellation = default)
        {
            var validationResult = await base.ValidateAsync(context, cancellation);
            if (validationResult.IsValid)
            {
                var request = context.InstanceToValidate;

                var isDataExist = await _db.BookTickets.AnyAsync(b => b.TicketCode == request.TicketCode, cancellation);
                if (isDataExist)
                {
                    validationResult.Errors.Add(new ValidationFailure("", "Data already exists.")
                    {
                        ErrorCode = HttpStatusCode.Conflict.ToString(),
                        ErrorMessage = "Data already exists."
                    });
                }
            }

            return validationResult;
        }
    }
}
