using Contracts.RequestModels.Cart;
using Entity.Entity;
using Newtonsoft.Json;
using System.Text;


namespace TrainingUnitTest
{
    public class TestsCart : BaseTest
    {
        private readonly HttpClient _client;

        public TestsCart()
        {
            _client = GetClient;
        }

        [Test, Order(1)]
        public async Task TestCreateCart()
        {
            var client = _factory.CreateClient();

            Guid customerId = Guid.Parse("1b49f750-9bfe-4b4c-8311-9380e7019321");
            Guid productId = Guid.Parse("b8e6c3ad-2e22-4da9-a672-52cdde6ddf6c");

            var fromBody = new CreateCartRequest
            {
                CustomerID = customerId,
                ProductID = productId,
                Quantity = 5
            };
            var temp = new StringContent(JsonConvert.SerializeObject(fromBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Api/v1/cart", temp);

            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.Pass();
        }

        [Test, Order(2)]
        public async Task TestGetCart()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/v1/cart");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        public async Task<Guid> CreateCartAndGetId()
        {
            var client = _factory.CreateClient();

            Guid customerId = await CreateCustomer2AndGetId();
            Guid productId = await CreateProduct2AndGetId();

            var createRequestBody = new CreateCartRequest
            {
                CustomerID = customerId,
                ProductID = productId,
                Quantity = 10
            };
            var createRequestBodyContent = new StringContent(JsonConvert.SerializeObject(createRequestBody), Encoding.UTF8, "application/json");
            var createResponse = await client.PostAsync("Api/v1/cart", createRequestBodyContent);
            Assert.That(createResponse.IsSuccessStatusCode, Is.True);

            var createdCartString = await createResponse.Content.ReadAsStringAsync();
            var createdCart = JsonConvert.DeserializeObject<Cart>(createdCartString);

            return createdCart.CartID;
        }

        /*[Test, Order(3)]
        public async Task TestUpdate()
        {
            var client = _factory.CreateClient();

            Guid cartId = await CreateCartAndGetId();

            var updateRequestBody = new UpdateCartDataRequest
            {
                Quantity = 10
            };
            var updateRequestBodyContent = new StringContent(JsonConvert.SerializeObject(updateRequestBody), Encoding.UTF8, "application/json");
            var updateResponse = await client.PutAsync($"Api/v1/cart/{cartId}", updateRequestBodyContent);

            Assert.That(updateResponse.IsSuccessStatusCode, Is.True);
        }*/

        [Test, Order(3)]
        public async Task TestUpdateProduct()
        {
            var client = _factory.CreateClient();

            Guid customerId = await CreateCustomer2AndGetId();
            Guid productId = await CreateProduct2AndGetId();

            var updateRequestBody = new
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = 10
            };

            var updateRequestBodyContent = new StringContent(JsonConvert.SerializeObject(updateRequestBody), Encoding.UTF8, "application/json");

            var updateResponse = await client.PutAsync($"Api/v1/customer/{customerId}/product/{productId}", updateRequestBodyContent);

            Assert.That(updateResponse.IsSuccessStatusCode, Is.True);
        }

        [Test, Order(4)]
        public async Task TestDeleteById()
        {
            var client = _factory.CreateClient();

            Guid cartId = await CreateCartAndGetId();

            var deleteResponse = await client.DeleteAsync($"Api/v1/cart/{cartId}");

            Assert.That(deleteResponse.IsSuccessStatusCode, Is.True);
        }

        [Test, Order(5)]
        public async Task TestGetById()
        {
            var client = _factory.CreateClient();

            Guid cartId = await CreateCartAndGetId();

            var getResponse = await client.GetAsync($"Api/v1/cart/{cartId}");

            Assert.That(getResponse.IsSuccessStatusCode, Is.True);

            var cartString = await getResponse.Content.ReadAsStringAsync();
            var cart = JsonConvert.DeserializeObject<Cart>(cartString);

            Assert.That(cartId, Is.EqualTo(cart.CartID));
        }

        [Test, Order(6)]
        public async Task TestDeleteAll()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/v1/cart");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }



        private async Task<Guid> CreateCustomer2AndGetId()
        {
            var client = _factory.CreateClient();

            var newCustomerData = new
            {
                Name = "NewCustomer",
                Email = "newcustomer@example.com"
            };

            var requestBodyContent = new StringContent(JsonConvert.SerializeObject(newCustomerData), Encoding.UTF8, "application/json");

            var createResponse = await client.PostAsync("Api/v1/customer", requestBodyContent);

            createResponse.EnsureSuccessStatusCode();

            var responseContent = await createResponse.Content.ReadAsStringAsync();
            var createdCustomer = JsonConvert.DeserializeObject<dynamic>(responseContent);
            Guid customerId = Guid.Parse(createdCustomer.id.ToString());

            return customerId;
        }

        private async Task<Guid> CreateProduct2AndGetId()
        {
            var client = _factory.CreateClient();

            var newProductData = new
            {
                Name = "NewProduct",
                Price = 1000
            };

            var requestBodyContent = new StringContent(JsonConvert.SerializeObject(newProductData), Encoding.UTF8, "application/json");

            var createResponse = await client.PostAsync("Api/v1/product", requestBodyContent);

            createResponse.EnsureSuccessStatusCode();

            var responseContent = await createResponse.Content.ReadAsStringAsync();
            var createdProduct = JsonConvert.DeserializeObject<dynamic>(responseContent);
            Guid productId = Guid.Parse(createdProduct.id.ToString());

            return productId;
        }
    }
}