using System.IO;
using System.Linq;
using Microsoft.Extensions.Options;

namespace PackageParser
{
    public class SolutionFileLocator : ISolutionFileLocator
    {
        private readonly PackagesFileConfig _packagesFileConfig;

        public SolutionFileLocator(IOptions<PackagesFileConfig> packagesFileConfig)
        {
            _packagesFileConfig = packagesFileConfig.Value;
        }

        public string GetSolutionFile(string directoryName)
        {
            var directoryInfo = new DirectoryInfo(directoryName);
            return GetSolutionFile(directoryInfo);
        }

        private string GetSolutionFile(DirectoryInfo directoryInfo)
        {
            while (directoryInfo != null)
            {
                if (directoryInfo.FullName == _packagesFileConfig.RootFolder)
                {
                    return "";
                }

                var solutionFiles = directoryInfo.GetFiles("*.sln");
                if (solutionFiles.Any())
                {
                    return solutionFiles.First().Name;
                }

                directoryInfo = directoryInfo.Parent;
            }

            return "";
        }
    }
}