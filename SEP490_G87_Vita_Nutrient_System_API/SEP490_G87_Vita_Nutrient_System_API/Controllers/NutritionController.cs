using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
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
            // Retrieve existing user details
            var existingUser = await _nutritionRepo.GetUserDetailsAsync(userDetails.UserId);
            if (existingUser == null)
                return NotFound("User not found");

            // Save user personal information
            var isSaved = await _nutritionRepo.SaveOrUpdateUserDetailsAsync(userDetails);
            if (!isSaved)
                return BadRequest("Invalid data provided");

            // Calculate nutritional needs
            var nutritionTargets = _nutritionRepo.CalculateNutritionNeeds(userDetails);

            // Save nutrition targets to database
            await _nutritionRepo.SaveNutritionTargetsAsync(nutritionTargets.Result);

            // Return the calculated nutrition details
            return Ok(nutritionTargets);
        }
    }

}
