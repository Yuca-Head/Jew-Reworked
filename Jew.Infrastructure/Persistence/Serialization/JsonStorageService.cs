using System.Text.Json;
using System.Threading.Tasks;


namespace Jew.Infrastructure.Persistence.Serialization;

public sealed class JsonStorageService<T>(string path, JsonSerializerOptions? options = null)
{

    private readonly string _path = path ?? "";
    private readonly JsonSerializerOptions _options = options ?? new JsonSerializerOptions { WriteIndented = true };

    public async Task SaveAsync(IEnumerable<T> values, string? path = null)
    {
        
        path ??= _path;
        string json = JsonSerializer.Serialize(values, _options);
        await File.WriteAllTextAsync(path, json);
    }
    
    public async Task<IEnumerable<T>> LoadAsync(IEnumerable<T>? defaultValues = null, string? path = null)
    {
        path ??= _path;
        string json;
        if (!File.Exists(path) || string.IsNullOrWhiteSpace(json = await File.ReadAllTextAsync(path)))
            return defaultValues ?? [];

        return JsonSerializer.Deserialize<IEnumerable<T>>(json, _options) ?? [];
    }

    public void Clear()
    {
        File.WriteAllText(path, "");
    }

}