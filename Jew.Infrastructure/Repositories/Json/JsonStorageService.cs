using System.Text.Json;

namespace Jew.Infrastructure.Repositories.Json;

public readonly struct JsonStorageService<T>(string path, JsonSerializerOptions? options = null)
{
    private readonly string _path = path;
    private readonly JsonSerializerOptions _options = options ?? new JsonSerializerOptions { WriteIndented = true };

    public void Save(IEnumerable<T> values)
    {
        string json =
            JsonSerializer.Serialize(values, _options);

        File.WriteAllText(_path, json);
    }
    
    public IEnumerable<T> Load(IEnumerable<T>? defaultValues = null)
    {
        if (!File.Exists(_path))
            return defaultValues ?? [];

        string json = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<IEnumerable<T>>(json, _options) ?? [];
    }


}