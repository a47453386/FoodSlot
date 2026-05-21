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
            while(!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //開始執行任務，狀態改為「處理中」
                    _systemMonitorService.AddLog(0, "開始執行任務：正同步Google Cloud指標並更新資料庫...", "處理中");

                    int totalRequestInDB = 0;

                    using (var scope=_serviceScopeFactory.CreateAsyncScope())
                    {
                        //取得GoogleMonitoringService
                        var googleService = scope.ServiceProvider.GetRequiredService<GoogleMonitoringService>();
                        try
                        {
                            await googleService.FetchAndSaveAsync(1);
                        }catch(Exception ex) 
                        {
                            _logger.LogWarning(ex, "定時從Google抓取資料失敗");
                        }

                        //取得資料庫資料
                        var dbContect = scope.ServiceProvider.GetRequiredService<FoodSlotContext>();

                        //計算「最近24小時」時間中斷點
                        var cutoffTime = DateTime.Now.AddHours(-24);
                        totalRequestInDB = dbContect.APIRequestLog
                            .Where(x => x.updatedAt >= cutoffTime)
                            .Sum(x => x.totalRequests);
                            


                    }

                    _systemMonitorService.AddLog(
                            totalRequestInDB,
                            $"定時監控與入庫已完成。當前資料庫統計(最近24小時累積總請求量)為:{totalRequestInDB}次。",
                            "待機中");


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
