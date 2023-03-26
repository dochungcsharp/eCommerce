using System.Data.SqlClient;
using System.Net;
using eCommerce.Model.Abstractions.Responses;
using eCommerce.Shared.Exceptions;
using Newtonsoft.Json;

namespace eCommerce.WebAPI.Middlewares;

public class HandleExceptionMiddleware : IMiddleware
{
    private readonly ILogger<HandleExceptionMiddleware> _logger;

    public HandleExceptionMiddleware(ILogger<HandleExceptionMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
            _logger.LogError(exception, exception.Message);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        BaseResponseModel baseResponseModel = new BaseResponseModel(System.Net.HttpStatusCode.InternalServerError, exception.Message);

        switch (exception)
        {
            case SqlException:
            {
                baseResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                break;
            }
            case UnauthorizedException:
            {
                baseResponseModel.StatusCode = HttpStatusCode.Unauthorized;
                break;
            }
            case ForbiddenException:
            {
                baseResponseModel.StatusCode = HttpStatusCode.Forbidden;
                break;
            }
            case NotFoundException:
            {
                baseResponseModel.StatusCode = HttpStatusCode.NotFound;
                break;
            }
            case BadRequestException:
            {
                baseResponseModel.StatusCode = HttpStatusCode.BadRequest;
                break;
            }
        }
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonConvert.SerializeObject(baseResponseModel));
    }
}