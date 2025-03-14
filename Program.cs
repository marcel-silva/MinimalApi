using MinimalAPI.Extensions;
using MinimalAPI.EndpointDefinitions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Register endpoint definitions by scanning for types that implement IEndpointDefinition
builder.Services.AddEndpointDefinitions(typeof(FileEndpointDefinition));

var app = builder.Build();

// Global exception handler
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (exceptionFeature != null)
        {
            if (exceptionFeature.Error is JsonException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Bad Request: " + exceptionFeature.Error.Message);
            }
            else
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An unexpected error occurred.");
            }
        }
    });
});


app.UseHttpsRedirection();

app.UseEndpointDefinitions();

await app.RunAsync();
