using Contracts.RequestModels.Cart;
using Entity.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Services.Validators.Cart
{
    public class CartDetailValidator : AbstractValidator<CartDetailRequest>
    {
        private readonly DBContext _db;

        public CartDetailValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.CartID)
                .NotEmpty().WithMessage("ID Can't be Empty")
                .MustAsync((id, cancellationToken) => BeAvailableId(id, cancellationToken))
                .WithMessage("ID not Exist");
        }
        public async Task<bool> BeAvailableId(Guid? id, CancellationToken cancellationToken)
        {
            if (id == null)
                return false;

            var existingId = await _db.Carts
                .AsNoTracking()
                .AnyAsync(Q => Q.CartID == id, cancellationToken);

            return existingId;
        }
    }
}
