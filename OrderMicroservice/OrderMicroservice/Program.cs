using OrderMicroservice.Models;
using OrderMicroservice.Services;
using OrderMicroservice.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var context = new ClientOrderContext();
builder.Services.AddSingleton<IEncryptionService, EncryptionService>();
builder.Services.AddSingleton<IOrderService, OrderService>(x => new OrderService(context));
builder.Services.AddSingleton<IDeliveryService, DeliveryService>(x => new DeliveryService(context));
builder.Services.AddSingleton<IClientService, ClientService>(
    (IServiceProvider employeeService) => new ClientService(context, employeeService.GetRequiredService<IEncryptionService>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
