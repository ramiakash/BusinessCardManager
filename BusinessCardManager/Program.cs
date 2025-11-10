using BusinessCardManager.API.Middleware;
using BusinessCardManager.Application.Common.Interfaces;
using BusinessCardManager.Domain.Interfaces;
using BusinessCardManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using BusinessCardManager.Infrastructure.FileServices.Exporters;
using BusinessCardManager.Infrastructure.FileServices.Parsers;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        b => b.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("BusinessCardManager.Application")));

builder.Services.AddAutoMapper(cfg =>
    cfg.AddMaps(Assembly.Load("BusinessCardManager.Application")));

builder.Services.AddValidatorsFromAssembly(Assembly.Load("BusinessCardManager.Application"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBusinessCardRepository, BusinessCardRepository>();
builder.Services.AddScoped<IFileExporter, CsvFileExporter>();
builder.Services.AddScoped<IFileExporter, XmlFileExporter>();
builder.Services.AddScoped<IFileParser, CsvFileParser>();
builder.Services.AddScoped<IFileParser, XmlFileParser>();
builder.Services.AddScoped<IQrCodeParser, QrCodeParser>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();