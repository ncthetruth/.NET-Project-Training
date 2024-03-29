using Contracts.ResponseModels.Cart;
using Contracts.RequestModels.Cart;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers.ManageCart
{
    public class GetCartDetailHandler : IRequestHandler<CartDetailRequest, CartDetailResponse>
    {
        private readonly DBContext _db;

        public GetCartDetailHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CartDetailResponse> Handle(CartDetailRequest request, CancellationToken cancellationToken)
        {
            var existingData = await _db.Carts
                .Include(c => c.Customer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(c => c.CartID == request.CartID);

            var datas = new List<CartData>
            {
                new CartData
                {
                    CartID = existingData.CartID,
                    CustomerName = existingData.Customer.Name,
                    ProductName = existingData.Product.Name,
                    Quantity = existingData.Quantity,
                    Price = existingData.Product.Price,
                    SubTotal = existingData.Quantity * existingData.Product.Price
                }
            };

            var response = new CartDetailResponse
            {
                CartDatas = datas
            };

            return response;
        }
    }
}
