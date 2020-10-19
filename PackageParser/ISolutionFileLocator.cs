namespace PackageParser
{
    public interface ISolutionFileLocator
    {
        string GetSolutionFile(string directoryName);
    }
}