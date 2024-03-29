using Contracts.ResponseModels.Customer;
using MediatR;

namespace Contracts.RequestModels.Customer
{
    public class CustomerDetailRequest : IRequest<CustomerDetailResponse>
    {
       public Guid? CustomerId { get; set; }
    }
}
