namespace Contracts.ResponseModels.Product
{
    public class ProductDataListResponse
    {
        public List<ProductDataModel> productList { get; set; } = [];
    }

    public class ProductDataModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
