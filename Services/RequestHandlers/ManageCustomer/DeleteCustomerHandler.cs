using Contracts.RequestModels.Customer;
using Contracts.ResponseModels.Customer;
using Entity.Entity;
using MediatR;

namespace Services.RequestHandlers.ManageCustomer
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerRequest, DeleteCustomerResponse>
    {
        private readonly DBContext _db;

        public DeleteCustomerHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteCustomerResponse> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            var existingData = await _db.Customers.FindAsync(request.CustomerId);
            if (existingData == null)
            {
                return new DeleteCustomerResponse()
                {
                    Success = false,
                    Message = "Data Not Found"
                };
            }
            _db.Customers.Remove(existingData);
            await _db.SaveChangesAsync(cancellationToken);
            return new DeleteCustomerResponse()
            {
                Success = true,
                Message = "Deleted"

            };
        }
    }
}
