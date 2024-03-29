using Contracts.RequestModels.Customer;
using Contracts.ResponseModels.Customer;
using Entity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.RequestHandlers.ManageCustomer
{
	public class GetCustomerDataListHandler : IRequestHandler<CustomerDataListRequest, CustomerDataListResponse>
	{
        private readonly DBContext _db;

        public GetCustomerDataListHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CustomerDataListResponse> Handle(CustomerDataListRequest request, CancellationToken cancellationToken)
        {
            var datas = await _db.Customers.Select(Q => new CustomerData
            {
                CustomerID = Q.CustomerID,
                Name = Q.Name,
                Email = Q.Email,
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            var response = new CustomerDataListResponse
            {
                CustomerDatas = datas
            };

            return response;
        }
    }
}
