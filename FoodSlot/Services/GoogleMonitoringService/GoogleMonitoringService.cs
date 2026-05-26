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

        public async Task<List<APIRequestLog>> FetchAndSaveAsync(int hours=1)
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
                Filter = "metric.type=\"serviceruntime.googleapis.com/api/request_count\"",
                Interval = new TimeInterval
                {
                    StartTime = Timestamp.FromDateTime(DateTime.UtcNow.AddHours(-hours)),
                    EndTime = Timestamp.FromDateTime(DateTime.UtcNow)
                },
                View = ListTimeSeriesRequest.Types.TimeSeriesView.Full
            };

            //數據處理與計算
            var response = client.ListTimeSeries(request);// 執行查詢

            //資料轉換與統計處理 (LINQ 邏輯)
            var summaryData = response
                .SelectMany(series => series.Points.Select(point => new
                {
                    apiName = series.Resource.Labels.ContainsKey("method")
                    ? series.Resource.Labels["method"] : 
                    series.Resource.Labels.ContainsKey("service")
                    ? series.Resource.Labels["service"]
                    :"Unknown",

                    count = point.Value.Int64Value
                }))
                .GroupBy(x => x.apiName)
                .Select(g => new APIRequestLog
                {
                    apiName = g.Key,                            
                    totalRequests = (int)g.Sum(x => x.count),   
                    updatedAt = DateTime.Now                    
                })              
                .ToList();
       
            //資料庫儲存處理
            if (summaryData.Any())
            {
                _context.APIRequestLog.AddRange(summaryData);
                await _context.SaveChangesAsync();
            }

            return summaryData;
        }
    }
}
