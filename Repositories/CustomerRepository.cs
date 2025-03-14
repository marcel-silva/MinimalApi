using MinimalAPI.Models;
using MinimalAPI.Services;
using MinimalAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MinimalAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly string _basePath;

        public CustomerRepository(IConfiguration configuration, IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
            var basePathValue = configuration.GetSection("FileStorage:BasePath").Value;
            if (string.IsNullOrWhiteSpace(basePathValue))
            {
                throw new ArgumentException("BasePath configuration is missing or empty.");
            }
            _basePath = basePathValue;
        }

        public async Task SaveCustomersAsync(IEnumerable<CustomerData> customers)
        {
            // Serialize the customer collection to JSON
            var json = JsonSerializer.Serialize(customers);
            // Delegate the file writing operation to the file storage service
            await _fileStorageService.StoreFileAsync(json, _basePath);
        }
    }
}
