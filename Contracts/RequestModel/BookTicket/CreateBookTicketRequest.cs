using MediatR;
using Contracts.ResponseModel.BookTicket;

namespace Contracts.RequestModel.BookTicket
{
    public class CreateBookTicketRequest : IRequest<CreateBookTicketResponse>
    {
        public string TicketCode { get; set; } = string.Empty;
        public int Quantity { get; set; }

    }
}
