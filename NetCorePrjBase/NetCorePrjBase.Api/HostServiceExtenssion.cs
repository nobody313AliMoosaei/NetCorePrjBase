using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetCorePrjBase.BL.DTOs.INPUT.SignUp.Login;
using NetCorePrjBaseDL.ApplicationDbContext;
using System.Runtime.CompilerServices;
using System.Text;

namespace NetCorePrjBase.Api
{
    public static class HostServiceExtenssion
    {
        public const string Development_CORS = "developmeent_CORS";
        public const string DefualtCORS = "DEFUALT_CORS";
        #region Builder
        public static WebApplicationBuilder ConfigurationDatabase(this WebApplicationBuilder  builder)
        {
            builder.Services.AddDbContext<Application_DbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("MainAppDB"));
            });
            return builder;
        }

        public static WebApplicationBuilder AddJWTBearerConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["JWTConfiguration:issuer"],
                    ValidAudience = builder.Configuration["JWTConfiguration:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfiguration:key"])),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateActor = true,
                };
            });
            return builder;
        }

        public static WebApplicationBuilder ConfigurationFluentValidation(this WebApplicationBuilder builder)
        {
            builder.Services.AddFluentValidation(e =>
            {
                e.ConfigureClientsideValidation(enabled: true);
                e.RegisterValidatorsFromAssemblyContaining<LoginUserInfoQueryValidator>();
            });
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            return builder;
        }
        public static WebApplicationBuilder ConfigurationCORS(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(option =>
            {
                option.AddPolicy(DefualtCORS, opt =>
                {
                    opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
                option.AddPolicy(Development_CORS, opt =>
                {
                    opt.WithOrigins("localhost:3000", "http://localhost:3000", "https://locahost:3000")
                    .AllowAnyHeader().AllowAnyMethod()
                    .AllowCredentials();
                });

            });

            return builder;
        }

        public static WebApplicationBuilder AddMemoryCache(this WebApplicationBuilder builder)
        {
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromDays(1);
            });

            return builder;
        }

        public static WebApplicationBuilder ConfigurationServiceGzip(this WebApplicationBuilder builder)
        {
            builder.Services.AddResponseCompression(option =>
            {
                option.Providers.Add<GzipCompressionProvider>();
                option.EnableForHttps = true;
            });
            builder.Services.Configure<GzipCompressionProviderOptions>(option =>
            {
                option.Level = System.IO.Compression.CompressionLevel.Optimal;
            });

            return builder;
        }


        #endregion


        #region Application
        public static WebApplication ConfigCORS(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
                app.UseCors(Development_CORS);
            else
                app.UseCors(DefualtCORS);
            return app;
        }
        #endregion
    }
}
