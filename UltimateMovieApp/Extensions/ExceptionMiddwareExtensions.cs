using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace UltimateMovieApp.Extensions
{
    public static class ExceptionMiddwareExtensions 
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
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
//app.UseExceptionHandler(Logger(ExceptionMiddwareExtensions));
//app.UseExceptionHandler(appError =>
//{
//    appError.Run(async context =>
//    {
//        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//        context.Response.ContentType = "application/json";

//        var contextFeature = context.Features.Get<IExceptionHandlerFeature>;
//        if (contextFeature != null)
//        {

//            await context.Response.WriteAsync(new ErrorDetails()
//            {
//                StatusCode = context.Response.StatusCode,
//                Message = "Internal Server Error"
//            }.ToString());
//        }
//    });
//});