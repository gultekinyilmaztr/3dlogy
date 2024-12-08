using Base.CrossCuttingConcerns.Exceptions.Extensions;
using Base.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Base.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using ValidationException = Base.CrossCuttingConcerns.Exceptions.Types.ValidationException;


namespace Base.CrossCuttingConcerns.Exceptions.Handlers;

public class HttpExceptionHandler : ExceptionHandler
{
    private HttpResponse? _response;
    public HttpResponse Response
    {
        get => _response ?? throw new ArgumentNullException(nameof(_response));
        set => _response = value;
    }
    protected override Task HandleException(BusinessException businessException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new BusinessProblemDetails(businessException.Message).AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(Exception exception)
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        string details = new InternalServerErrorProblemDetails(exception.Message).AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(ValidationException validationException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new HttpProblemDetails.ValidationProblemDetails(validationException.Errors).AsJson();
        return Response.WriteAsync(details);
    }
}