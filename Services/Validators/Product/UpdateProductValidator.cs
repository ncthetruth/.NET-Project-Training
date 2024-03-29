using Contracts.RequestModels.Product;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators.Product
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        private readonly DBContext _db;
        public UpdateProductValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.ProductID).NotEmpty().MustAsync(ProductIDExists).WithMessage("This product ID was not found.");
            RuleFor(Q=>Q.Name).NotEmpty().MaximumLength(50);
            RuleFor(Q => Q.Price).NotEmpty().GreaterThan(0);
        }

        public async Task<bool> ProductIDExists(Guid id, CancellationToken cancellationToken)
        {
            bool IsExist = await _db.Products.Where(Q=>Q.ProductID == id).AsNoTracking().AnyAsync(cancellationToken);
            return IsExist;
        }
    }
}
