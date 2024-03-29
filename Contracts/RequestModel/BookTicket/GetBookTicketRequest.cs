using MediatR;
using Contracts.ResponseModel.BookTicket;

namespace Contracts.RequestModel.BookTicket
{
    public class GetBookTicketRequest : IRequest<GetBookTicketResponse>
    {
        public string BookId { get; set; } = string.Empty;
    }
}
