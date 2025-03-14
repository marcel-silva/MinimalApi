using MinimalAPI.Models;
using MinimalAPI.Repositories;
using MinimalAPI.Interfaces;
using MinimalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPI.EndpointDefinitions
{
    public class FileEndpointDefinition : IEndpointDefinition
    {
        public void DefineServices(IServiceCollection services)
        {
            // Register the file storage service and the customer repository
            services.AddSingleton<IFileStorageService, FileStorageService>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
        }

        public void DefineEndpoints(WebApplication app)
        {
            // Use model binding for CustomerData collection
            app.MapPost("/store", StoreRequest);
        }

        internal async Task<IResult> StoreRequest(
            [FromBody] IEnumerable<CustomerData> customers, 
            IConfiguration configuration, 
            ICustomerRepository repository)
        {
            if (customers == null || !customers.Any())
            {
                return Results.BadRequest("No customer data provided.");
            }

            // Validate each customer using DataAnnotations
            var allErrors = new List<string>();
            foreach (var customer in customers)
            {
                var context = new ValidationContext(customer);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(customer, context, validationResults, true);
                if (!isValid)
                {
                    allErrors.AddRange(validationResults.Select(r => r.ErrorMessage).OfType<string>());
                }
            }

            if (allErrors.Any())
            {
                return Results.BadRequest(string.Join("; ", allErrors));
            }

            try
            {
                // Use the repository to save the validated customer data
                await repository.SaveCustomersAsync(customers);
                return Results.Ok(new { Message = "Customer data stored successfully." });
            }
            catch (Exception ex)
            {
                return Results.Problem($"Error storing customer data: {ex.Message}");
            }
        }
    }
}
