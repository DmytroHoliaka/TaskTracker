using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskTracker.BLL.Abstractions;
using TaskTracker.BLL.Profiles;
using TaskTracker.DAL.EntityFramework;

namespace TaskTracker.WebAPI;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<TaskTrackerContext>(
            options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("TaskTrackerContext") ??
                    throw new InvalidOperationException("Connection string 'TaskTrackerContext' not found.")));

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("https://localhost:5173");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
        });

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();
        
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        builder.Services.AddMediatR(
            config => config.RegisterServicesFromAssemblies(TaskTracker.BLL.AssemblyReference.Assembly));
       
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddAutoMapper(config =>
        {
            config.AddProfile<TodoItemProfile>();
        });

        var app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();

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