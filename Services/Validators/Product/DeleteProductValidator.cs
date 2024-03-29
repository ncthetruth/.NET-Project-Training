using Contracts.RequestModels.Product;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators.Product
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductRequest>
    {
        private readonly DBContext _db;

        public DeleteProductValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.ProductId)
                .NotEmpty().WithMessage("ID Can't be Empty")
                .MustAsync(BeAvailableId).WithMessage("ID not Exist");
        }

        public async Task<bool> BeAvailableId(Guid? id, CancellationToken cancellationToken)
        {
            var existingId = await _db.Products.Where(Q => Q.ProductID == id)
                .AsNoTracking().AnyAsync();

            return existingId;
        }
    }
}
