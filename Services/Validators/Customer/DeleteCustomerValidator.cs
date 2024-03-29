using Contracts.RequestModels.Customer;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators.Customer
{
    public class DeleteCustomerValidator : AbstractValidator<DeleteCustomerRequest>
    {
        private readonly DBContext _db;

        public DeleteCustomerValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.CustomerId)
                .NotEmpty().WithMessage("ID Can't be Empty")
                .MustAsync(BeAvailableId).WithMessage("ID not Exist");
        }

        public async Task<bool> BeAvailableId(Guid? id, CancellationToken cancellationToken)
        {
            var existingId = await _db.Customers.Where(Q => Q.CustomerID == id)
                .AsNoTracking().AnyAsync();

            return existingId;
        }
    }
}
