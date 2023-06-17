using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using ProductApp.Infrastructure;

namespace ProductApp.Api.Infrastructure;

public class ProductContextSeed
{
    public async Task SeedAsync(ProductContext context,ILogger<ProductContextSeed> logger)
    {
        var policy = CreatePolicy(logger, nameof(ProductContextSeed));

        await policy.ExecuteAsync(async () =>
        {
            using (context)
            {
                context.Database.Migrate();

                await context.SaveChangesAsync();
            }
        });
    }



    private string[] GetHeaders(string[] requiredHeaders, string csvfile)
    {
        string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

        if (csvheaders.Count() != requiredHeaders.Count())
        {
            throw new Exception($"requiredHeader count '{requiredHeaders.Count()}' is different then read header '{csvheaders.Count()}'");
        }

        foreach (var requiredHeader in requiredHeaders)
        {
            if (!csvheaders.Contains(requiredHeader))
            {
                throw new Exception($"does not contain required header '{requiredHeader}'");
            }
        }

        return csvheaders;
    }


    private AsyncRetryPolicy CreatePolicy(ILogger<ProductContextSeed> logger, string prefix, int retries = 3)
    {
        return Policy.Handle<SqlException>().
            WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogWarning(exception, "[{prefix}] Error seeding database (attempt {retry} of {retries})", prefix, retry, retries);
                }
            );
    }
}
