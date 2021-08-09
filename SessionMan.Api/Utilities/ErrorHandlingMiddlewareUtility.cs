using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SessionMan.DataAccess.DataTransfer;
using SessionMan.Shared.Helpers;
using SessionMan.Shared.Models;

namespace SessionMan.Api.Utilities
{
    public class ErrorHandlingMiddlewareUtility
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddlewareUtility> _logger;

        public ErrorHandlingMiddlewareUtility(RequestDelegate next, ILogger<ErrorHandlingMiddlewareUtility> logger)
        {
            _next = next;
            _logger = logger;
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
                ErrorRecord errorRecord;
                _logger.LogError($"An exception occured. Exception Message: {error.Message}");
                
                switch(error)
                {
                    case BadRequestException badRequestException:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorRecord = new ErrorRecord("Bad Request", response.StatusCode, badRequestException.Message);
                        break;
                    case InvalidDataStateException invalidDataStateException:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        errorRecord = new ErrorRecord("Bad Request", response.StatusCode, invalidDataStateException.Message);
                        break;
                    case TaskCanceledException taskCancelled:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorRecord = new ErrorRecord("Request Cancelled", response.StatusCode, "The request was cancelled by the requester.");
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorRecord = new ErrorRecord("Unexpected Error Occured", response.StatusCode, "The application encountered an unexpected error. Please contact the administrator.");
                        break;
                }

                string result = JsonConvert.SerializeObject(errorRecord);
                _logger.LogDebug(result);
                await response.WriteAsync(result);
            }
        }
    }
}