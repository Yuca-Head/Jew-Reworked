using System.Text.Json;


namespace Jew.Infrastructure.Persistence.Serialization;

public sealed class JsonStorageService<T>(string path, JsonSerializerOptions? options = null)
{

    private readonly string _path = path;
    private readonly JsonSerializerOptions _options = options ?? new JsonSerializerOptions { WriteIndented = true };

    public void Save(IEnumerable<T> values, string? path = null)
    {
        
        path ??= _path;
        string json = JsonSerializer.Serialize(values, _options);
        File.WriteAllText(path, json);
    }
    
    public IEnumerable<T> Load(IEnumerable<T>? defaultValues = null, string? path = null)
    {
        path ??= _path;
        string json;
        if (!File.Exists(path) || string.IsNullOrWhiteSpace(json= File.ReadAllText(path)))
            return defaultValues ?? [];

        return JsonSerializer.Deserialize<IEnumerable<T>>(json, _options) ?? [];
    }

    public void Clear()
    {
        File.WriteAllText(path, "");
    }

}