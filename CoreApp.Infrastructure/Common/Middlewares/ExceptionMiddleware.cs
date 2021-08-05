using CoreApp.Application.Common.Constants;
using CoreApp.Application.Common.DTO;
using CoreApp.Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreApp.Infrastructure.Common.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ResourceNotFoundException ex)
            {
                await HandleAppiAplicationExceptionAsync(httpContext, ex);
            }
            catch (ApiApplicationException ex)
            {
                await HandleAppiAplicationExceptionAsync(httpContext, ex);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                // _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDTO()
            {
                Status = context.Response.StatusCode,
                Message = ErrorMessages.NOT_HANDLED,
                Code = ErrorCodes.NOT_HANDLED,
                Details = new List<ErrorDetailDTO>
                {
                    new ErrorDetailDTO
                    {
                        Message = exception.Message
                    }
                }
            }));
        }

        private async Task HandleAppiAplicationExceptionAsync(HttpContext context, ApiApplicationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.Error.Status;

            await context.Response.WriteAsync(JsonSerializer.Serialize(exception.Error));
        }
        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDTO()
            {
                Status = context.Response.StatusCode,
                Message = ErrorMessages.VALIDATION,
                Code = ErrorCodes.VALIDATION,
                Details = exception.Errors.Select(error =>
                {
                    return new ErrorDetailDTO
                    {
                        Field = error.PropertyName,
                        Message = error.ErrorMessage
                    };
                }).ToList()
            }));
        }

    }
}
