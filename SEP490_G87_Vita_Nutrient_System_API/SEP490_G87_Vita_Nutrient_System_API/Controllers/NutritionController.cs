using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Dtos.Disease;
using SEP490_G87_Vita_Nutrient_System_API.Dtos.Food;
using SEP490_G87_Vita_Nutrient_System_API.Dtos.FoodAndDisease;
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

        [HttpGet("user")]
        public async Task<IActionResult> GetUsers(int userId, string? search, int page = 1, int pageSize = 10)
        {
            var paginatedUsers = await _nutritionRepo.GetUsers(userId, search, page, pageSize);

            return Ok(paginatedUsers);
        }

        [HttpGet("user-detail/{userId}")]
        public async Task<IActionResult> GetUserDetail(int userId)
        {
            var paginatedUsers = await _nutritionRepo.GetUserDetail(userId);

            return Ok(paginatedUsers);
        }

        [HttpPut("user/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, string inforConfirmBad, string inforConfirmGood)
        {
            return Ok(await _nutritionRepo.UpdateUser(userId, inforConfirmBad, inforConfirmGood));
        }

        [HttpGet("food-disease/{diseaseId}/{foodId}")]
        public async Task<IActionResult> CheckFoodDiseaseRelation(int diseaseId, int foodId)
        {
            var isGoodOrBad = await _nutritionRepo.CheckFoodDiseaseRelation(diseaseId, foodId);

            return Ok(isGoodOrBad);
        }

        [HttpGet("get-food-list")]
        public async Task<ActionResult<List<GetFoodListDTO>>> GetFoodLists([FromQuery] string? search)
        {
            var result = await _nutritionRepo.GetFoodLists(search);
            return Ok(result);
        }

        [HttpGet("get-food/{id}")]
        public async Task<ActionResult<FoodList>> GetFoodList(int id)
        {
            var foodList = await _nutritionRepo.GetFoodList(id);
            if (foodList == null)
            {
                return NotFound();
            }
            return Ok(foodList);
        }

        // POST: api/FoodList
        [HttpPost("save-food")]
        public async Task<ActionResult<FoodList>> PostFoodList(SaveFoodDTO foodList)
        {
            await _nutritionRepo.SaveFoodList(foodList);

            return CreatedAtAction("GetFoodList", new { id = foodList.FoodListId }, foodList);
        }

        // DELETE: api/FoodList/{id}
        [HttpDelete("FoodList/{id}")]
        public async Task<IActionResult> DeleteFoodList(int id)
        {
            var result = await _nutritionRepo.DeleteFoodList(id);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("list-disease")]
        public async Task<ActionResult<IEnumerable<ListOfDisease>>> GetDiseases([FromQuery] string? search)
        {
            return await _nutritionRepo.GetDiseases(search);
        }

        // GET: api/ListOfDisease/{id}
        [HttpGet("ListOfDisease/{id}")]
        public async Task<ActionResult<ListOfDisease>> GetDisease(int id)
        {
            var result = await _nutritionRepo.GetDiseases(id);
            if (result == null)
            {
                return NotFound("Disease is not exist!");
            }
            return Ok(result);
        }

        // POST: api/ListOfDisease
        [HttpPost("save-disease")]
        public async Task<ActionResult<ListOfDisease>> SaveDisease(SaveDiseaseDTO disease)
        {
            var result = await _nutritionRepo.SaveDisease(disease);
            if(result == null)
            {
                return Conflict("Save failed!");
            }
            return CreatedAtAction("GetDisease", new { id = disease.Id }, disease);
        }

        [HttpPost("create-food-and-disease")]
        public async Task<IActionResult> CreateFoodAndDisease([FromBody] SaveFoodAndDiseaseDTO foodAndDisease)
        {
            var result = await _nutritionRepo.SaveFoodAndDiseases(foodAndDisease);
            if (result == null)
            {
                return Conflict("The combination of food and disease already exists.");
            }

            return CreatedAtAction(nameof(CreateFoodAndDisease), result);
        }

        [HttpDelete("delete-food-and-disease{foodId}/{diseaseId}")]
        public async Task<IActionResult> DeleteFoodAndDisease(int foodId, int diseaseId)
        {
            var result = await _nutritionRepo.DeleteFoodAndDisease(foodId, diseaseId);

            if (result == 0)
            {
                return NotFound("The specified food and disease combination does not exist.");
            }

            return Ok(result);
        }

        [HttpGet("get-food-and-disease/{foodId}/{diseaseId}")]
        public async Task<IActionResult> GetFoodAndDisease(int foodId, int diseaseId)
        {
            var result = await _nutritionRepo.GetFoodAndDiseases(foodId, diseaseId);

            if (result == null)
            {
                return NotFound("The specified food and disease combination does not exist.");
            }

            return Ok(result);
        }

        [HttpGet("get-all-food-and-disease")]
        public async Task<IActionResult> GetAllFoodAndDiseases()
        {
            var result = await _nutritionRepo.GetFoodAndDiseases();

            if (result == null || result.Count == 0)
            {
                return NotFound("No food and disease combinations found.");
            }

            return Ok(result);
        }

    }

}
