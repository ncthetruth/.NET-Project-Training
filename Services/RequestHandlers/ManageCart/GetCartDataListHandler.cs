using Contracts.RequestModels.Cart;
using Contracts.ResponseModels.Cart;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Services.RequestHandlers.ManageCart
{
    public class GetCardDataListHandler : IRequestHandler<CartDataListRequest, CartDataListResponse>
    {
        private readonly DBContext _db;

        public GetCardDataListHandler(DBContext db) 
        {
            _db = db;
        }
        public async Task<CartDataListResponse> Handle(CartDataListRequest request, CancellationToken cancellationToken)
        {
            var datas = await _db.Carts
                .Include(c => c.Customer)
                .Include(p => p.Product)
                .Select(Q => new CartData
                {
                    CartID = Q.CartID,
                    CustomerName = Q.Customer.Name,
                    ProductName = Q.Product.Name,
                    Quantity = Q.Quantity,
                    Price = Q.Product.Price,
                    SubTotal = Q.Quantity * Q.Product.Price
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var response = new CartDataListResponse
            {
                CartDatas = datas
            };

            return response;
        }
    }
}
