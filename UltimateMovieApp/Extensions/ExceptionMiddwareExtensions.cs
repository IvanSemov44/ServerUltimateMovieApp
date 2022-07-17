

using Contracts;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace UltimateMovieApp.Extensions
{
    public static class ExceptionMiddwareExtensions 
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager loggerManager)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>;
                    if (contextFeature != null)
                    {
                        loggerManager.LogError($"Someting went wrong: {contextFeature}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error"
                        }.ToString());
                    }
                });
            });
        }
    }
}
