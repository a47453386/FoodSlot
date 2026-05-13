using FoodSlot.Services.Interfaces;
using FoodSlot.DTOs;

using Microsoft.AspNetCore.Mvc;

namespace FoodSlot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreSearchController : ControllerBase
    {
        private readonly IAPIResultService _apiResultService;

        public StoreSearchController(
            IAPIResultService apiResultService)
        {
            _apiResultService = apiResultService;
        }

        [HttpPost("Nearby")]
        public async Task<IActionResult> Nearby(
            [FromBody] NearbySearchRequestDTO request)
        {
            var result =
                await _apiResultService
                    .NearbySearchAsync(request);

            return Ok(result);
        }
    }
}