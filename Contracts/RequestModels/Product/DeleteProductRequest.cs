using MediatR;
using Contracts.ResponseModels.Product;

namespace Contracts.RequestModels.Product
{
    public class DeleteProductRequest : IRequest<DeleteProductResponse>
    {
        public Guid? ProductId { get; set; }
    }
}
