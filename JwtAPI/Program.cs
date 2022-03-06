using JwtAPI.Helpers;
using JwtAPI.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// configure strongly typed settings object
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

string signingKey = builder.Configuration.GetSection("AppSettings:Secret").Value;

// Add services to the container.

services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();

// the option param will allow to send an auth param in the request header.
services.AddSwaggerGen(options => 
                        {
                            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                            {
                                Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer cJvTjk3nsm36buAYS_7MCg'",
                                In = ParameterLocation.Header,
                                Name = "Authorization",
                                Type = SecuritySchemeType.ApiKey
                                
                            });
                            options.OperationFilter<SecurityRequirementsOperationFilter>();
                            options.SwaggerDoc("v1", new OpenApiInfo() 
                            {
                                Title = "JwtApi",
                                Version = "v1"
                            });
                        });

// Will check the JWT for Authentication
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                 ValidateIssuer = false,
                 ValidateAudience = false
             };
         });

// configure DI for application services
services.AddScoped<IUserService, UserService>();
services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
