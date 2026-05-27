namespace Jew.Infrastructure.Persistence.Serialization;

public static class AppPaths
{
    public static string GetDataFilePath(Enums.Environment env, string fileName)
    {
        var envFolder = env switch
        {
            Enums.Environment.Test => "Test",
            _ => "Prod"
        };

        var path = Path.Combine(
            AppContext.BaseDirectory,
            "Persistence",
            "Data",
            envFolder,
            fileName
        );

        EnsureDirectory(path);

        return path;
    }

    private static void EnsureDirectory(string filePath)
    {
        var directory = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrWhiteSpace(directory))
            Directory.CreateDirectory(directory);
    }
}