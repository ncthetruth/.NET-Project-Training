using Contracts.ResponseModels.Cart;
using MediatR;

namespace Contracts.RequestModels.Cart
{
    public class DeleteAllCartRequest : IRequest<DeleteAllCartResponse>
    {
    }
}
