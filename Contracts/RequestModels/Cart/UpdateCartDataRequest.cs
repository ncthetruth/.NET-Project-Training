using Contracts.ResponseModels.Cart;
using MediatR;

namespace Contracts.RequestModels.Cart
{
    public class UpdateCartDataRequest : UpdateCartDataModel, IRequest<UpdateCartDataResponse>
    {
        public Guid? CartID { get; set; }
    }

    public class UpdateCartDataModel
    {
        public int Quantity { get; set; }
    }
}
