using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.ResponseCompression;
using System.Threading.Channels;
using API.Application.Middleware;
using API.Application.Workers;
using API.Application.Hubs;
using API.Domain.Model;
using API.Domain.Service;
using API.Domain.Service.Interface;
using API.Infra.DataAcess.Connection;
using API.Infra.DataAcess.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IExcelImportService, ExcelImportService>();
builder.Services.AddTransient<IExcelImportRepository, ExcelImportRepository>();

// Adiciona o HostedService (Worker)
builder.Services.AddHostedService<ImportWorker>();

// Adiciona o Channel para comunicação
builder.Services.AddSingleton(Channel.CreateUnbounded<ExcelInfo>());
builder.Services.AddTransient<DbSession>();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        ["application/octet-stream"]);
});


var app = builder.Build();

app.UseResponseCompression();

app.UseCors("AllowAll");

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ImportHub>("/hub");

app.Run();
