using CloudCostumers.API.Config;
using CloudCostumers.API.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

// Add services to the container.
builder.Services.AddControllers();  // Set up MVC controller services.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();  // Add support for API endpoint exploration.
builder.Services.AddSwaggerGen();  // Configure Swagger/OpenAPI documentation generation.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Enable Swagger documentation in development mode.
    app.UseSwaggerUI();  // Enable Swagger UI for interactive API documentation.
}

app.UseHttpsRedirection();  // Redirect HTTP requests to HTTPS.

app.UseAuthorization();  // Apply authorization middleware for protected routes.

app.MapControllers();  // Map the API controllers to routes.

app.Run();  // Start the application.

void ConfigureServices(IServiceCollection services)  {
    services.Configure<UsersApiOptions>(
        builder.Configuration.GetSection("UsersApiOptions")
    );
    services.AddTransient<IUsersService, UsersService>();  // Register transient service for user management.
    services.AddHttpClient<IUsersService, UsersService>();
    // Add more services here as needed for the application.
}
