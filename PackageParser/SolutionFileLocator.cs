using System.IO;
using System.Linq;

namespace PackageParser
{
    public interface ISolutionFileLocator
    {
        string GetSolutionFile(string directoryName);
    }

    public class SolutionFileLocator : ISolutionFileLocator
    {
        private readonly PackagesFileConfig _packagesFileConfig;

        public SolutionFileLocator(PackagesFileConfig packagesFileConfig)
        {
            _packagesFileConfig = packagesFileConfig;
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