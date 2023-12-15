using ImageResizer.Repository.Interfaces;
using ImageResizer.Repository;
using ImageResizer.DTOs;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve case sensitivity
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.Configure<ConfigPNG>(builder.Configuration.GetSection("ConfigPNG"));

builder.Services.AddTransient<IImageRepository>(provider =>
{
    var config = provider.GetRequiredService<IOptions<ConfigPNG>>().Value;

    return new ImageRepository(
        config.TinyPng.ApiKey,
        config.Ftp.Host,
        config.Ftp.Username,
        config.Ftp.Password
    );
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
