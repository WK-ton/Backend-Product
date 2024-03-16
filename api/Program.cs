using api.Data;
using api.Repository;
using api.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


//interface
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICarsBangkhenRepository, CarsBangkhenRepository>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add controllers service
builder.Services.AddControllers();

var app = builder.Build();

// Enable Swagger and Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
    });
    app.UseRouting();
}

// Map endpoints to ProductController
app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action}/{id?}",
    defaults: new { controller = "Product", action = "GetAllProducts" } // Assuming GetAllProducts is an action in ProductController
);

app.UseHttpsRedirection();
app.Run();
