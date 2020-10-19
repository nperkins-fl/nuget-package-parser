using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Options;

namespace PackageParser
{
    public class PackagesFilesLocator : IPackagesFilesLocator
    {
        private readonly PackagesFileConfig _packagesFileConfig;

        public PackagesFilesLocator(IOptions<PackagesFileConfig> packagesFileConfig)
        {
            _packagesFileConfig = packagesFileConfig.Value;
        }


        public IEnumerable<string> GetPackagesFiles()
        {
            return Directory.GetFiles(_packagesFileConfig.RootFolder, _packagesFileConfig.SearchPattern, SearchOption.AllDirectories);
        }
    }
}