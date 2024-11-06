using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotOfTheDaysController : ControllerBase
    {
        private ISlotOfTheDayRepositories repositories = new SlotOfTheDayRepositories();


        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        [HttpGet("GetAllSlotOfTheDays")]
        public async Task<IActionResult> GetAllSlotOfTheDays()
        {
            var slotOfTheDays = await repositories.GetAllSlotOfTheDayAsync();
            return Ok(slotOfTheDays);  
        }
    }
}
