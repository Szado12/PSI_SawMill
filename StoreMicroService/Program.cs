using Microsoft.AspNetCore.Authorization;
using StoreMicroService.Models;
using StoreMicroService.Services;
using StoreMicroService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var context = new StoreContext();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IProductService, ProductService>(x=> new ProductService(context));
builder.Services.AddSingleton<IWoodTypeService, WoodTypeService>(x => new WoodTypeService(context));
builder.Services.AddSingleton<IProductTypeService, ProductTypeService>(x => new ProductTypeService(context));
builder.Services.AddSingleton<IWarehouseService, WarehouseService>(x => new WarehouseService(context));

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
