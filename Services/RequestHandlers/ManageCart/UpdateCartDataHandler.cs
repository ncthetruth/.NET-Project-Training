using Contracts.ResponseModels.Cart;
using Contracts.RequestModels.Cart;
using MediatR;
using Entity.Entity;

namespace Services.RequestHandlers.ManageCart
{
    public class UpdateCartDataHandler : IRequestHandler<UpdateCartDataRequest, UpdateCartDataResponse>
    {
        private readonly DBContext _db;

        public UpdateCartDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<UpdateCartDataResponse> Handle(UpdateCartDataRequest request, CancellationToken cancellationToken)
        {
            var existingData = await _db.Carts.FindAsync(request.CartID);
            if (existingData == null)
            {
                return new UpdateCartDataResponse
                {
                    Success = false,
                    Message = "Data Not Found"
                };
            }

            existingData.Quantity = request.Quantity;
            string msgResult = "";

            if (existingData.Quantity == 0)
            {
                _db.Carts.Remove(existingData);
                msgResult = "Data Removed (Quantity = 0)";
            }
            else
            {
                _db.Carts.Update(existingData);
                msgResult = "Quantity Updated";
            }

            await _db.SaveChangesAsync(cancellationToken);

            return new UpdateCartDataResponse
            {
                Success = true,
                Message = msgResult
            };
        }
    }
}
