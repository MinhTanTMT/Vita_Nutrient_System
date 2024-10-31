using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionTargetsDailyController : ControllerBase
    {
        private INutritionTargetsDailyRepositories repositories = new NutritionTargetsDailyRepositories();


        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        [HttpGet("GetAllNutritionTargetsDailies")]
        public async Task<IActionResult> GetAllNutritionTargetsDailies()
        {
            var nutritionTargetsDailies = await repositories.GetAllNutritionTargetsDailyAsync();
            return Ok(nutritionTargetsDailies);
        }
    }
}
