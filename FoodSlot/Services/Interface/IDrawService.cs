using FoodSlot.ViewModels.Slot;

namespace FoodSlot.Interfaces
{
    public interface IDrawService
    {
        Task<VMSlotDrawResult> DrawAsync(int? userID);
        Task<List<VMFoodSlotItem>> GetFoodsPoolAsync(int? userID);
    }
}