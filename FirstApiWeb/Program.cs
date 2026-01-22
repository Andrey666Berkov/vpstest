var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("react", p =>
        p.WithOrigins("https://berkovtest.ru", "http://berkovtest.ru")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//..dsfgbdf

app.UseCors("react");

app.MapControllers();

app.Run();
//....ююютbdfcsdffgbю

