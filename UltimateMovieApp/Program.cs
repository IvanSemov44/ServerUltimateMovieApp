using Constracts;
using Entities;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Repository;
using Microsoft.AspNetCore.Mvc;
using UltimateMovieApp.ActionFilters;
using Microsoft.Extensions.Logging;
using UltimateMovieApp.Extensions;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
});

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000/")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
        //.SetIsOriginAllowedToAllowWildcardSubdomains() ;
    });
});


builder.Services.AddScoped<ValidationFilterAttribute>();

builder.Services.AddScoped<ValidateMovieForExistFilter>();

builder.Services.AddScoped<ValidateCompanyExistAttribute>();

builder.Services.AddScoped<ValidateEmplayeeForCompanyFilter>();

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

ExceptionMiddwareExtensions.ConfigureExceptionHandler(app);

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseCors("CorsPolicy");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
