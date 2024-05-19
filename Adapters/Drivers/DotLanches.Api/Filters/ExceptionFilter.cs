﻿using DotLanches.Domain.Exceptions;
using DotLanches.Infra.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DotLanches.Api.Filters
{
    public class ExceptionFilter : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var status = exception switch
            {
                EntityNotFoundException => StatusCodes.Status404NotFound,
                DomainValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var error = new ProblemDetails()
            {
                Status = status,
                Title = "An error occurred while processing the request",
                Detail = exception.Message
            };
            
            httpContext.Response.StatusCode = status;
            await httpContext.Response.WriteAsJsonAsync(error, cancellationToken);

            return true;
        }
    }
}
