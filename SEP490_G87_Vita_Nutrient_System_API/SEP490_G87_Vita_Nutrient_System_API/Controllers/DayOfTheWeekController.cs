using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayOfTheWeekController : ControllerBase
    {
        private IDayOfTheWeekRepositories repositories = new DayOfTheWeekRepositories();


        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        [HttpGet("GetAllDayOfTheWeek")]
        public async Task<IActionResult> GetAllDayOfTheWeek()
        {
            var dayOfTheWeek = await repositories.GetAllDayOfTheWeekAsync();
            return Ok(dayOfTheWeek);
        }
    }
}
