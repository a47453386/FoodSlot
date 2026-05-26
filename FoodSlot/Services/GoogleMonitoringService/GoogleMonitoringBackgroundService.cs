using FoodSlot.Models;

namespace FoodSlot.Services.GoogleMonitoringService
{
    public class GoogleMonitoringBackgroundService:BackgroundService
    {
        private readonly ILogger<GoogleMonitoringBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly SystemMonitorService _systemMonitorService;
        //頻率設定
        private readonly TimeSpan _checkInterval=TimeSpan.FromHours(1);

        public GoogleMonitoringBackgroundService(
            ILogger<GoogleMonitoringBackgroundService> logger,
            IServiceScopeFactory serviceScopeFactory,
            SystemMonitorService systemMonitorService)
        {
            _logger = logger;
            _serviceScopeFactory= serviceScopeFactory;
            _systemMonitorService= systemMonitorService;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //初始化log
            _systemMonitorService.AddLog(0, "Google監控背景服務已啟動", "待機中");
            //
            _systemMonitorService.DeleteOldLogFiles();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //開始執行任務，狀態改為「處理中」
                    _systemMonitorService.AddLog(0, "開始執行任務：正同步Google Cloud指標並更新資料庫...", "處理中");

                    using (var scope=_serviceScopeFactory.CreateAsyncScope())
                    {
                        var googleService=scope.ServiceProvider.GetRequiredService<GoogleMonitoringService>();

                        //本次抓取結果
                        var fetchResult = await googleService.FetchAndSaveAsync(1);


                        var dbContext = scope.ServiceProvider.GetRequiredService<FoodSlotContext>();

                        var cutoffTime = DateTime.Now.AddHours(-24);

                        //資料庫24小時累積總量
                       var apiSummary = dbContext.APIRequestLog
                            .Where(x => x.updatedAt >= cutoffTime)
                            .GroupBy(x=>x.apiName)
                            .Select(x => new
                            {
                                apiName=x.Key,
                                total=x.Sum(x=>x.totalRequests)
                            })
                            .ToList();
                        //警戒值
                        int warningThreshold = 100;



                        foreach (var item in fetchResult)
                        {
                            _systemMonitorService.AddLog(
                                item.totalRequests, $"[{item.apiName}]本小時請求數：{item.totalRequests}次。", "處理中");
                        }

                       foreach(var api in apiSummary)
                        {
                            bool isWarning=api.total>=warningThreshold;
                            string status = isWarning ? "警戒" : "待機中";

                            _systemMonitorService.AddLog(
                                api.total,
                                $"[24小時統計]{api.apiName}:{api.total}次"+(isWarning?$"已達警戒值{warningThreshold}次!":""),
                                status);
                        }


                    }



                }
                catch (Exception ex)
                {
                    _systemMonitorService.AddLog(0, $"定時監控發生異常:{ex.Message}", "異常");
                    _logger.LogError(ex, "Google監控背景服務再執行週期中發生錯誤。");

                }

                await Task.Delay(_checkInterval,stoppingToken);
            }
            _systemMonitorService.AddLog(0, "Google監控背景服務已停止", "已停止");

        }



    }
}
