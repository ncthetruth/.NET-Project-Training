using Contracts.RequestModels.Customer;
using Contracts.RequestModels.Product;
using Contracts.ResponseModels.Customer;
using Contracts.ResponseModels.Product;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers.ManageProduct
{
    public class DeleteAllProductHandler : IRequestHandler<DeleteAllProductRequest, DeleteAllProductResponse>
    {
        private readonly DBContext _db;

        public DeleteAllProductHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteAllProductResponse> Handle(DeleteAllProductRequest request, CancellationToken cancellationToken)
        {
            var allData = await _db.Products.ToListAsync(cancellationToken);

            if (allData == null || !allData.Any())
            {
                return new DeleteAllProductResponse()
                {
                    Success = false,
                    Message = "No data found to delete"
                };
            }

            _db.Products.RemoveRange(allData);
            await _db.SaveChangesAsync(cancellationToken);

            return new DeleteAllProductResponse()
            {
                Success = true,
                Message = "All data deleted"
            };
        }
    }
}
