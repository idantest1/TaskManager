using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Data;
using TaskManager.Data.Interfaces;
using TaskManager.Data.Repositories;
using TaskManager.Logic;
using TaskManager.Logic.Interfaces;

namespace TaskManager.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<TasksDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
