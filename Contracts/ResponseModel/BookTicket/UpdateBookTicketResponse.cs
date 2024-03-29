

using System.Globalization;

namespace Contracts.ResponseModel.BookTicket
{
    public class UpdateBookTicketResponse
    {
        public string TicketCode { get; set; } = string.Empty;
        public string TicketName { get; set; } = string.Empty;
        public int Quantity {  get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public string Message {  get; set; } = string.Empty;
        public bool Success {  get; set; }

    }
}
