using MinimalAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalAPI.Interfaces
{
    /// <summary>
    /// Provides methods for persisting customer data.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Saves a collection of customer data.
        /// </summary>
        /// <param name="customers">The customer data to save.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveCustomersAsync(IEnumerable<CustomerData> customers);
    }
}