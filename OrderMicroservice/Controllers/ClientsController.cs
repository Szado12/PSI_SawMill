using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMicroservice.ModelViews;
using OrderMicroservice.Services.Interfaces;
using OrderMicroservice.Utils;

namespace OrderMicroservice.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            return _clientService.GetClients().ToActionResult();
        }

        [HttpGet("{id}")]
        public IActionResult GetClient([FromRoute] int id)
        {
            return _clientService.GetClient(id).ToActionResult();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient([FromRoute] int id)
        {
            return _clientService.DeleteClient(id).ToActionResult();
        }

        [HttpPut("{id}")]
        public IActionResult EditClient([FromRoute] int id, ClientDetails data)
        {
            return _clientService.EditClient(id, data).ToActionResult();
        }

        [HttpPost]
        public IActionResult AddClient(ClientDetails data)
        {
            return _clientService.AddClient(data).ToActionResult();
        }

    }
}
