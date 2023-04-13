using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using ToDoer.Application.Exceptions;

namespace ToDoer.Application.ErrorHandler
{
    public class ApiError : ProblemDetails
    {
        public const string UnhandlerErrorCode = "UnhandledError";
        private HttpContext _httpContext;
        private Exception _exception;



        public LogLevel LogLevel { get; set; }
        public string Code { get; set; }

        public string TraceId
        {
            get
            {
                if (Extensions.TryGetValue("TraceId", out var traceId))
                {
                    return (string)traceId;
                }

                return null;
            }

            set => Extensions["TraceId"] = value;
        }

        public ApiError(HttpContext httpContext, Exception exception)
        {
            _httpContext = httpContext;
            _exception = exception;

            TraceId = httpContext.TraceIdentifier;

            //default
            Code = UnhandlerErrorCode;
            Status = (int)HttpStatusCode.InternalServerError;
            Title = exception.Message;
            LogLevel = LogLevel.Error;
            Instance = httpContext.Request.Path;

            HandleException((dynamic)exception);
        }


        private void HandleException(ToDoNotFoundException exception)
        {
            Code = exception.Message;
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = exception.Source;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(SubtaskNotFoundException exception)
        {
            Code = exception.Message;
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = exception.Source;
            LogLevel = LogLevel.Information;
        }


        private void HandleException(Exception exception)
        {
        }

    }
}
