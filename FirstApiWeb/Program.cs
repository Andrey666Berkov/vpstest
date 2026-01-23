using FirstApiWeb.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductsService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("react", p =>
        p.WithOrigins("https://berkovtest.ru", "http://berkovtest.ru")
            .AllowAnyHeader()
            .AllowAnyMethod());
});
// 🔥 Автоприменение миграций при старте

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
    Console.WriteLine($"Running in env: {app.Environment.EnvironmentName}");
}
//..dsfgbdf

app.UseCors("react");

app.MapControllers();

app.Run();
//....ююютbdfcsdffgbюfdsfdscd .

