using MediatR;
using Contracts.ResponseModel.BookTicket;
using System.Globalization;

namespace Contracts.RequestModel.BookTicket
{
    public class DeleteBookTicketRequest : IRequest<DeleteBookTicketResponse>
    {
        public string BookCode { get; set; } = string.Empty;
        public string TicketCode { get; set; } = string.Empty;
        public int qty {  get; set; }
    }
}
