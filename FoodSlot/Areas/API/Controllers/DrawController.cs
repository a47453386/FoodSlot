using FoodSlot.Interfaces;
using FoodSlot.ViewModels.Slot;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodSlot.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrawController : ControllerBase
    {
        private readonly IDrawService _drawService;
        public DrawController(IDrawService drawService)
        {
            _drawService = drawService;
        }

        [HttpPost]
        public async Task<IActionResult> Start()
        {
            //預設
            int? userID = null;

            //判斷是否登入，有登入時，取得登入者的userID字串
            if (User.Identity?.IsAuthenticated == true)
            {
                string? userIDString =
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //userIDString不是空值時，將userID字串轉成int
                if (!string.IsNullOrWhiteSpace(userIDString))
                {
                    userID = int.Parse(userIDString);
                }
            }

            //將DrawAsync()執行結果存到result
            VMSlotDrawResult result =
                await _drawService.DrawAsync(userID);

            return Ok(result);
        }
    }
}