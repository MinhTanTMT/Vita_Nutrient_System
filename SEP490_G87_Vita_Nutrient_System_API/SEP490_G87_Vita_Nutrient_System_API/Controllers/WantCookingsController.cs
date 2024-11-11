using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WantCookingsController : ControllerBase
    {
        private IWantCookingRepositories repositories = new WantCookingRepositories();


        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        [HttpGet("GetAllWantCookings")]
        public async Task<IActionResult> GetAllWantCookings()
        {
            var wantCookings = await repositories.GetAllWantCookingAsync();
            return Ok(wantCookings);
        }
    }
}
