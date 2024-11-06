using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Mapper;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealSettingsController : ControllerBase
    {
        private readonly IMealsRepositories _mealRepository = new MealsRepositories();

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<MealSettingsDto>>> GetAllMealSettings(int userId)
        {
            var mealSettings = await _mealRepository.GetAllMealSettingsAsync(userId);
            return Ok(mealSettings);
        }

        [HttpGet("{mealSettingsId}")]
        public async Task<ActionResult<MealSettingsDto>> GetMealSettingById(int mealSettingsId)
        {
            try
            {
                var mealSetting = await _mealRepository.GetMealSettingByIdAsync(mealSettingsId);
                return Ok(mealSetting);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Meal setting not found.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<MealSettingsDto>> AddMealSetting([FromBody] MealSettingsCreateDto newMealSetting)
        {
            var createdMealSetting = await _mealRepository.AddMealSettingAsync(newMealSetting);
            return CreatedAtAction(nameof(GetMealSettingById), new { mealSettingsId = createdMealSetting.Id }, createdMealSetting);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MealSettingsDto>> UpdateMealSetting(int id, [FromBody] MealSettingsUpdateDto updatedMealSetting)
        {
            try
            {
                var updated = await _mealRepository.UpdateMealSettingAsync(id, updatedMealSetting);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Meal setting not found.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMealSetting(int id)
        {
            try
            {
                await _mealRepository.DeleteMealSettingAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Meal setting not found.");
            }
        }
    }
}
