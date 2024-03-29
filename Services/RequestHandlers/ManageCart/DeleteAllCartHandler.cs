using Contracts.RequestModels.Cart;
using Contracts.ResponseModels.Cart;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers.ManageCart
{
    public class DeleteAllCartHandler : IRequestHandler<DeleteAllCartRequest, DeleteAllCartResponse>
    {
        private readonly DBContext _db;

        public DeleteAllCartHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteAllCartResponse> Handle(DeleteAllCartRequest request, CancellationToken cancellationToken)
        {
            var allData = await _db.Carts.ToListAsync(cancellationToken);

            if (allData == null || !allData.Any())
            {
                return new DeleteAllCartResponse()
                {
                    Success = false,
                    Message = "No data found to delete"
                };
            }

            _db.Carts.RemoveRange(allData);
            await _db.SaveChangesAsync(cancellationToken);

            return new DeleteAllCartResponse()
            {
                Success = true,
                Message = "All data deleted"
            };
        }
    }
}
