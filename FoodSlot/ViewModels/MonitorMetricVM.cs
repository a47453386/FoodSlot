namespace FoodSlot.ViewModels
{
    public class MonitorMetricVM
    {
        public DateTime Time { get; set; }     // 對應 X 軸：時間
        public double Value { get; set; }       // 對應 Y 軸：數值 (例如請求數、抽籤次數)
        public string? Status { get; set; }     // 狀態 (例如：成功、失敗)
        public string? Message { get; set; }    // 詳細訊息
    }
}
