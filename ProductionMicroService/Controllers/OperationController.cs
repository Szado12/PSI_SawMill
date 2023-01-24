using Microsoft.AspNetCore.Mvc;
using ProductionMicroService.Services.Interfaces;
using ProductionMicroService.Utils;
using ProductionMicroService.ViewModels.Machine;
using ProductionMicroService.ViewModels.Operation;

namespace ProductionMicroService.Controllers
{
  [ApiController]
  [Route("[controller]")]
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
    public IActionResult GetOperationsByMachine(int machineId)
    {
      return _operationService.GetAllOperationsByMachine(machineId).ToActionResult();
    }

    [HttpGet]
    public IActionResult UpdateOperation(UpdateOperationViewModel updateOperation)
    {
      return _operationService.UpdateOperation(updateOperation).ToActionResult();
    }

    [HttpPost]
    public IActionResult AddOperation(AddOperationViewModel addOperation)
    {
      return _operationService.AddOperation(addOperation).ToActionResult();
    }

    [HttpPost]
    public IActionResult DeleteOperation (int operationId)
    {
      return _operationService.DeleteOperation(operationId).ToActionResult();
    }
  }
}
