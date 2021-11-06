using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DodoApp.Installers
{
    public class MapperInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Startup));
        }
    }
}