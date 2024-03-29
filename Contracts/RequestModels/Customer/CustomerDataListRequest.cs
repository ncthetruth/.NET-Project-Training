using Contracts.ResponseModels.Customer;
using MediatR;

namespace Contracts.RequestModels.Customer
{
	public class CustomerDataListRequest : IRequest<CustomerDataListResponse>
	{
	}
}
