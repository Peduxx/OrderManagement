using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application;
using OrderManagement.Application.Errors;

namespace OrderManagement.Api.Extensions
{
    public static class ResultMapper
    {
        public static IActionResult ToActionResult(this Result result, ControllerBase controller)
        {
            if (result.IsSuccess)
                return controller.Ok(result);

            return result.Error.Type switch
            {
                ErrorType.BadRequest => controller.BadRequest(result),
                ErrorType.NotFound => controller.NotFound(result),
                ErrorType.Conflict => controller.Conflict(result),
                ErrorType.InternalServerError => controller.StatusCode(500, result),
                _ => controller.StatusCode(500, result)
            };
        }
    }
}
