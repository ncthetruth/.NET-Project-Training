using MediatR;
using Contracts.ResponseModel.BookTicket;

namespace Contracts.RequestModel.BookTicket
{
    public class UpdateBookTicketRequest : UpdateBookTicketModel, IRequest<UpdateBookTicketResponse>
    {
        public string BookCode { get; set; } = string.Empty;
    }

    public class UpdateBookTicketModel
    {
        public int Quantity { get; set; }
        public string TicketCode { get; set; } = string.Empty;
    }
}
