using Contracts.ResponseModel.GetAvailableTicket;

namespace Contracts.ResponseModel.BookTicket
{
    public class CreateBookTicketResponse
    {
        public decimal SummaryPrice { get; set; }
        public string BookCode { get; set; }

        public List<AvailableTicketData> AvailableTickets { get; set; } = new List<AvailableTicketData>();


    }

}
