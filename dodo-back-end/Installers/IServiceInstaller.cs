using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DodoApp.Installers
{
    public interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}