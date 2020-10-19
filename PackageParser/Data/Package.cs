namespace PackageParser.Data
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public int PackagesConfigId { get; set; }

        public PackagesConfig PackagesConfig { get; set; }
    }
}