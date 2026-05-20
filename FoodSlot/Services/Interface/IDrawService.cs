using FoodSlot.ViewModels.Slot;

namespace FoodSlot.Interfaces
{
    public interface IDrawService
    {
        Task<VMSlotDrawResult> DrawAsync(int? userID);
    }
}