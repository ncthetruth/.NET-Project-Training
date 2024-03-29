using System.ComponentModel.DataAnnotations;

namespace Entity.Entity
{
	public class Product
	{
		[Key]
		public Guid ProductID { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; } = string.Empty;

		[Required]
		public decimal Price { get; set; }

		public List<Cart> Carts { get; set; } = new List<Cart>();
	}
}
