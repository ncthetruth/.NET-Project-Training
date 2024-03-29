using Contracts.RequestModels.Customer;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators.Customer
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerDataRequest>
    {
        private readonly DBContext _db;

        public UpdateCustomerValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.Name).NotEmpty().WithMessage("Name Can't be Empty")
                .MaximumLength(50).WithMessage("Max 50 Characters");

            RuleFor(Q => Q.Email).EmailAddress()
                .NotEmpty().WithMessage("Email Can't be Empty")
                .MustAsync(BeAvailableEmail).WithMessage("Email Already Exist");
        }
        public async Task<bool> BeAvailableEmail(string email, CancellationToken cancellationToken)
        {
            var existingEmail = await _db.Customers.Where(Q => Q.Email == email)
                .AsNoTracking().AnyAsync();

            return !existingEmail;
        }
    }
}
