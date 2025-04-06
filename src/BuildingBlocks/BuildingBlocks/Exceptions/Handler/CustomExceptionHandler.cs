using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
       
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message: {ExceptionMessage}, Time of Occurence {time}", exception.Message, DateTime.UtcNow);
            //using switch
            /* var details = new ProblemDetails();
            switch (exception) 
            {
                case InternalServerException:
                    details.Detail=exception.Message;
                    details.Title = exception.GetType().Name;
                    details.Status = StatusCodes.Status500InternalServerError;
                    break;

                case BadRequestException:
                    details.Detail=exception.Message;
                    details.Title = exception.GetType().Name;
                    details.Status = StatusCodes.Status404NotFound;
                    break;
                default:
                    details.Detail = exception.Message;
                    details.Title = exception.GetType().Name;
                    details.Status= StatusCodes.Status500InternalServerError;
                    break;
            }*/


            //using pattern matching
            (string details, string title, int statusCode) details = exception switch
            {
                InternalServerException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode =StatusCodes.Status500InternalServerError
                ),
                ValidationException=>
                (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                BadRequestException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                NotFoundException => 
                (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status404NotFound
                ),
                _ =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.title,
                Detail = details.details,
                Status = details.statusCode,
                Instance = context.Request.Path
            };
            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

            if(exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors", validationException.ValidationResult);
            }
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

    }
}
