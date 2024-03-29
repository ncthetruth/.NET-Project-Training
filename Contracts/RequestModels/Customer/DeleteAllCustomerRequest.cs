using MediatR;
using Contracts.ResponseModels.Customer;

namespace Contracts.RequestModels.Customer
{
    public class DeleteAllCustomerRequest : IRequest<DeleteAllCustomerResponse>
    {
    }
}
