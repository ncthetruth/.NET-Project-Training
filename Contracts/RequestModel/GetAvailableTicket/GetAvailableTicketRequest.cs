using Contracts.ResponseModel.GetAvailableTicket;
using MediatR;

namespace Contracts.RequestModel.GetAvailableTicket
{
    public class GetAvailableTicketRequest : IRequest<GetAvailableTicketResponse>
    {
        public DateTimeOffset EventDateMin { get; set; }
        public DateTimeOffset EventDateMax { get; set; }
        public string TicketName { get; set; } = string.Empty;
        public string TicketCode { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string OrderState { get; set; } = string.Empty;
        public string OrderBy {  get; set; } = string.Empty;
    }
}
