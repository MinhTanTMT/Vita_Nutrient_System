using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
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

        [HttpGet("GetNutritionTargetsDaily/{id}")]
        public async Task<IActionResult> GetNutritionTargetsDaily(int id)
        {
            var nutritionTargetDTO = await repositories.GetNutritionTargetByIdAsync(id);
            if (nutritionTargetDTO == null)
            {
                return NotFound();
            }
            return Ok(nutritionTargetDTO);
        }

        [HttpPut("UpdateNutritionTargetsDaily/{id}")]
        public async Task<IActionResult> UpdateNutritionTargetsDaily(int id, [FromBody] NutritionTargetsDailyDTO updatedNutritionTargetDTO)
        {
            if (id != updatedNutritionTargetDTO.Id)
            {
                return BadRequest("Nutrition target ID mismatch.");
            }

            var updatedTargetDTO = await repositories.UpdateNutritionTargetAsync(updatedNutritionTargetDTO);
            if (updatedTargetDTO == null) 
            {
                return NotFound();
            }

            return Ok(updatedTargetDTO);
        }

    }
}
