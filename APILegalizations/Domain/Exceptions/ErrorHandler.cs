using APILegalizations.Domain.Exceptions.Exception;
using APILegalizations.Domain.Exceptions.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APILegalizations.Domain.Exceptions
{
    public class ErrorHandler : ExceptionFilterAttribute
    {
        private readonly Dictionary<Type, (HttpStatusCode StatusCode, ErrorResponse ErrorResponse)> _exceptionHandlers;

        public ErrorHandler()
        {
            _exceptionHandlers = new Dictionary<Type, (HttpStatusCode, ErrorResponse)>
            {
                    // Inyectamos los errores que queremos manejar
                    { typeof(UserException), (HttpStatusCode.NotFound, new ErrorResponse(100, "USER_INVALID", null)) },
                    { typeof(LoginException), (HttpStatusCode.NotFound, new ErrorResponse(110, "USER_NOT_FOUND", null)) },

            };
        }
        public override void OnException(ExceptionContext context)
        {
            if (_exceptionHandlers.TryGetValue(context.Exception.GetType(), out (HttpStatusCode StatusCode, ErrorResponse ErrorResponse) handler))
            {
                handler.ErrorResponse.Message = context.Exception.Message;
                context.HttpContext.Response.StatusCode = (int)handler.StatusCode;
                context.Result = new JsonResult(handler.ErrorResponse);
            }
            else
            {
                ErrorResponse genericErrorResponse = new ErrorResponse(500, "INTERNAL_SERVER_ERROR", "An unexpected error occurred.");
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new JsonResult(genericErrorResponse);
            }

            base.OnException(context);
        }
    }
}