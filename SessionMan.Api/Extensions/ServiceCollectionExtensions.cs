using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SessionMan.DataAccess.Data;
using SessionMan.DataAccess.Repository;
using SessionMan.DataAccess.Repository.IRepository;

namespace SessionMan.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMySqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new SqlConnectionStringBuilder(
                configuration.GetConnectionString("SessionManDb")) {Password = configuration["DbPassword"]};
            services.AddPooledDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(builder.ConnectionString, b => b.MigrationsAssembly("SessionMan.Api")));
        }

        public static void AddMyScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientSqlRepository>();
            services.AddScoped<ISessionRepository, SessionSqlRepository>();
        }
    }
}