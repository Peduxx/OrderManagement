using Microsoft.EntityFrameworkCore;
using OrderManagement.Api.Middlewares;
using OrderManagement.Application.Commands.Custormers.Handlers;
using OrderManagement.Application.Queries.Customers.Handlers;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infra.Data;
using OrderManagement.Infra.Data.Services.QueryServices;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllCustomersQueryHandler).Assembly));

builder.Services.AddScoped<ICustomerQueryService, CustomerQueryService>();
builder.Services.AddScoped<IOrderQueryService, OrderQueryService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();

builder.Services.AddDbContext<OrderManagementContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LogHandlingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
