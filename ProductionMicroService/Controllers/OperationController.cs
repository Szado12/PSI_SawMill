using Microsoft.AspNetCore.Mvc;
using ProductionMicroService.Services.Interfaces;
using ProductionMicroService.Utils;
using ProductionMicroService.ViewModels.Machine;
using ProductionMicroService.ViewModels.Operation;

namespace ProductionMicroService.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class OperationController
  {
    private IOperationService _operationService;

    public OperationController(IOperationService operationService)
    {
      _operationService = operationService;
    }

    [HttpGet]
    public IActionResult GetOperations()
    {
      return _operationService.GetAllOperations().ToActionResult();
    }

    [HttpGet]
    [Route("machine/{machineId}")]
    public IActionResult GetOperationsByMachine([FromRoute] int machineId)
    {
      return _operationService.GetAllOperationsByMachine(machineId).ToActionResult();
    }

    [HttpPut]
    public IActionResult UpdateOperation(UpdateOperationViewModel updateOperation)
    {
      return _operationService.UpdateOperation(updateOperation).ToActionResult();
    }

    [HttpPost]
    public IActionResult AddOperation(AddOperationViewModel addOperation)
    {
      return _operationService.AddOperation(addOperation).ToActionResult();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteOperation ([FromRoute] int id)
    {
      return _operationService.DeleteOperation(id).ToActionResult();
    }
  }
}
