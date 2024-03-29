namespace Contracts.ResponseModels.Cart
{
    public class DeleteCartResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
