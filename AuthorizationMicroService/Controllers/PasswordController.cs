// AuthorizationMicroService.Controllers.PasswordController

using Microsoft.AspNetCore.Mvc;

namespace AuthorizationMicroService.Controllers;

[ApiController]
[Route("api/PasswordController")]
public class PasswordController : ControllerBase
{
  [HttpGet]
  public IActionResult Get()
  {
    return Ok("kupa");
  }
}