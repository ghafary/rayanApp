using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace ProductApp.FunctionalTests
{
    public class ProductScenarios
        : ProductScenarioBase
    {
        [Fact]
        public async Task Get_get_all_stored_orders_and_response_ok_status_code()
        {
            using var server = CreateServer();
            var response = await server.CreateClient()
                .GetAsync(Get.Products);

            var s = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task update_product_created_bad_request_response()
        {
            using var server = CreateServer();
            var content = new StringContent(BuildProduct(), UTF8Encoding.UTF8, "application/json")
            {
                Headers = { { "x-requestid", Guid.NewGuid().ToString() } }
            };
            var response = await server.CreateClient()
                .PutAsync(Put.UpdateProduct, content);

            var s = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task delete_product_created_bad_request_response()
        {
            using var server = CreateServer();
            var content = new StringContent(BuildProduct(), UTF8Encoding.UTF8, "application/json")
            {
                Headers = { { "x-requestid", Guid.NewGuid().ToString() } }
            };
            var response = await server.CreateClient()
                .PutAsync(Delete.DeleteProduct, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        string BuildProduct()
        {
            var order = new
            {
                OrderNumber = "-1"
            };
            return JsonSerializer.Serialize(order);
        }
    }
}
