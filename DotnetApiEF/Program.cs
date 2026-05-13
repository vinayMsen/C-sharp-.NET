using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using AutoMapper;
using System.Configuration.Assemblies;
using Microsoft.Extensions.Configuration;
using DotnetApiEF.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(); // add controllers to the service collection
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



    builder.Services.AddCors((options) =>
        {
            options.AddPolicy("DevCors", (corsBuilder) =>
                {
                    corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });

        });

    builder.Services.AddScoped<IUserRepository, UserRepository>();

            // JWT validation 
            string? tokenKeyString = builder.Configuration.GetSection("AppSettings:TokenKey").Value;
        
                        SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                tokenKeyString != null ? tokenKeyString : ""
                            )
                        );
                        
            
     // using These symmetricsecurity key to create tokenvalidatoin parameters corsBuilder.WithOrigins

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
               IssuerSigningKey = tokenKey, // the key used to sign the token
               ValidateIssuer = false,      // we are not validating the issuer of the token
               ValidateIssuerSigningKey = true, // we are validating the signing key of the token
               ValidateAudience = false // we are not validating the audience of the token
            };


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                });


     
    var app = builder.Build();
     
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseCors("DevCors");
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseCors("ProdCors");
        app.UseHttpsRedirection();
    }


app.UseAuthentication(); // add authentication middleware to the pipeline to check for the presence of 
// a valid JWT token in the request headers

app.UseAuthorization(); // add authorization middleware to the pipeline

app.MapControllers(); 

app.Run();


