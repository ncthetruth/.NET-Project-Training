using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
    public class AvailableTicket
    {
        [Key]
        public string TicketCode { get; set; } = string.Empty;

        [Required]
        public int Quota { get; set; }

        public DateTimeOffset EventDate { get; set; }
        
        [Required]
        [StringLength(255)]
        public string TicketName { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }
    }
}
