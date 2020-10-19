using System.Collections.Generic;

namespace PackageParser
{
    public interface IPackagesFilesLocator
    {
        IEnumerable<string> GetPackagesFiles();
    }
}