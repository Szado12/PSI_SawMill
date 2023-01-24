using System.Reflection.Metadata.Ecma335;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace OrderMicroservice.Utils
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            return result.IsFailure ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Value);
        }
    }
}
