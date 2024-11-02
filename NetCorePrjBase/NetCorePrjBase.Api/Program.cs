using FluentValidation.AspNetCore;
using NetCorePrjBase.Api;
using NetCorePrjBase.BL.DTOs.INPUT.SignUp.Login;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        options.RegisterValidatorsFromAssemblyContaining<LoginUserInfoQueryValidator>();
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ConfigurationFluentValidation();
builder.ConfigurationDatabase();
builder.AddJWTBearerConfig();
builder.ConfigurationCORS();
builder.ConfigurationServiceGzip();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigCORS();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCaching();

app.MapControllers();

app.Run();
