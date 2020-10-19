using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PackageParser.Data;
using PackageParser.Xml;

namespace PackageParser
{
    public class PackagesFileReader : IPackagesFileReader, IHostedService
    {
        private readonly PackagesContext _context;
        private readonly IPackagesFilesLocator _packagesFilesLocator;
        private readonly ISolutionFileLocator _solutionFileLocator;
        private readonly PackagesFileConfig _packagesFileConfig;

        public PackagesFileReader(IPackagesFilesLocator packagesFilesLocator,
                                  PackagesContext context,
                                  ISolutionFileLocator solutionFileLocator,
                                  IOptions<PackagesFileConfig> packagesFileConfig)
        {
            _packagesFilesLocator = packagesFilesLocator;
            _context = context;
            _solutionFileLocator = solutionFileLocator;
            _packagesFileConfig = packagesFileConfig.Value;
        }

        public void ReadAllPackagesFiles()
        {
            var packagesFiles = _packagesFilesLocator.GetPackagesFiles();

            foreach (var packagesFile in packagesFiles)
            {
                var packagesConfig = CreatePackagesConfig(packagesFile);
                _context.PackagesConfigs.Add(packagesConfig);
            }

            _context.SaveChanges();
        }

        private PackagesConfig CreatePackagesConfig(string packagesFile)
        {
            var directoryName = Path.GetDirectoryName(packagesFile);
            var topLevelFolder = GetTopLevelFolder(packagesFile);
            var solutionFile = _solutionFileLocator.GetSolutionFile(directoryName);
            var packages = GetPackages(packagesFile);

            return new PackagesConfig
                   {
                       FullPath = packagesFile,
                       Folder = directoryName,
                       TopLevelFolder = topLevelFolder,
                       SolutionFile = solutionFile,
                       Packages = packages
                   };
        }

        private string GetTopLevelFolder(string packagesFile)
        {
            return packagesFile.Remove(0, _packagesFileConfig.RootFolder.Length + 1)
                               .Split('\\')
                               .First();
        }

        private IEnumerable<Package> GetPackages(string packagesFile)
        {
            var serializer = new XmlSerializer(typeof(PackagesRoot));
            var packagesXml = (PackagesRoot) serializer.Deserialize(File.OpenText(packagesFile));

            return packagesXml.Package.Select(p => new Package
                                                   {
                                                       Name = p.Id,
                                                       Version = p.Version
                                                   })
                              .ToList();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ReadAllPackagesFiles();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}