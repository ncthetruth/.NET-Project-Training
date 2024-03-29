using Contracts.RequestModel.GetAvailableTicket;
using Entity.Entity;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace Services.Validator.GetAvailableTicket
{
    public class CreateAvailableTicketValidator : AbstractValidator<CreateAvailableTicketRequest>
    {
        private readonly DBContext _db;

        public CreateAvailableTicketValidator(DBContext db)
        {
            _db = db;

            RuleFor(q => q.TicketName)
                .NotEmpty().WithMessage("Ticket Name cannot be empty.")
                .MaximumLength(100).WithMessage("Ticket Name cannot exceed 100 characters.");

            RuleFor(q => q.CategoryName)
                .NotEmpty().WithMessage("Category Name cannot be empty.")
                .MaximumLength(100).WithMessage("Category Name cannot exceed 100 characters.");

            RuleFor(q => q.Quota)
                .NotEmpty().WithMessage("Quota cannot be empty.")
                .GreaterThan(0).WithMessage("Quota must be greater than 0.");

            RuleFor(q => q.Price)
                .NotEmpty().WithMessage("Price cannot be empty.");
        }

        public override async Task<ValidationResult> ValidateAsync(ValidationContext<CreateAvailableTicketRequest> context, CancellationToken cancellationToken = default)
        {
            var validationResult = await base.ValidateAsync(context, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            var request = context.InstanceToValidate;

            var existingTicket = await _db.AvailableTickets
                .FirstOrDefaultAsync(t =>
                    t.TicketName == request.TicketName &&
                    t.CategoryName == request.CategoryName,
                    cancellationToken);

            if (existingTicket != null)
            {
                validationResult.Errors.Add(new ValidationFailure("", "Ticket with same name and category already exists.")
                {
                    ErrorCode = HttpStatusCode.Conflict.ToString(),
                    Severity = Severity.Error
                });
            }

            return validationResult;
        }
    }
}
