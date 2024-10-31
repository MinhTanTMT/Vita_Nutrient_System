using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutritionController : ControllerBase
    {
        private readonly NutritionRepositories _nutritionRepo = new NutritionRepositories();

        [HttpPost("CalculateNutritionNeeds")]
        public async Task<IActionResult> CalculateNutritionNeeds([FromBody] UserDetailsDTO userDetails)
        {
            var existingUser = await _nutritionRepo.GetUserDetailsAsync(userDetails.UserId);
            if (existingUser == null)
                return NotFound("User not found");

            var isSaved = await _nutritionRepo.SaveOrUpdateUserDetailsAsync(userDetails);
            if (!isSaved)
                return BadRequest("Invalid data provided");

            var nutritionTargets = _nutritionRepo.CalculateNutritionNeeds(userDetails);

            await _nutritionRepo.SaveNutritionTargetsAsync(nutritionTargets.Result);

            return Ok(nutritionTargets);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers(string? search, int page = 1, int pageSize = 10)
        {
            var paginatedUsers = await _nutritionRepo.GetUsers(search, page, pageSize);

            return Ok(paginatedUsers);
        }

        [HttpPut("user/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] User updateUser)
        {
            await _nutritionRepo.UpdateUser(userId, updateUser);
            return NoContent();
        }

        [HttpGet("food-disease/{diseaseId}/{foodId}")]
        public async Task<IActionResult> CheckFoodDiseaseRelation(int diseaseId, int foodId)
        {
            var isGoodOrBad = await _nutritionRepo.CheckFoodDiseaseRelation(diseaseId, foodId);

            return Ok(isGoodOrBad);
        }
    }

}
