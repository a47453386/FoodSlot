namespace FoodSlot.Services.GoogleMonitoringService
{
    public class GoogleMonitoringBackgroundService:BackgroundService
    {
        private readonly ILogger<GoogleMonitoringBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly GoogleMonitoringService _googleMonitoringService;
        private readonly SystemMonitorService _systemMonitorService;
        //頻率設定
        private readonly TimeSpan _checkInterval=TimeSpan.FromHours(1);

        public GoogleMonitoringBackgroundService(
            ILogger<GoogleMonitoringBackgroundService> logger,
            IServiceScopeFactory serviceScopeFactory,
            GoogleMonitoringService googleMonitoringService,
            SystemMonitorService systemMonitorService)
        {
            _logger = logger;
            _serviceScopeFactory= serviceScopeFactory;
            _googleMonitoringService= googleMonitoringService;
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
                    _systemMonitorService.AddLog(0, "正從 Google Cloud 抓取 API 請求量...", "處理中");


                    



                }
                catch (Exception ex)
                {

                }
            }
        }



    }
}
