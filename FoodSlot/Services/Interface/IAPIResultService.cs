using FoodSlot.Areas.API.DTOs;

namespace FoodSlot.Services.Interfaces
{
    public interface IAPIResultService
    {
        Task<List<StoreDTO>> NearbySearchAsync(
            NearbySearchRequestDTO request);
    }
}