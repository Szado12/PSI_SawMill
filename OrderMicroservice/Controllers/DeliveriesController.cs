using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMicroservice.ModelViews.Deliveries;
using OrderMicroservice.Services;
using OrderMicroservice.Services.Interfaces;
using OrderMicroservice.Utils;

namespace OrderMicroservice.Controllers
{
    [Route("api/delivery")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveriesController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet]
        public IActionResult GetDeliveries()
        {
            return _deliveryService.GetDeliveries().ToActionResult();
        }

        [HttpGet("{id}")]
        public IActionResult GetDeliveryById([FromRoute] int id)
        {
            return _deliveryService.GetDeliveryById(id).ToActionResult();
        }

        [HttpGet("order-{id}")]
        public IActionResult GetDeliveryByOrderId([FromRoute] int id)
        {
            return _deliveryService.GetDeliveryByOrderId(id).ToActionResult();
        }
        [HttpGet("states")]
        public IActionResult GetDeliveryStates()
        {
            return _deliveryService.GetDeliveryStates().ToActionResult();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderState([FromRoute] int id, int? orderStateId, int? employeeId)
        {
            if(employeeId == null && orderStateId == null)
                return BadRequest("At least one of properties must be given");

            return _deliveryService.UpdateDelivery(id, orderStateId, employeeId).ToActionResult();
        }
    }
}
