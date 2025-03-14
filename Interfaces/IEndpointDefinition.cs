namespace MinimalAPI.Interfaces
{
    /// <summary>
    /// Defines methods for registering services and endpoints.
    /// </summary>
    public interface IEndpointDefinition
    {
        /// <summary>
        /// Registers services required by the endpoint.
        /// </summary>
        /// <param name="services">The service collection to which the services are added.</param>
        void DefineServices(IServiceCollection services);

        /// <summary>
        /// Maps the endpoints to the provided web application.
        /// </summary>
        /// <param name="app">The web application to which endpoints are mapped.</param>
        void DefineEndpoints(WebApplication app);
    }
}