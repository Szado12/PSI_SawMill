using Microsoft.AspNetCore.Mvc;
using ProductionMicroService.Services.Interfaces;
using ProductionMicroService.Utils;
using ProductionMicroService.ViewModels.ProductionPlan;

namespace ProductionMicroService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProductionPlanController
  {
    private readonly IProductionPlanService _productionPlanService;

    public ProductionPlanController(IProductionPlanService productionPlanService)
    {
      _productionPlanService = productionPlanService;
    }

    [HttpGet]
    public IActionResult GetProductionPlans()
    {
      return _productionPlanService.GetProductionPlan().ToActionResult();
    }

    [HttpGet]
    public IActionResult GetProductionPlansByEmployee(int employeeId)
    {
      return _productionPlanService.GetProductionPlanByEmployee(employeeId).ToActionResult();
    }

    [HttpGet]
    public IActionResult GetProductionPlansByMachine(int machineId)
    {
      return _productionPlanService.GetProductionPlanByMachine(machineId).ToActionResult();
    }

    [HttpPost]
    public IActionResult AddProductionPlans(AddProductionPlan productionPlan)
    {
      return _productionPlanService.AddProductionPlan(productionPlan).ToActionResult();
    }

    [HttpPut]
    public IActionResult UpdateProductionPlans(UpdateProductionPlanViewModel updateProductionPlan)
    {
      return _productionPlanService.UpdateProductionPlan(updateProductionPlan).ToActionResult();
    }

    [HttpDelete]
    public IActionResult ArchiveProductionPlans(int productionPlanId)
    {
      return _productionPlanService.DeleteProductionPlan(productionPlanId).ToActionResult();
    }
  }
}
