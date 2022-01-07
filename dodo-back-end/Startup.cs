using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DodoApp.Data;
using DodoApp.Installers;
using DodoApp.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

/* TODO: Currency summary per days
    ReadCurrencySummaryPerDayDto:
        Day: the day of the summary being taken
        TotalFundChange; of the day
        TotalProfitChange; of the day
        FundAmount; of the day
        ProfitAmount; of the day
    
    ReadCurrencySummaryPerTransactionDto: ReadCurrencyDto
*/
namespace DodoApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x => 
                typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface)
                .Select(Activator.CreateInstance).Cast<IServiceInstaller>().ToList();
            
            installers.ForEach(installer => installer.InstallServices(services, Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dodo_back_end v1"));
            }

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
