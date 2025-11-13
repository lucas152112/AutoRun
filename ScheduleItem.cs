using System.Text.Json.Serialization;

namespace AutoRun;

public class ScheduleItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string ExePath { get; set; } = string.Empty;

    public string Arguments { get; set; } = string.Empty;

    // Time to run (local time)
    public TimeSpan TimeOfDay { get; set; }

    // Days of week to run
    public List<DayOfWeek> Days { get; set; } = new();

    // Enable/disable the schedule
    public bool Enabled { get; set; } = true;

    [JsonIgnore]
    public string DaysDisplay
    {
        get
        {
            if (Days.Count == 0)
                return "(無)";
            
            // 檢查是否為每天（包含所有 7 天）
            var allDays = new[]
            {
                DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday
            };
            
            if (Days.Count == 7 && allDays.All(d => Days.Contains(d)))
                return "每天";
            
            return string.Join(',', Days.Select(WeekdayDisplay));
        }
    }

    [JsonIgnore]
    public string TimeDisplay => DateTime.Today.Add(TimeOfDay).ToString("HH:mm");

    private static string WeekdayDisplay(DayOfWeek d)
    {
        // Chinese short names Monday-Sunday
        return d switch
        {
            DayOfWeek.Monday => "一",
            DayOfWeek.Tuesday => "二",
            DayOfWeek.Wednesday => "三",
            DayOfWeek.Thursday => "四",
            DayOfWeek.Friday => "五",
            DayOfWeek.Saturday => "六",
            DayOfWeek.Sunday => "日",
            _ => d.ToString()
        };
    }
}
