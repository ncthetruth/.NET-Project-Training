using Contracts.RequestModels.Customer;
using Entity.Entity;
using Newtonsoft.Json;
using System.Text;


namespace TrainingUnitTest
{
    public class TestCustomer : BaseTest
    {
        private readonly HttpClient _client;

        public TestCustomer()
        {
            _client = GetClient;
        }

        [Test, Order(1)]
        public async Task TestCreateCustomer()
        {
            var client = _factory.CreateClient();
            var fromBody = new CreateCustomerRequest
            {
                Name = "CreateTesting2",
                Email = "Create2@testing.com"
            };
            var temp = new StringContent(JsonConvert.SerializeObject(fromBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Api/v1/customer", temp);

            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.Pass();
        }

        [Test, Order(2)]
        public async Task TestGetCustomer()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/v1/customer");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        public async Task<Guid> CreateCustomerAndGetId()
        {
            var client = _factory.CreateClient();

            var createRequestBody = new CreateCustomerRequest
            {
                Name = "CreateAndGetTesting",
                Email = "Delete@testing.com"
            };
            var createRequestBodyContent = new StringContent(JsonConvert.SerializeObject(createRequestBody), Encoding.UTF8, "application/json");
            var createResponse = await client.PostAsync("Api/v1/customer", createRequestBodyContent);
            Assert.That(createResponse.IsSuccessStatusCode, Is.True);

            var createdCustomerString = await createResponse.Content.ReadAsStringAsync();
            var createdCustomer = JsonConvert.DeserializeObject<Customer>(createdCustomerString);

            return createdCustomer.CustomerID;
        }

        [Test, Order(3)]
        public async Task TestUpdate()
        {
            var client = _factory.CreateClient();

            Guid customerId = await CreateCustomerAndGetId();

            var updateRequestBody = new UpdateCustomerDataRequest
            {
                Name = "UpdatedCustomerName",
                Email = "updatedemail@Testing.com"
            };
            var updateRequestBodyContent = new StringContent(JsonConvert.SerializeObject(updateRequestBody), Encoding.UTF8, "application/json");
            var updateResponse = await client.PutAsync($"Api/v1/customer/{customerId}", updateRequestBodyContent);

            Assert.That(updateResponse.IsSuccessStatusCode, Is.True);
        }

        [Test, Order(4)]
        public async Task TestDeleteById()
        {
            var client = _factory.CreateClient();

            Guid customerId = await CreateCustomerAndGetId();

            var deleteResponse = await client.DeleteAsync($"Api/v1/customer/{customerId}");

            Assert.That(deleteResponse.IsSuccessStatusCode, Is.True);
        }

        [Test, Order(5)]
        public async Task TestGetById()
        {
            var client = _factory.CreateClient();

            Guid customerId = await CreateCustomerAndGetId();

            var getResponse = await client.GetAsync($"Api/v1/customer/{customerId}");

            Assert.That(getResponse.IsSuccessStatusCode, Is.True);

            var customerString = await getResponse.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(customerString);

            Assert.That(customerId, Is.EqualTo(customer.CustomerID));
        }

        [Test, Order(6)]
        public async Task TestDeleteAll()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/v1/customer");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }
    }
}