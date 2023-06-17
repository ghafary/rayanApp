using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductApp.Api.Extensions;
using ProductApp.Api.Infrastructure;
using ProductApp.Api.Infrastructure.Services;
using ProductApp.Application.Common.Behaviours;
using ProductApp.Application.Common.Interfaces;
using ProductApp.Application.Common.Mappings;
using ProductApp.Application.Infrastructure.Idempotency;
using ProductApp.Application.Products.Commands.CreateProduct;
using ProductApp.Application.Products.Commands.UpdateProduct;
using ProductApp.Application.Products.Interfaces;
using ProductApp.Application.Products.Queries.GetProductsWithPagination;
using ProductApp.Infrastructure;
using ProductApp.Infrastructure.Idempotency;
using ProductApp.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContexts(builder.Configuration);
builder.Services.AddApplicationOptions(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(IMapFrom<>)));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(ValidatorBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
    cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddSingleton<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
builder.Services.AddSingleton<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
builder.Services.AddSingleton<IValidator<GetProductsWithPaginationQuery>, GetProductsWithPaginationQueryValidator>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRequestManager, RequestManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
//app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProductContext>();
    var logger = app.Services.GetService<ILogger<ProductContextSeed>>();
    await context.Database.MigrateAsync();
    await new ProductContextSeed().SeedAsync(context, logger);
}

app.Run();

