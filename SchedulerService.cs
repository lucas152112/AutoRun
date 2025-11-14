using System.Diagnostics;

namespace AutoRun;

public class SchedulerService : IDisposable
{
    private readonly System.Threading.Timer _timer;
    private readonly object _lock = new();
    private List<ScheduleItem> _items = new();
    private readonly Dictionary<Guid, DateTime> _lastFiredAtMinute = new();
    private volatile bool _isRunning;

    public bool IsRunning => _isRunning;

    public event EventHandler<string>? OnRun; // message when a process starts
    public event EventHandler<string>? OnDebug; // debug messages for troubleshooting

    public SchedulerService()
    {
        // tick every 5 seconds to catch minute boundaries
        _timer = new System.Threading.Timer(OnTick, null, Timeout.Infinite, Timeout.Infinite);
    }

    public void Start()
    {
        if (_isRunning) return;
        _isRunning = true;
        _timer.Change(0, 5000);
        OnDebug?.Invoke(this, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 排程器已啟動，檢查間隔: 5秒");
    }

    public void Stop()
    {
        if (!_isRunning) return;
        _isRunning = false;
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
        OnDebug?.Invoke(this, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 排程器已停止");
    }

    public void SetItems(IEnumerable<ScheduleItem> items)
    {
        lock (_lock)
        {
            _items = items.ToList();
            // cleanup last fired cache for removed items to avoid growth
            var ids = new HashSet<Guid>(_items.Select(i => i.Id));
            var toRemove = _lastFiredAtMinute.Keys.Where(k => !ids.Contains(k)).ToList();
            foreach (var k in toRemove) _lastFiredAtMinute.Remove(k);
            
            OnDebug?.Invoke(this, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 已載入 {_items.Count} 個排程項目");
        }
    }

    private static DateTime TruncateToMinute(DateTime t) => new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, 0);

    private void OnTick(object? state)
    {
        if (!_isRunning) return;
        try
        {
            var now = DateTime.Now;
            var minuteKey = TruncateToMinute(now);

            List<ScheduleItem> snapshot;
            lock (_lock)
            {
                snapshot = _items.ToList();
            }

            foreach (var item in snapshot)
            {
                if (!item.Enabled) continue;
                if (item.Days is null || item.Days.Count == 0) continue;
                if (!item.Days.Contains(now.DayOfWeek)) continue;

                // match HH:mm - 使用 TimeSpan 的 Hours 和 Minutes 屬性
                var scheduleHour = item.TimeOfDay.Hours;
                var scheduleMinute = item.TimeOfDay.Minutes;
                
                if (now.Hour == scheduleHour && now.Minute == scheduleMinute)
                {
                    bool alreadyFired;
                    lock (_lock)
                    {
                        alreadyFired = _lastFiredAtMinute.TryGetValue(item.Id, out var fired) && fired == minuteKey;
                        if (!alreadyFired)
                        {
                            _lastFiredAtMinute[item.Id] = minuteKey; // mark before start to avoid duplicates
                        }
                    }
                    if (!alreadyFired)
                    {
                        TryStart(item);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // 記錄錯誤而不是吞掉
            OnRun?.Invoke(this, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 排程器錯誤: {ex.Message}");
        }
    }

    private void TryStart(ScheduleItem item)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = item.ExePath,
                Arguments = item.Arguments,
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(item.ExePath) ?? Environment.CurrentDirectory
            };
            Process.Start(psi);
            OnRun?.Invoke(this, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 已啟動: {item.Name} -> {item.ExePath} {item.Arguments}");
        }
        catch (Exception ex)
        {
            OnRun?.Invoke(this, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 錯誤: {item.Name} -> {ex.Message}");
        }
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}
