using FoodSlot.DTOs;

using FoodSlot.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FoodSlot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleMapsAPIController : ControllerBase
    {
        private readonly IAPIResultService _apiResultService;

        public GoogleMapsAPIController(
            IAPIResultService apiResultService)
        {
            _apiResultService = apiResultService;
        }

        /// Nearby Search 測試
        [HttpPost("NearbySearch")]
        public async Task<IActionResult> NearbySearch(
            [FromBody] NearbySearchRequestDTO request)
        {
            try
            {
                var result =
                    await _apiResultService
                        .NearbySearchAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }
    }
}