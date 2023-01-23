using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMicroservice.ModelViews;
using OrderMicroservice.Services.Interfaces;
using OrderMicroservice.Utils;

namespace OrderMicroservice.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            return _orderService.GetOrders().ToActionResult();
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById([FromRoute] int id)
        {
            return _orderService.GetOrderById(id).ToActionResult();
        }

        [HttpGet("client-{id}")]
        public IActionResult GetOrderByClientId([FromRoute] int id)
        {
            return _orderService.GetOrdersByClient(id).ToActionResult();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder([FromRoute] int id)
        {
            return _orderService.DeleteOrder(id).ToActionResult();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder([FromRoute] int id, int orderStateId)
        {
            return _orderService.UpdateOrder(id, orderStateId).ToActionResult();
        }

        [HttpPost]
        public IActionResult AddOrder(AddOrderView data)
        {
            return _orderService.AddOrder(data).ToActionResult();
        }
    }
}
