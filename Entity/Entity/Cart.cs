using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
	public class Cart
	{
		[Key]
		public Guid CartID { get; set; }

		[Required]
		public int Quantity { get; set; }

		[ForeignKey("ProductID")]
		public Guid ProductID { get; set; }

		[ForeignKey("CustomerID")]
		public Guid CustomerID { get; set; }

		public Product? Product { get; set; }

		public Customer? Customer { get; set; }
	}
}
