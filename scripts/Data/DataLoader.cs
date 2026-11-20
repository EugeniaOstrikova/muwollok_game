using System.Text.Json;
using Godot;

namespace Muwollok.Data;

public static class DataLoader
{
    // В JSON поля с маленькой буквы (id), в C# с большой (Id)
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static T Load<T>(string path)
    {
        // Godot.FileAccess умеет читать res:// — и в редакторе, и в собранной игре
        string json = FileAccess.GetFileAsString(path);
        if (string.IsNullOrEmpty(json))
        {
            GD.PushError($"Не удалось прочитать файл: {path}");
            return default;
        }
        return JsonSerializer.Deserialize<T>(json, Options);
    }

    public static FormData LoadForm(string id) => Load<FormData>($"res://data/forms/{id}.json");
    public static CaseData LoadCase(string id) => Load<CaseData>($"res://data/cases/{id}.json");
}