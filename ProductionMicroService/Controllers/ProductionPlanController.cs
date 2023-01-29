using Microsoft.AspNetCore.Mvc;
using ProductionMicroService.Services.Interfaces;
using ProductionMicroService.Utils;
using ProductionMicroService.ViewModels.ProductionPlan;

namespace ProductionMicroService.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
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
      return _productionPlanService.GetProductionPlans().ToActionResult();
    }

    [HttpGet]
    [Route("employe/{employeeId}")]
    public IActionResult GetProductionPlansByEmployee([FromRoute] int employeeId)
    {
      return _productionPlanService.GetProductionPlanByEmployee(employeeId).ToActionResult();
    }

    [HttpGet]
    [Route("machine/{employeeId}")]
    public IActionResult GetProductionPlansByMachine(int machineId)
    {
      return _productionPlanService.GetProductionPlanByMachine(machineId).ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> AddProductionPlans(AddProductionPlan productionPlan)
    {
      return (await _productionPlanService.AddProductionPlan(productionPlan)).ToActionResult();
    }
    
    [HttpDelete]
    [Route("{id}")]
    public IActionResult ArchiveProductionPlans([FromRoute] int id)
    {
      return _productionPlanService.DeleteProductionPlan(id).ToActionResult();
    }

    [HttpDelete]
    [Route("MoveToNextState/{id}")]
    public async Task<IActionResult> MoveToNextStateAsync([FromRoute] int id)
    {
      return (await _productionPlanService.UpdateState(id)).ToActionResult();
    }

    [HttpDelete]
    [Route("MoveToErrorState/{id}")]
    public async Task<IActionResult> MoveToErrorStateAsync([FromRoute] int id)
    {
      return _productionPlanService.UpdateStateError(id).ToActionResult();
    }
  }
}
