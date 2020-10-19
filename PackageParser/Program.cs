using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PackageParser.Data;

namespace PackageParser
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appSettings.json");

            var configuration = configBuilder.Build();

            var serviceCollection = new ServiceCollection()
                                    .AddOptions()
                                    .Configure<PackagesFileConfig>(configuration.GetSection("PackagesFiles"))
                                    .AddDbContext<PackagesContext>(opt =>
                                                                       opt.UseSqlServer(configuration
                                                                                            .GetConnectionString("PackagesContext")))
                                    .AddSingleton<PackagesFilesLocator>()
                                    .AddSingleton<PackagesFileReader>()
                                    .AddSingleton<SolutionFileLocator>();

            var provider = serviceCollection.BuildServiceProvider();
            var options = provider.GetService<IOptions<PackagesFileConfig>>();
            var reader = provider.GetService<PackagesFileReader>();

            reader .ReadAllPackagesFiles();
        }
    }
}