using Contracts.RequestModels.Product;
using Contracts.ResponseModels.Product;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers.ManageProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
    {
        private readonly DBContext _db;
        public UpdateProductHandler(DBContext db)
        {
            _db = db;
        }
        public async Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            Product? selectedData = await _db.Products.Where(Q => Q.ProductID == request.ProductID).Select(Q => Q).FirstOrDefaultAsync(cancellationToken);

            if (selectedData == null)
            {
                return new UpdateProductResponse
                {
                    Success = false,
                    Message = "Data not found.",
                };
            }

            selectedData.Name = request.Name;
            selectedData.Price = request.Price;
            await _db.SaveChangesAsync(cancellationToken);

            return new UpdateProductResponse
            {
                Success = true,
                Message = "Data updated.",
            };
        }
    }
}
