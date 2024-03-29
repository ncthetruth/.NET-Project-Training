using Contracts.ResponseModels.Customer;
using MediatR;

namespace Contracts.RequestModels.Customer 
{
    public class DeleteCustomerRequest : IRequest<DeleteCustomerResponse>
    {
        public Guid? CustomerId { get; set; }
    }
}
