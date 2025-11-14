using System;
using System.Collections.Generic;

namespace AutoRun.Tests
{
    /// <summary>
    /// 手動測試程式 - 驗證排程器時間比對邏輯
    /// </summary>
    public class SchedulerTest
    {
        public static void TestTimeComparison()
        {
            Console.WriteLine("=== 時間比對邏輯測試 ===\n");

            // 測試案例 1: 正確的時間比對
            var testTime1 = new TimeSpan(9, 30, 0); // 09:30:00
            var now1 = new DateTime(2024, 1, 1, 9, 30, 15); // 2024-01-01 09:30:15
            
            Console.WriteLine($"測試 1: 排程時間 {testTime1:hh\\:mm}");
            Console.WriteLine($"       當前時間 {now1:HH:mm:ss}");
            Console.WriteLine($"       Hours: {testTime1.Hours}, Minutes: {testTime1.Minutes}");
            Console.WriteLine($"       比對結果: {(now1.Hour == testTime1.Hours && now1.Minute == testTime1.Minutes ? "? 匹配" : "? 不匹配")}");
            Console.WriteLine();

            // 測試案例 2: 不匹配的時間
            var testTime2 = new TimeSpan(14, 0, 0); // 14:00:00
            var now2 = new DateTime(2024, 1, 1, 14, 1, 0); // 2024-01-01 14:01:00
            
            Console.WriteLine($"測試 2: 排程時間 {testTime2:hh\\:mm}");
            Console.WriteLine($"       當前時間 {now2:HH:mm:ss}");
            Console.WriteLine($"       Hours: {testTime2.Hours}, Minutes: {testTime2.Minutes}");
            Console.WriteLine($"       比對結果: {(now2.Hour == testTime2.Hours && now2.Minute == testTime2.Minutes ? "? 匹配" : "? 不匹配")}");
            Console.WriteLine();

            // 測試案例 3: 跨午夜時間
            var testTime3 = new TimeSpan(23, 59, 0); // 23:59:00
            var now3 = new DateTime(2024, 1, 1, 23, 59, 45); // 2024-01-01 23:59:45
            
            Console.WriteLine($"測試 3: 排程時間 {testTime3:hh\\:mm}");
            Console.WriteLine($"       當前時間 {now3:HH:mm:ss}");
            Console.WriteLine($"       Hours: {testTime3.Hours}, Minutes: {testTime3.Minutes}");
            Console.WriteLine($"       比對結果: {(now3.Hour == testTime3.Hours && now3.Minute == testTime3.Minutes ? "? 匹配" : "? 不匹配")}");
            Console.WriteLine();

            // 測試案例 4: 星期檢查
            Console.WriteLine("=== 星期檢查測試 ===\n");
            
            var days = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday };
            var testDate1 = new DateTime(2024, 1, 1); // 星期一
            var testDate2 = new DateTime(2024, 1, 2); // 星期二
            var testDate3 = new DateTime(2024, 1, 3); // 星期三
            
            Console.WriteLine($"排程星期: 一、三、五");
            Console.WriteLine($"測試日期 1: {testDate1:yyyy-MM-dd} ({testDate1.DayOfWeek}) - {(days.Contains(testDate1.DayOfWeek) ? "? 匹配" : "? 不匹配")}");
            Console.WriteLine($"測試日期 2: {testDate2:yyyy-MM-dd} ({testDate2.DayOfWeek}) - {(days.Contains(testDate2.DayOfWeek) ? "? 匹配" : "? 不匹配")}");
            Console.WriteLine($"測試日期 3: {testDate3:yyyy-MM-dd} ({testDate3.DayOfWeek}) - {(days.Contains(testDate3.DayOfWeek) ? "? 匹配" : "? 不匹配")}");
            Console.WriteLine();

            // 測試案例 5: 完整排程條件檢查
            Console.WriteLine("=== 完整排程條件測試 ===\n");
            
            var schedule = new ScheduleItem
            {
                Id = Guid.NewGuid(),
                Name = "測試排程",
                ExePath = "notepad.exe",
                TimeOfDay = new TimeSpan(10, 30, 0),
                Days = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday },
                Enabled = true
            };
            
            var testNow = new DateTime(2024, 1, 1, 10, 30, 25); // 星期一 10:30:25
            
            Console.WriteLine($"排程設定:");
            Console.WriteLine($"  名稱: {schedule.Name}");
            Console.WriteLine($"  時間: {schedule.TimeOfDay:hh\\:mm}");
            Console.WriteLine($"  星期: {string.Join(", ", schedule.Days)}");
            Console.WriteLine($"  啟用: {schedule.Enabled}");
            Console.WriteLine();
            Console.WriteLine($"當前時間: {testNow:yyyy-MM-dd HH:mm:ss} ({testNow.DayOfWeek})");
            Console.WriteLine();
            
            bool enabled = schedule.Enabled;
            bool dayMatch = schedule.Days.Contains(testNow.DayOfWeek);
            bool timeMatch = testNow.Hour == schedule.TimeOfDay.Hours && testNow.Minute == schedule.TimeOfDay.Minutes;
            
            Console.WriteLine($"檢查結果:");
            Console.WriteLine($"  啟用檢查: {(enabled ? "? 通過" : "? 未通過")}");
            Console.WriteLine($"  星期檢查: {(dayMatch ? "? 通過" : "? 未通過")}");
            Console.WriteLine($"  時間檢查: {(timeMatch ? "? 通過" : "? 未通過")}");
            Console.WriteLine($"  綜合結果: {(enabled && dayMatch && timeMatch ? "? 應該執行" : "? 不應執行")}");
        }
    }
}
