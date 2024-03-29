using Contracts.RequestModels.Product;
using Entity.Entity;
using Newtonsoft.Json;
using System.Text;


namespace TrainingUnitTest
{
    public class TestsProduct : BaseTest
    {
        private readonly HttpClient _client;

        public TestsProduct()
        {
            _client = GetClient;
        }

        [Test, Order(1)]
        public async Task TestCreateProduct()
        {
            var client = _factory.CreateClient();
            var fromBody = new CreateProductRequest
            {
                Name = "CreateTesting2",
                Price = 100000
            };
            var temp = new StringContent(JsonConvert.SerializeObject(fromBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Api/v1/product", temp);

            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.Pass();
        }

        [Test, Order(2)]
        public async Task TestGetProduct()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/v1/product");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        public async Task<Guid> CreateProductAndGetId()
        {
            var client = _factory.CreateClient();

            var createRequestBody = new CreateProductRequest
            {
                Name = "CreateAndGetTesting",
                Price = 20000
            };
            var createRequestBodyContent = new StringContent(JsonConvert.SerializeObject(createRequestBody), Encoding.UTF8, "application/json");
            var createResponse = await client.PostAsync("Api/v1/product", createRequestBodyContent);
            Assert.That(createResponse.IsSuccessStatusCode, Is.True);

            var createdProductString = await createResponse.Content.ReadAsStringAsync();
            var createdProduct = JsonConvert.DeserializeObject<Product>(createdProductString);

            return createdProduct.ProductID;
        }

        [Test, Order(3)]
        public async Task TestUpdate()
        {
            var client = _factory.CreateClient();

            Guid productId = await CreateProductAndGetId();

            var updateRequestBody = new UpdateProductRequest
            {
                Name = "UpdatedProductName",
                Price = 300000
            };
            var updateRequestBodyContent = new StringContent(JsonConvert.SerializeObject(updateRequestBody), Encoding.UTF8, "application/json");
            var updateResponse = await client.PutAsync($"Api/v1/product/{productId}", updateRequestBodyContent);

            Assert.That(updateResponse.IsSuccessStatusCode, Is.True);
        }

        [Test, Order(4)]
        public async Task TestDeleteById()
        {
            var client = _factory.CreateClient();

            Guid productId = await CreateProductAndGetId();

            var deleteResponse = await client.DeleteAsync($"Api/v1/product/{productId}");

            Assert.That(deleteResponse.IsSuccessStatusCode, Is.True);
        }

        [Test, Order(5)]
        public async Task TestGetById()
        {
            var client = _factory.CreateClient();

            Guid productId = await CreateProductAndGetId();

            var getResponse = await client.GetAsync($"Api/v1/product/{productId}");

            Assert.That(getResponse.IsSuccessStatusCode, Is.True);

            var productString = await getResponse.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(productString);

            Assert.That(productId, Is.EqualTo(product.ProductID));
        }

        [Test, Order(6)]
        public async Task TestDeleteAll()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/v1/product");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

    }
}