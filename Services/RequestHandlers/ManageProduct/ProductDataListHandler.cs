using Contracts.RequestModels.Product;
using Contracts.ResponseModels.Product;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers.ManageProduct
{
    public class ProductListHandler : IRequestHandler<ProductDataListRequest, ProductDataListResponse>
    {
        private readonly DBContext _db;
        public ProductListHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<ProductDataListResponse> Handle(ProductDataListRequest request, CancellationToken cancellationToken)
        {
            List<ProductDataModel> productList = await _db.Products.Select(Q => new ProductDataModel
            {
                ProductId = Q.ProductID,
                Name = Q.Name,
                Price = Q.Price,
            }).AsNoTracking().ToListAsync(cancellationToken);

            return new ProductDataListResponse { productList = productList };
        }
    }
}
