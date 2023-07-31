using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UniversityRestApi.Exceptions;


public class UniversityException: Exception
{

    public UniversityException(int statusCode, object? payload = null) =>
        (StatusCode, Payload) = (statusCode, payload);

    public int StatusCode { get; set; }

    public Object? Payload { get; set; }
}

public class UniversityResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    void IActionFilter.OnActionExecuted(ActionExecutedContext context)
    {
        if(context.Exception is UniversityException universityException)
        {
            var result = new ObjectResult(universityException.Payload);
            result.StatusCode = universityException.StatusCode;
            context.Result = result; 
            context.ExceptionHandled = true;
        }
    }

    void IActionFilter.OnActionExecuting(ActionExecutingContext context){}
}