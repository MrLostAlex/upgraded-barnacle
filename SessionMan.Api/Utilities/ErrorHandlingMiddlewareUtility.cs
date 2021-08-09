using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SessionMan.DataAccess.DataTransfer;

namespace SessionMan.Api.Utilities
{
    public class ErrorHandlingMiddlewareUtility
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddlewareUtility(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";
                ErrorBaseRecord errorRecord;
                switch(error)
                {
                    // case AppException e:
                    //     // custom application error
                    //     response.StatusCode = (int)HttpStatusCode.BadRequest;
                    //     break;
                    case KeyNotFoundException keyNotFoundException:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        errorRecord = new ErrorBaseRecord()
                        {
                            Title = "Key Not Found Exception",
                            ErrorMessage = keyNotFoundException.Message,
                            StatusCode = StatusCodes.Status404NotFound
                        };
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorRecord = new ErrorBaseRecord()
                        {
                            Title = "Unexpected Error Occured",
                            ErrorMessage = error.Message,
                            StatusCode = StatusCodes.Status500InternalServerError
                        };
                        break;
                }

                string result = JsonConvert.SerializeObject(errorRecord);
                await response.WriteAsync(result);
            }
        }
    }
}