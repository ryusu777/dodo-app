using DodoApp.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DodoApp.Installers
{
    public class RepositoryInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGoodsRepo, GoodsRepo>();
            services.AddScoped<ITransactionRepo, TransactionRepo>();
        }
    }
}