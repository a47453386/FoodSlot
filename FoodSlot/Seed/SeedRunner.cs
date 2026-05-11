using FoodSlot.Models;
using FoodSlot.Seed.Data;

namespace FoodSlot.Seed
{
    public class SeedRunner
    {
        private readonly FoodSlotContext _context;
        private readonly SeedSystemSetting _seedSystemSetting;
        private readonly SeedSystemSettingCategory _seedSystemSettingCategory;
        private readonly SeedFood _seedFood;
        private readonly SeedUser _seedUser;
        private readonly SeedUserRange _seedUserRange;
        private readonly SeedUserRangeSettings _seedUserRangeSettings;
        private readonly SeedUserFoodSettings _seedUserFoodSettings;

        public SeedRunner(
            FoodSlotContext context, 
            SeedSystemSetting seedSystemSetting,
            SeedSystemSettingCategory seedSystemSettingCategory,
            SeedFood seedFood,
            SeedUser seedUser,
            SeedUserRange seedUserRange,
            SeedUserRangeSettings seedUserRangeSettings,
            SeedUserFoodSettings seedUserFoodSettings)
        {
            _context = context;
            _seedSystemSetting = seedSystemSetting;
            _seedSystemSettingCategory = seedSystemSettingCategory;
            _seedFood = seedFood;
            _seedUser = seedUser;
            _seedUserRange = seedUserRange;
            _seedUserRangeSettings = seedUserRangeSettings;
            _seedUserFoodSettings = seedUserFoodSettings;
        }

        public void Run() 
        {
            _seedSystemSetting.Run();
            _seedSystemSettingCategory.Run();
            _seedFood.Run();
            _seedUser.Run();
            _seedUserRange.Run();
            _seedUserRangeSettings.Run();
            _seedUserFoodSettings.Run();

            Console.WriteLine("更新Seed資料 完成");

        }

    }
}
