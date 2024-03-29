using Contracts.RequestModels.Customer;
using Contracts.ResponseModels.Cart;
using Contracts.ResponseModels.Customer;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers.ManageCustomer
{
    public class DeleteAllCustomerHandler : IRequestHandler<DeleteAllCustomerRequest, DeleteAllCustomerResponse>
    {
        private readonly DBContext _db;

        public DeleteAllCustomerHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<DeleteAllCustomerResponse> Handle(DeleteAllCustomerRequest request, CancellationToken cancellationToken)
        {
            var allData = await _db.Customers.ToListAsync(cancellationToken);

            if (allData == null || !allData.Any())
            {
                return new DeleteAllCustomerResponse()
                {
                    Success = false,
                    Message = "No data found to delete"
                };
            }

            _db.Customers.RemoveRange(allData);
            await _db.SaveChangesAsync(cancellationToken);

            return new DeleteAllCustomerResponse()
            {
                Success = true,
                Message = "All data deleted"
            };
        }
    }
}
