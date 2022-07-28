using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using UltimateMovieApp.ActionFilters;
using UltimateMovieApp.Extensions;
using Entities.Models;
using Constracts;
using Entities;
using Repository;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
});

builder.Services.AddIdentityCore<MovieUser>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 6;
    opt.User.RequireUniqueEmail = true;
});
builder.Services.AddIdentity<MovieUser, IdentityRole>()
    .AddEntityFrameworkStores<RepositoryContext>();

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = configuration["JwtSettings:ValidIssuer"],
        ValidAudience = configuration["JwtSettings:ValidAudince"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]))
    };
});



builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddControllers(opt =>
{
    opt.RespectBrowserAcceptHeader = true;
}).AddXmlSerializerFormatters();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        //.WithHeaders(HeaderNames.ContentType, "X-Pagination")
        .AllowAnyHeader()
        .WithExposedHeaders("X-Pagination")
        .AllowAnyMethod()
        // .AllowAnyHeader()
        //.AllowAnyMethod()
        //.AllowAnyOrigin();
        //.AllowCredentials();
        .SetIsOriginAllowedToAllowWildcardSubdomains();
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
app.UseAuthentication();

app.MapControllers();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers()
    .RequireCors("CorsPolicy");
});

app.Run();
