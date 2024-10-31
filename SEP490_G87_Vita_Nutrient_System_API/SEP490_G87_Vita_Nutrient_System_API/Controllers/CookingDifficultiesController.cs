using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookingDifficultiesController : ControllerBase
    {
        private ICookingDifficultiesRepositories repositories = new CookingDifficultiesRepositories();


        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        [HttpGet("GetAllCookingDifficulties")]
        public async Task<IActionResult> GetAllCookingDifficulties()
        {
            var cookingDifficulties = await repositories.GetAllCookingDifficultiesAsync();
            return Ok(cookingDifficulties);  
        }
    }
}
