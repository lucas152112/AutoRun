using System.Text.Json;

namespace AutoRun;

public static class Storage
{
    private static string DataPath => Path.Combine(AppContext.BaseDirectory, "setting.json");
    private static string SettingsPath => Path.Combine(AppContext.BaseDirectory, "settings.json");

    public static async Task<List<ScheduleItem>> LoadAsync()
    {
        try
        {
            if (!File.Exists(DataPath)) return new List<ScheduleItem>();
            await using var fs = File.OpenRead(DataPath);
            var items = await JsonSerializer.DeserializeAsync<List<ScheduleItem>>(fs, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            });
            return items ?? new List<ScheduleItem>();
        }
        catch
        {
            return new List<ScheduleItem>();
        }
    }

    public static async Task SaveAsync(IEnumerable<ScheduleItem> items)
    {
        await using var fs = File.Create(DataPath);
        await JsonSerializer.SerializeAsync(fs, items, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }

    public static async Task<AppSettings> LoadSettingsAsync()
    {
        try
        {
            if (!File.Exists(SettingsPath)) return new AppSettings();
            await using var fs = File.OpenRead(SettingsPath);
            var s = await JsonSerializer.DeserializeAsync<AppSettings>(fs, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            });
            return s ?? new AppSettings();
        }
        catch
        {
            return new AppSettings();
        }
    }

    public static async Task SaveSettingsAsync(AppSettings settings)
    {
        await using var fs = File.Create(SettingsPath);
        await JsonSerializer.SerializeAsync(fs, settings, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}
