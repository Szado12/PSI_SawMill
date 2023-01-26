using ProductionMicroService.Models;
using ProductionMicroService.Services;
using ProductionMicroService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var context = new ProductionContext();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IOperationService, OperationService>(x=> new OperationService(context));
builder.Services.AddSingleton<IMachineService,MachineService>(x=> new MachineService(context,x.GetRequiredService<IOperationService>()));
builder.Services.AddSingleton<IProductionPlanService,ProductionPlanService>(x=> new ProductionPlanService(context, x.GetRequiredService<IMachineService>(), x.GetRequiredService<IOperationService>()));

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
