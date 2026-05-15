using FoodSlot.Models;
using Google.Api;
using Google.Api.Gax.ResourceNames;
using Google.Cloud.Monitoring.V3;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace FoodSlot.Services.GoogleMonitoringService
{
    public class GoogleMonitoringService
    {
        private readonly FoodSlotContext _context;
        private readonly string _projectId;
        private readonly string _keyPath;

        public GoogleMonitoringService(FoodSlotContext context, IConfiguration config)
        {
            _context = context;
            _projectId = config["GoogleCloud:ProjectId"]!;
            _keyPath = config["GoogleCloud:KeyPath"]!;
        }

        public async Task FetchAndSaveAsync()
        {
            // 設定金鑰路徑
            Environment.SetEnvironmentVariable(
                "GOOGLE_APPLICATION_CREDENTIALS", _keyPath);

            //身份驗證與初始化
            var client = MetricServiceClient.Create();
            var projectName = ProjectName.FromProject(_projectId);

            //建立查詢請求 
            var request = new ListTimeSeriesRequest
            {
                Name = projectName.ToString(),
                Filter = "metric.type =\"serviceruntime.googleapis.com/api/request_count\" AND resource.labels.service=\"places.googleapis.com\"",
                Interval = new TimeInterval
                {
                    StartTime = Timestamp.FromDateTime(DateTime.UtcNow.AddHours(-1)),
                    EndTime = Timestamp.FromDateTime(DateTime.UtcNow)
                },
                View = ListTimeSeriesRequest.Types.TimeSeriesView.Full
            };

            //數據處理與計算
            var response = client.ListTimeSeries(request);// 執行查詢

            foreach (var timeSeries in response)
            {
                var apiName = timeSeries.Metric.Labels
                    .GetValueOrDefault("method", "");

                if (string.IsNullOrEmpty(apiName) || apiName == "unknown") continue;

                var totalRequests = timeSeries.Points
                    .Sum(p => (int)p.Value.Int64Value);

                //同步到 SQL Server
                var apiRequestLogs = new APIRequestLog
                {
                    apiName = apiName,
                    totalRequests = totalRequests,
                    updatedAt = DateTime.Now
                };

               _context.APIRequestLog.Add(apiRequestLogs);
            }

            await _context.SaveChangesAsync();
        }
    }
}
