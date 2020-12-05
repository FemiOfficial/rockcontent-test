using System;
using Api.Resources.Response;
using Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Api.Extensions;

namespace Api.Controllers.Config
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorResponse(messages: errors, "Validation Error", ApiReponseStatusCodes.BadRequest);

            return new BadRequestObjectResult(response);
        }
    }
}
