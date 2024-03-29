using MediatR;
using Contracts.ResponseModels.Product;

namespace Contracts.RequestModels.Product
{
    public class DeleteAllProductRequest : IRequest<DeleteAllProductResponse>
    {
    }
}
