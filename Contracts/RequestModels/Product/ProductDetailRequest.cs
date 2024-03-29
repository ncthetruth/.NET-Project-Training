using MediatR;
using Contracts.ResponseModels.Product;

namespace Contracts.RequestModels.Product
{
    public class ProductDetailRequest : IRequest<ProductDetailResponse>
    {
        public Guid? ProductId { get; set; }
    }
}
