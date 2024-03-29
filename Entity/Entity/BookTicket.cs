using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
    public class BookTicket
    {
        [Key]
        public string BookCode { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("TicketCode")]
        public string TicketCode { get; set; }

        public AvailableTicket availableTicket { get; set; }
    }
}
