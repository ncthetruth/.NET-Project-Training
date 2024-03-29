using Contracts.ResponseModel.GetAvailableTicket;
using MediatR;

namespace Contracts.RequestModel.GetAvailableTicket
{
    public class CreateAvailableTicketRequest : IRequest<CreateAvailableTicketResponse>
    {
        public DateTimeOffset EventDate { get; set; }
        public int Quota { get; set; }
        public string TicketName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
