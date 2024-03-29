using Contracts.RequestModels.Cart;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
namespace Services.Validators.Cart
{
    public class CreateCartValidator : AbstractValidator<CreateCartRequest>
    {
        private readonly DBContext _db;

        public CreateCartValidator(DBContext db)
        {
            _db = db;

            RuleFor(Q => Q.ProductID)
                .NotEmpty().WithMessage("ProductID cannot be empty.")
                .MustAsync(BeAvailableProductId).WithMessage("ProductID does not exist in the database.");


            RuleFor(Q => Q.CustomerID)
                .NotEmpty().WithMessage("CustomerID cannot be empty.")
                .MustAsync(BeAvailableCustomerId).WithMessage("CustomerID does not exist in the database.");

            RuleFor(Q => Q.Quantity)
                .NotEmpty().WithMessage("Quantity cannot be empty.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }

        public async Task<bool> BeAvailableProductId(Guid productId, CancellationToken cancellationToken)
        {
            var isProductExist = await _db.Products
                .AnyAsync(p => p.ProductID == productId, cancellationToken);

            return isProductExist;
        }

        public async Task<bool> BeAvailableCustomerId(Guid customerId, CancellationToken cancellationToken)
        {
            var isCustomerExist = await _db.Customers
                .AnyAsync(c => c.CustomerID == customerId, cancellationToken);

            return isCustomerExist;
        }
    }
}
