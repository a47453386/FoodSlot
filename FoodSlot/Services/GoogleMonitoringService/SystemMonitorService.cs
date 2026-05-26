using FoodSlot.ViewModels;
using System.Collections.Concurrent;


namespace FoodSlot.Services.GoogleMonitoringService
{
    public class SystemMonitorService
    {
        private readonly ConcurrentQueue<VMMonitorMetric> _metrics = new();
        private readonly ILogger<SystemMonitorService> _logger;
        public string CurrentStatus { get; set; } = "待機中";

        //限制時間範圍
        private readonly int _maxMemoryHours = 24;
        private readonly int _maxLogDays = 365;

        public SystemMonitorService(ILogger<SystemMonitorService> logger)
        {
            _logger = logger;
        }

        //新增日誌與監控指標
        public void AddLog (double value, string message,string status="處理中")
        {
            CurrentStatus = status;
            var now= DateTime.Now;

            //組裝結構化物件
            var metricEntry = new VMMonitorMetric
            {
                Time = now,
                Value = value,
                Status = status,
                Message = message
            };

            //寫進記憶體
            _metrics.Enqueue(metricEntry);

            //剔除超過24小時前的舊資料
            var cutoffTime = now.AddHours(-_maxMemoryHours);
            while(_metrics.TryPeek(out var oldest)&&oldest.Time< cutoffTime)
            {
                _metrics.TryDequeue(out _);
            }

            //寫入實體檔案(每日一個檔案)
            WriteLogToFile(metricEntry);

           
        }
        //折線圖
        public List<VMMonitorMetric> GetChartData()
        {
            return _metrics.ToList();
        }

        //Log列表－取得所有日誌文字
        public List<string> GetLogs()
        {
            var logs = _metrics
                .Select(x => $"[{x.Time:HH:mm:ss}][{x.Status}]{x.Message}")
                .ToList();
            return logs;
        }

        // 將日誌寫入到 bin/Debug/net8.0/Logs/ 資料夾下
        private void WriteLogToFile(VMMonitorMetric entry)
        {
            try
            {
                string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if(!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                //每天產出一個檔案
                string fileName = $"system-log-{entry.Time:yyyy-MM-dd}.txt";
                string filePath = Path.Combine(logDir, fileName);

                //組合寫入內容
                string fullcontent = $"[{entry.Time:yyyy-MM-dd HH:mm:ss}][{entry.Status}]數值:{entry.Value}|{entry.Message}{Environment.NewLine}";

                lock(logDir)
                {
                    File.AppendAllText(filePath, fullcontent);
                }

            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "實體日誌檔案寫入失敗!\n內容:{Message}", entry.Message);
            }
        }

        //清理超過365天的檔案
        public void DeleteOldLogFiles()
        {
            try
            {
                string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if (!Directory.Exists(logDir)) return;

                var directoryInfo = new DirectoryInfo(logDir);
                var files= directoryInfo.GetFiles("system-log-*.txt");
                
                foreach(var file in files)
                {
                    if(DateTime.Now-file.LastWriteTime>TimeSpan.FromDays(_maxLogDays))
                    {
                        file.Delete();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "清理 365 天前的舊日誌檔案時發生錯誤。");
            }
        }

    }
}
