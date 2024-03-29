using Contracts.ResponseModels.Customer;
using Contracts.RequestModels.Customer;
using MediatR;
using Entity.Entity;

namespace Services.RequestHandlers.ManageCustomer
{
    public class UpdateCustomerDataHandler : IRequestHandler<UpdateCustomerDataRequest, UpdateCustomerDataResponse>
    {
        private readonly DBContext _db;

        public UpdateCustomerDataHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<UpdateCustomerDataResponse> Handle(UpdateCustomerDataRequest request, CancellationToken cancellationToken)
        {
            var existingData = await _db.Customers.FindAsync(request.CustomerId);
            if (existingData == null)
            {
                return new UpdateCustomerDataResponse()
                {
                    Success = false,
                    Message = "Data Not Found"
                };
            }
            existingData.Name = request.Name;
            existingData.Email = request.Email;
            _db.Customers.Update(existingData);
            await _db.SaveChangesAsync(cancellationToken);
            return new UpdateCustomerDataResponse()
            {
                Success = true,
                Message = "Uploaded"

            };
        }
    }
}
