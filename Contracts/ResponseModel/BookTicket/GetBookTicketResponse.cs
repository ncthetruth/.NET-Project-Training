namespace Contracts.ResponseModel.BookTicket
{
    public class GetBookTicketResponse
    {
        public List<TicketCategory> TicketCategories { get; set; } = new List<TicketCategory>();
    }

    public class TicketCategory
    {
        public int QtyPerCategory { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }

    public class Ticket
    {
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public DateTimeOffset EventDate { get; set; }
    }
}
