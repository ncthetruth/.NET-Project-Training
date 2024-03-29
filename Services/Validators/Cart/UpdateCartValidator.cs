using Contracts.RequestModels.Cart;
using Entity.Entity;
using FluentValidation;

namespace Services.Validators.Cart
{
    public class UpdateCartValidator : AbstractValidator<UpdateCartDataRequest>
    {
        private readonly DBContext _db;

        public UpdateCartValidator(DBContext db)
        {
            _db = db;
            RuleFor(Q => Q.CartID).NotEmpty().WithMessage("Name Can't be Empty");
        }
    }
}
