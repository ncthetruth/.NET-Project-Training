

namespace Contracts.ResponseModel.GetAvailableTicket
{
    public class GetAvailableTicketResponse
    {
        public List<AvailableTicketData> AvailableTickets { get; set; } = new List<AvailableTicketData>();
        
        
    }

    public class AvailableTicketData
    {
        public DateTimeOffset EventDate { get; set; }
        public int Quota { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
       
    }
}
