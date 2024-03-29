
using Contracts.ResponseModels.Product;
using MediatR;

namespace Contracts.RequestModels.Product
{
    public class UpdateProductRequest : UpdateProductModel, IRequest<UpdateProductResponse>
    {
        public Guid ProductID { get; set; }
        
    }
    public class UpdateProductModel
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
