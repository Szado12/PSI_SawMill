using Microsoft.AspNetCore.Mvc;
using ProductionMicroService.Services.Interfaces;
using ProductionMicroService.Utils;
using ProductionMicroService.ViewModels.Machine;

namespace ProdcutionMicroService.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class MachineController : ControllerBase
  {
    private IMachineService _machineService;

    public MachineController(IMachineService machineService)
    {
      _machineService = machineService;
    }

    [HttpGet]
    public IActionResult GetMachines()
    {
      return _machineService.GetAllMachines().ToActionResult();
    }

    [HttpGet]
    [Route("operation/{operationId}")]
    public IActionResult GetMachinesByOperation([FromRoute] int operationId)
    {
      return _machineService.GetAllMachinesByOperation(operationId).ToActionResult();
    }


    [HttpGet]
    [Route("{machineId}")]
    public IActionResult GetMachines([FromRoute] int machineId)
    {
      return _machineService.GetMachineById(machineId).ToActionResult();
    }

    [HttpPost]
    public IActionResult AddMachine(AddMachineViewModel machine)
    {
      return _machineService.AddMachine(machine).ToActionResult();
    }

    [HttpPut]
    public IActionResult UpdateMachine(UpdateMachineViewModel machine)
    {
      return _machineService.UpdateMachine(machine).ToActionResult();
    }

    [HttpDelete]
    [Route("{machineId}")]
    public IActionResult ArchiveMachine([FromRoute] int machineId)
    {
      return _machineService.DeleteMachine(machineId).ToActionResult();
    }
  }
}