using Contracts.ResponseModels.Cart;
using MediatR;

namespace Contracts.RequestModels.Cart
{
    public class CreateCartRequest : IRequest<CreateCartResponse>
    {
        public Guid CustomerID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
