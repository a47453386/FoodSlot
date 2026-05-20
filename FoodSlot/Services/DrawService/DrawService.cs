using FoodSlot.Interfaces;
using FoodSlot.Models;
using FoodSlot.ViewModels.Slot;
using Microsoft.EntityFrameworkCore;

namespace FoodSlot.Services.SlotService
{
    public class DrawService : IDrawService
    {
        private readonly FoodSlotContext _context;

        public DrawService(FoodSlotContext context)
        {
            _context = context;
        }

        public async Task<VMSlotDrawResult> DrawAsync(int? userID)
        {
            int actualUserID = userID ?? 1;//有登入就用userID，沒有就用1(管理者)
            //取得會員設定的主食ID
            List<int> foodIDs =
                await _context.UserFoodSettings
                    .Where(x => x.userID == actualUserID)
                    .Select(x => x.foodID)
                    .ToListAsync();
            //防呆，判斷是否為子類別，並取得主食的其他資料
            List<Food> foods =
                await _context.Foods
                    .Where(x =>
                        foodIDs.Contains(x.foodID) &&
                        x.parentfoodID != null)
                    .ToListAsync();

            if (!foods.Any())
            {
                throw new Exception("無可抽取食物資料");
            }
            //隨機抽出一個 Food，foods.Count=foods[]有幾筆資料
            //Random.Shared.Next()隨機產生數字
            //範例：foods.Count=4，Random.Shared.Next(4)可能結果為0123
            Food selectedFood =
                foods[Random.Shared.Next(foods.Count)];
            //把 Food Model 轉成 VM
            VMFoodSlotItem selectedVM =
                ConvertToVM(selectedFood);

            //建立拉霸輪盤資料
            //建立空的List
            List<VMFoodSlotItem> reelItems = [];

            for (int i = 0; i < 29; i++)
            {
                //每次迴圈隨機取得一個使用者設定的主食
                Food randomFood =
                    foods[Random.Shared.Next(foods.Count)];

                //加入到 reelItems 清單尾端
                reelItems.Add(ConvertToVM(randomFood));
            }

            // 最後一格固定中獎項目
            reelItems.Add(selectedVM);

            //把每一筆 Food 轉成 VMFoodSlotItem
            List <VMFoodSlotItem> foodsPool =
                foods.Select(ConvertToVM).ToList();

            //建立最後回傳的物件
            return new VMSlotDrawResult
            {
                selectedFood = selectedVM,//中獎項目
                reelItems = reelItems,//拉霸動畫的資料列表
                foodsPool = foodsPool//獎池列表
            };
        }

        //把 Food Model 轉成 VM
        private static VMFoodSlotItem ConvertToVM(Food food)
        {
            return new VMFoodSlotItem
            {
                foodID = food.foodID,
                foodname = food.foodname,
                photoUrl = $"/FoodImages/{food.photo}.webp"
            };
        }
    }
}