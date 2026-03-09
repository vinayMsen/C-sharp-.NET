namespace MiddlewareDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initializes the configuration for the web application
            var builder = WebApplication.CreateBuilder(args);
            // Builds the web application based on the configured settings
            var app = builder.Build();
            // Maps HTTP GET requests to the root URL "/" to a method returning "Hello World!"
            app.MapGet("/", () => "Hello World!");
            // Maps HTTP GET requests to "/greet" URL to a method returning a greeting message
            app.MapGet("/greet", () => "Hello from the /greet endpoint!");
            // Maps HTTP GET requests to "/greet/{name}" URL to a method that uses a route parameter
            app.MapGet("/greet/{name}", (string name) => $"Hello, {name}!");
            // Starts the web application which begins listening for incoming requests
            app.Run();
        }
    }
}