using Contracts.RequestModels.Customer;
using Contracts.ResponseModels.Customer;
using Entity.Entity;
using MediatR;

namespace Services.RequestHandlers.ManageCustomer
{
	public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
	{
        private readonly DBContext _db;

        public CreateCustomerHandler(DBContext db)
        {
            _db = db;
        }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                CustomerID = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email
            };

            _db.Customers.Add(customer);
            await _db.SaveChangesAsync(cancellationToken);

            var response = new CreateCustomerResponse
            {
                CustomerID = customer.CustomerID,
            };
            
            return response;
        }
    }
}
