using System.Collections.Generic;

namespace PackageParser.Data
{
    public class PackagesConfig
    {
        public int Id { get; set; } 
        public string Folder { get; set; }
        public string FullPath { get; set; }
        public string TopLevelFolder { get; set; }
        public string SolutionFile { get; set; }
        public IEnumerable<Package> Packages { get; set; }
    }
}