using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PackageParser.Data;

namespace PackageParser
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureServices((hostContext, services) =>
                                          {
                                              services
                                                  .AddOptions()
                                                  .Configure<PackagesFileConfig>(hostContext.Configuration.GetSection("PackagesFiles"))
                                                  .AddDbContext<PackagesContext>(opt =>
                                                                                     opt.UseSqlServer(hostContext.Configuration
                                                                                                                 .GetConnectionString("PackagesContext")))
                                                  .AddSingleton<IPackagesFilesLocator, PackagesFilesLocator>()
                                                  .AddSingleton<IPackagesFileReader, PackagesFileReader>()
                                                  .AddSingleton<ISolutionFileLocator, SolutionFileLocator>()
                                                  
                                                  .AddHostedService<PackagesFileReader>();
                                          });
        }
    }
}