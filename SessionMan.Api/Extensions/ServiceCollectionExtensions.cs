using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SessionMan.Api.Data;

namespace SessionMan.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMySqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new SqlConnectionStringBuilder(
                configuration.GetConnectionString("SessionManDb")) {Password = configuration["DbPassword"]};
            services.AddPooledDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(builder.ConnectionString));
        }
    }
}