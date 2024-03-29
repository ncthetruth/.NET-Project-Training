using Contracts.RequestModels.Cart;
using Contracts.ResponseModels.Cart;
using Entity.Entity;
using MediatR;

namespace Services.RequestHandlers.ManageCart
{
    public class CreateCartHandler : IRequestHandler<CreateCartRequest, CreateCartResponse>
    {
        private readonly DBContext _db;

        public CreateCartHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CreateCartResponse> Handle(CreateCartRequest request, CancellationToken cancellationToken)
        {
            var cart = new Cart
            {
                CartID = Guid.NewGuid(),
                CustomerID = request.CustomerID,
                ProductID = request.ProductID,
                Quantity = request.Quantity
            };

            _db.Carts.Add(cart);
            await _db.SaveChangesAsync(cancellationToken);

            var response = new CreateCartResponse
            {
                CartID = cart.CartID,
            };

            return response;
        }
    }
}
