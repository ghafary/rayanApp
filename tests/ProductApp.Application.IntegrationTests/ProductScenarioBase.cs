using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ProductApp.FunctionalTests;

public class ProductScenarioBase
{
    private class ProductApplication : WebApplicationFactory<Program>
    {
        public TestServer CreateServer()
        {
            return Server;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter, AuthStartupFilter>();
            });

            builder.ConfigureAppConfiguration(c =>
            {
                var directory = Path.GetDirectoryName(typeof(ProductScenarioBase).Assembly.Location)!;

                c.AddJsonFile(Path.Combine(directory, "appsettings.Product.json"), optional: false);
            });

            return base.CreateHost(builder);
        }
    }

    public TestServer CreateServer()
    {
        var factory = new ProductApplication();
        return factory.CreateServer();
    }

    public static class Get
    {
        public static string Products = "api/v1/products";

        public static string ProductBy(int id)
        {
            return $"api/v1/products/{id}";
        }
    }

    public static class Put
    {
        public static string UpdateProduct= "api/v1/products";
    }

    public static class Delete
    {
        public static string DeleteProduct = "api/v1/products";
    }

    private class AuthStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<AutoAuthorizeMiddleware>();

                next(app);
            };
        }
    }
}
