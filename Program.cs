using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Configuration;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add configuration from appsettings.json
var configuration = builder.Configuration;

var app = builder.Build();

// Map POST endpoint for storing JSON data
app.MapPost("/store", async (HttpContext httpContext) =>
{
    // Read the JSON request body
    using var reader = new StreamReader(httpContext.Request.Body);
    var jsonData = await reader.ReadToEndAsync();
    
    // Check if the body is empty
    if (string.IsNullOrWhiteSpace(jsonData))
    {
        return Results.BadRequest("Request body is empty");
    }

    // Retrieve the base path from configuration
    var basePath = configuration.GetSection("FileStorage:BasePath").Value;

    // Check if basePath is null or empty
    if (string.IsNullOrWhiteSpace(basePath))
    {
        return Results.Problem("BasePath configuration is missing or empty.");
    }

    // Define directories
    string usersDir = Path.Combine(basePath, "Users");
    string inDir = Path.Combine(usersDir, "IN");

    // Create directories if they do not exist
    if (!Directory.Exists(usersDir))
    {
        Directory.CreateDirectory(usersDir);
    }
    if (!Directory.Exists(inDir))
    {
        Directory.CreateDirectory(inDir);
    }

    // Create a unique filename using timestamp
    string fileName = $"data_{DateTime.Now:yyyyMMddHHmmssfff}.json";
    string filePath = Path.Combine(inDir, fileName);

    // Save the JSON data to a file
    await File.WriteAllTextAsync(filePath, jsonData);

    return Results.Ok(new { Message = "Data stored successfully", FileName = fileName });
});

app.Run();

