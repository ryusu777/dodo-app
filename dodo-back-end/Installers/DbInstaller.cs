using DodoApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DodoApp.Installers
{
    public class DbInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DodoAppContext>(options =>
            {
                options.UseMySql(
                    configuration.GetConnectionString("MySqlConnection"), 
                    ServerVersion.AutoDetect(configuration.GetConnectionString("MySqlConnection")));
            });
        }
    }
}