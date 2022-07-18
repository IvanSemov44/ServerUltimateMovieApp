using Constracts;
using Entities;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Net;
using NLog;
using NLog.Web;
using Microsoft.AspNetCore.Mvc;
using UltimateMovieApp.ActionFilters;
using Microsoft.Extensions.DependencyInjection;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

//TODO: need a better cofg for LogManager

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    //builder.Services.AddControllers(options =>
    //{
    //    options.Filters.Add<ValidationFilterAttribute>();
    //});



    builder.Services.AddControllers().AddNewtonsoftJson();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
        {
            builder.WithOrigins("http://localhost:3000/")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowedToAllowWildcardSubdomains() ;
        });
    });

    builder.Services.Configure<ApiBehaviorOptions>(opt =>
    {
        opt.SuppressModelStateInvalidFilter = true;
    });
    builder.Services.AddScoped<ValidationFilterAttribute>();

    builder.Services.AddScoped<ValidateCompanyExistAttribute>();

    builder.Services.AddScoped<ValidateEmplayeeForCompanyFilter>();

    builder.Services.AddDbContext<RepositoryContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
    });


    builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseHsts();
    }
    app.UseExceptionHandler(appError =>
    {
        appError.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var contextFeature = context.Features.Get<IExceptionHandlerFeature>;
            if (contextFeature != null)
            {
                logger.Error($"Someting went wrong: {contextFeature}");

                await context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error"
                }.ToString());
            }
        });
    });

    app.UseForwardedHeaders();

    // Configure the HTTP request pipeline.

    app.UseRouting();

    app.UseCors("CorsPolicy");

    //app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    logger.Error(ex,"this is catch from main"); 
}
finally
{
    NLog.LogManager.Shutdown();
};