using Contracts.ResponseModels.Customer;
using Contracts.RequestModels.Customer;
using Entity.Entity;
using MediatR;

namespace Services.RequestHandlers.ManageCustomer
{
    public class GetCustomerDetailHandler : IRequestHandler<CustomerDetailRequest, CustomerDetailResponse>
    {
        private readonly DBContext _db;

        public GetCustomerDetailHandler(DBContext db)
        {
            _db = db;
        }
        public async Task<CustomerDetailResponse> Handle(CustomerDetailRequest request, CancellationToken cancellationToken)
        {
            var existingDatas = await _db.Customers.FindAsync(request.CustomerId);

            var data = new CustomerDetailResponse
            {
                CustomerId = existingDatas.CustomerID,
                Name = existingDatas.Name,
                Email = existingDatas.Email

            };
            return data;


        }
    }
}
