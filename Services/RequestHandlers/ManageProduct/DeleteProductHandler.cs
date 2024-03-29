using Contracts.RequestModels.Product;
using Contracts.ResponseModels.Product;
using Entity.Entity;
using MediatR;

namespace Services.RequestHandlers.ManageProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, DeleteProductResponse>
    {
        private readonly DBContext _db;

        public DeleteProductHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteProductResponse> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var existingData = await _db.Products.FindAsync(request.ProductId);
            if (existingData == null)
            {
                return new DeleteProductResponse()
                {
                    Success = false,
                    Message = "Data Not Found"
                };
            }
            _db.Products.Remove(existingData);
            await _db.SaveChangesAsync(cancellationToken);
            return new DeleteProductResponse()
            {
                Success = true,
                Message = "Deleted"

            };
        }
    }
}
