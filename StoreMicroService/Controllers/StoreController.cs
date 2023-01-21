using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreMicroService.Services.Interfaces;
using StoreMicroService.Utils;
using StoreMicroService.ViewModels.Warehouse;

namespace StoreMicroService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StoreController : ControllerBase
  {
    private IWarehouseService _warehouseService;

    public StoreController(IWarehouseService warehouseService)
    {
      _warehouseService = warehouseService;
    }

    [HttpGet]
    public IActionResult GetWarehouses()
    {
      return _warehouseService.GetWarehouses().ToActionResult();
    }

    [HttpGet("{warehouseId}")]
    public IActionResult GetWarehouseDetails([FromRoute] int warehouseId)
    {
      return _warehouseService.GetWarehouseDetails(warehouseId).ToActionResult();
    }

    [HttpPost]
    public IActionResult AddNewWarehouse(AddWarehouseViewModel warehouse)
    {
      return _warehouseService.AddWarehouse(warehouse).ToActionResult();
    }

    [HttpPut]
    public IActionResult UpdateWarehouseDetails(UpdateWarehouseViewModel warehouse)
    {
      return _warehouseService.UpdateWarehouse(warehouse).ToActionResult();
    }

  }
}
