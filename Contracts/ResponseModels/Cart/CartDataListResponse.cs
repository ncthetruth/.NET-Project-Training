namespace Contracts.ResponseModels.Cart
{
    public class CartDataListResponse
    {
        public List<CartData> CartDatas { get; set; } = new List<CartData>();
    }

    /*public class CartData
    {
        public Guid CartID { get; set; }
        public Guid CustomerID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
    }*/

    public class CartData
    {
        public Guid CartID { get; set; }
        public string CustomerName {  get; set; } = string.Empty;
        public string ProductName {  get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
