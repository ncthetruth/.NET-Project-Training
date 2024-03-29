
using Contracts.RequestModels.Product;
using Contracts.ResponseModels.Product;
using Entity.Entity;
using MediatR;

namespace Services.RequestHandlers.ManageProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
    {
        private readonly DBContext _db;
        public CreateProductHandler(DBContext dBContext)
        {
            _db = dBContext;
        }

        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            Product data = new Product
            {
                ProductID = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
            };

            _db.Products.Add(data);
            await _db.SaveChangesAsync(cancellationToken);
            return new CreateProductResponse() { ProductId = data.ProductID };
        }
    }
}
