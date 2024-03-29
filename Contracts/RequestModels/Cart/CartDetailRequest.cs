using Contracts.ResponseModels.Cart;
using MediatR;

namespace Contracts.RequestModels.Cart
{
    public class CartDetailRequest : IRequest<CartDetailResponse>
    {
        public Guid CartID { get; set; }
    }
}
