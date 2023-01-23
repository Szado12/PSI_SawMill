using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace ProductionMicroService.Utils
{
  public static class ResultExtensions
  {
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
      return result.IsFailure ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Value);
    }
  }
}