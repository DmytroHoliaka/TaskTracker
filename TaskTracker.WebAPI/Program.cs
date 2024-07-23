using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Globalization;
using TaskTracker.BLL.Abstractions;
using TaskTracker.BLL.Behaviours;
using TaskTracker.BLL.Profiles;
using TaskTracker.DAL.EntityFramework;
using TaskTracker.WebAPI.ServiceExtensions;

namespace TaskTracker.WebAPI;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddDbContextConfiguration(builder.Configuration)
            .AddCorsConfiguration()
            .AddCommonServices()
            .AddMediatRConfiguration()
            .AddAutoMapperConfiguration()
            .AddFluentValidationConfiguration()
            .AddCustomConfiguration();

        builder.Host.AddSerilogConfiguration();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors();

        app.UseAuthorization();

        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        app.Run();
    }
}