using System.ComponentModel.DataAnnotations;

namespace Entity.Entity
{
	public class Customer
	{
		[Key]
		public Guid CustomerID { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; } = string.Empty;

		[Required]
		[StringLength(255)]
		public string Email { get; set; } = string.Empty;

		public List<Cart> Carts { get; set; } = new List<Cart>();
    }
}
