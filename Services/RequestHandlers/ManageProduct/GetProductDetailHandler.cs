using Contracts.ResponseModels.Product;
using Contracts.RequestModels.Product;
using Entity.Entity;
using MediatR;

namespace Services.RequestHandlers.ManageProduct
{
    public class GetProductDetailHandler : IRequestHandler<ProductDetailRequest, ProductDetailResponse>
    {
        private readonly DBContext _db;

        public GetProductDetailHandler(DBContext db)
        {
            _db = db;
        }
        public async Task<ProductDetailResponse> Handle(ProductDetailRequest request, CancellationToken cancellationToken)
        {
            var existingDatas = await _db.Products.FindAsync(request.ProductId);

            var data = new ProductDetailResponse
            {
                ProductId = existingDatas.ProductID,
                Name = existingDatas.Name,
                Price = existingDatas.Price

            };
            return data;


        }
    }
}
