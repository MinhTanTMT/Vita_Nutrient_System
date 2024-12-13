﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    //abc
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private IMealsRepositories repositories = new MealsRepositories();


        [HttpPost("CreateMealSettingsDetail")]
        public async Task<IActionResult> CreateMealSettingsDetail([FromBody] CreateMealSettingsDetailDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mealSettingsDetail = new MealSettingsDetail
            {
                MealSettingsId = dto.MealSettingsId,
                SlotOfTheDayId = dto.SlotOfTheDayId,
                NutritionTargetsDailyId = dto.NutritionTargetsDailyId,
                DayOfTheWeekId = dto.DayOfTheWeekId,
                SkipCreationProcess = dto.SkipCreationProcess,
                Size = dto.Size,
                NutritionFocus = dto.NutritionFocus,
                NumberOfDishes = dto.NumberOfDishes,
                TypeFavoriteFood = dto.TypeFavoriteFood,
                WantCookingId = dto.WantCookingId,
                TimeAvailable = dto.TimeAvailable,
                CookingDifficultyId = dto.CookingDifficultyId,
                Name = dto.Name,
                IsActive = dto.IsActive,
                OrderNumber = dto.OrderNumber,

            };
            try
            {
                await repositories.AddMealSettingsDetailAsync(mealSettingsDetail);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
            return CreatedAtAction(nameof(CreateMealSettingsDetail), mealSettingsDetail);
        }

        [HttpPut("AddMealToList/{mealId}")]
        public async Task<IActionResult> AddMealToListAsync(int mealId)
        {
            try
            {
                // Gọi repository để thêm meal vào danh sách
                var mealSettingsDetail = await repositories.AddMealToListAsync(mealId);

                if (mealSettingsDetail == null)
                {
                    return NotFound(new { message = "MealSettingsDetail không tìm thấy." });
                }

                return Ok(new { message = "Bữa ăn đã được thêm vào danh sách và sắp xếp thứ tự thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Lỗi trong quá trình xử lý: {ex.Message}" });
            }
        }

        [HttpPut("EditMealSettingsDetail/{id}")]
        public async Task<IActionResult> EditMealSettingsDetailAsync(int id, [FromBody] MealSettingsDetailDTO model)
        {
            try
            {
                var updatedMeal = await repositories.EditMealSettingsDetailAsync(id, model);

                if (updatedMeal == null)
                {
                    return NotFound(new { message = "MealSettingsDetail không tìm thấy." });
                } 
                return Ok(new { message = "Cập nhật thành công", updatedMeal });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("UpdateCalo/{id}")]
        public async Task<IActionResult> UpdateCalo(int id)
        {
            try
            {
                await repositories.UpdateCalo(id);
                return Ok(new { message = "Cập nhật calo thành công" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Lỗi trong quá trình cập nhật calo: {ex.Message}" });
            }
        }


        [HttpPut("UpdateMealSetting/{id}")]
        public async Task<IActionResult> UpdateMealSetting(int id, [FromBody] MealSettingDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await repositories.UpdateMealSettingForMealAsync(id, dto);

            return Ok(new { message = "Cập nhật thành công" });
        }



        [HttpGet("GetAllMeal")]
        public async Task<IActionResult> GetAllMeal()
        {
            var mealList = await repositories.GetAllMealAsync();
            return Ok(mealList);
        }

        [HttpGet("GetAllMealSettingByUserId/{UserId}")]
        public async Task<IActionResult> GetAllMealSettingByUserId(int UserId)
        {
            var mealListByUserId = await repositories.GetAllMealSettingByUserIdAsync(UserId);
            if (mealListByUserId == null)
            {
                return NotFound(new { message = "Không tìm thấy MealSetting cho người dùng này." });
            }

            return Ok(mealListByUserId.ToList());
        }

        [HttpGet("GetAllMealSettingBySelected/{UserId}")]
        public async Task<IActionResult> GetAllMealSettingBySelected(int UserId)
        {
            var mealListBySelected = await repositories.GetAllMealSettingBySelectedAsync(UserId);
            if (mealListBySelected == null)
            {
                return NotFound(new { message = "Không tìm thấy MealSetting cho người dùng này." });
            }

            return Ok(mealListBySelected.ToList());
        }
        [HttpGet("FindMealSettingsDetailByNutritionTargetsDailyId/{nutritionTargetsDailyId}")]
        public async Task<IActionResult> FindMealSettingsDetailByNutritionTargetsDailyId(int nutritionTargetsDailyId)
        {
            var mealSettingsDetail = await repositories.FindMealSettingsDetailByNutritionTargetsDailyIdAsync(nutritionTargetsDailyId);

            if (mealSettingsDetail == null)
            {
                return NotFound(new { message = "MealSettingsDetail không tìm thấy." });
            }

            return Ok(mealSettingsDetail);
        }

        [HttpGet("FindMealSettingsDetailById/{id}")]
        public async Task<IActionResult> FindMealSettingsDetailById(int id)
        {
            var mealById = await repositories.FindMealSettingsDetailByIdAsync(id);
            return Ok(mealById);
        }
        [HttpGet("GetMealSettingDetailByMealSettingId/{id}")]
        public async Task<IActionResult> GetMealSettingDetailByMealSettingId(int id)
        {
            var mealById = await repositories.GetMealSettingDetailByMealSettingIdAsync(id);
            return Ok(mealById);
        }
        
        [HttpGet("GetMealSettingByUserId/{id}")]
        public async Task<IActionResult> GetMealSettingByUserId(int id)
        {
            var mealSettingById = await repositories.GetMealSettingByUserIdAsync(id);
            return Ok(mealSettingById);
        }

        [HttpDelete("DeleteMealSettingsDetail/{id}")]
        public async Task<IActionResult> DeleteMealSettingsDetail(int id)
        {
            var mealSettingsDetail = await repositories.FindMealSettingsDetailByIdAsync(id);
            if (mealSettingsDetail == null)
            {
                return NotFound(new { message = "MealSettingsDetail không tìm thấy." });
            }

            await repositories.DeleteMealSettingsDetailAsync(id);

            return Ok(new { id = id, message = "Xóa thành công." });
        }

        [HttpPut("RemoveMealToList/{mealId}")]
        public async Task<IActionResult> RemoveMealToListAsync(int mealId)
        {
            try
            {
                var result = await repositories.RemoveMealToListAsync(mealId);

                if (result == null)
                {
                    return NotFound(new { message = "MealSettingsDetail không tìm thấy." });
                }

                return Ok(new { message = "Bữa ăn đã được xóa khỏi danh sách và sắp xếp lại thứ tự thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Lỗi trong quá trình xử lý: {ex.Message}" });
            }
        }



        [HttpPut("ChangeOrderNumber/{mealId}")]
        public async Task<IActionResult> ChangeOrderNumberAsync(int mealId, string direction)
        {
            try
            {
                var result = await repositories.ChangeOrderNumberAsync(mealId, direction);

                if (!result)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy bữa ăn hoặc lỗi khi thay đổi thứ tự." });
                }

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = $"Lỗi trong quá trình xử lý: {ex.Message}" });
            }
        }

        [HttpPost("SaveUserAndCreateMeals")]
        public async Task<IActionResult> SaveUserAndCreateMeals([FromBody] MealAndUserPhysicalStatisticsDTO userStats)
        {
            try
            {
                if (userStats == null)
                {
                    return BadRequest(new { message = "Thông tin người dùng không hợp lệ." });
                }

                await repositories.SaveUserAndCreateMealsAsync(userStats);
                return Ok(new { message = "Lưu thông tin người dùng và tạo bữa ăn thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Lỗi khi xử lý: {ex.Message}" });
            }
        }

    }
}

