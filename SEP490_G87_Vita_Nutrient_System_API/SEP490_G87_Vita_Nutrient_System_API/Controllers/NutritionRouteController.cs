﻿using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionRouteController : ControllerBase
    {
        private readonly INutritionRouteRepositories _nutritionRouteRepositories = new NutritionRouteRepositories();


        [HttpGet("{nutritionistId}/users")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllPremiumUser(int nutritionistId)
        {
            var users = await _nutritionRouteRepositories.GetAllPremiumUserAsync(nutritionistId);
            return Ok(users);
        }



        // GET: api/nutritionroute/{nutritionistId}/user/{userId}
        [HttpGet("{nutritionistId}/user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserListManagementDTO>>> GetPremiumUserByNutritionistIdAndUserId(int nutritionistId, int userId)
        {
            var routes = await _nutritionRouteRepositories.GetPremiumUserByNutritionistIdAndUserIdAsync(nutritionistId, userId);
            return Ok(routes);
        }

        // GET: api/nutritionroute/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<NutritionRouteDTO>> GetNutritionRouteById(int id)
        {
            var route = await _nutritionRouteRepositories.GetNutritionRouteByIdAsync(id);
            if (route == null)
            {
                return NotFound("Không tìm thấy lộ trình dinh dưỡng.");
            }
            return Ok(route);
        }

        // POST: api/nutritionroute/{userListManagementId}
        [HttpPost("{userListManagementId}")]
        public async Task<ActionResult> CreateNutritionRoute(int userListManagementId, [FromBody] NutritionRouteDTO nutritionRouteDto)
        {
            if (nutritionRouteDto == null || !ModelState.IsValid)
            {
                return BadRequest("Dữ liệu lộ trình dinh dưỡng không hợp lệ.");
            }

            var isCreated = await _nutritionRouteRepositories.CreateNutritionRouteAsync(nutritionRouteDto, userListManagementId);
            if (!isCreated)
            {
                return NotFound("Không thể tạo lộ trình dinh dưỡng. Kiểm tra gói đăng ký hoặc thời gian.");
            }

            return Ok("Tạo lộ trình dinh dưỡng thành công.");
        }

        // PUT: api/nutritionroute/{id}/{userListManagementId}
        [HttpPut("{id}/{userListManagementId}")]
        public async Task<ActionResult> UpdateNutritionRoute(int id, int userListManagementId, [FromBody] NutritionRouteDTO nutritionRouteDto)
        {
            if (id != nutritionRouteDto.Id)
            {
                return BadRequest("ID lộ trình dinh dưỡng không khớp.");
            }

            try
            {
                var isUpdated = await _nutritionRouteRepositories.UpdateNutritionRouteAsync(nutritionRouteDto, userListManagementId);

                if (!isUpdated)
                {
                    return NotFound("Không thể cập nhật lộ trình. Kiểm tra gói đăng ký hoặc thời gian.");
                }
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Không tìm thấy lộ trình dinh dưỡng.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật lộ trình dinh dưỡng: {ex.Message}");
            }

            return Ok("Cập nhật lộ trình dinh dưỡng thành công.");
        }


        // DELETE: api/NutritionRoute/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNutritionRoute(int id)
        {
            await _nutritionRouteRepositories.DeleteNutritionRouteAsync(id);
            return NoContent();
        }

        [HttpGet("{nutritionistId}/user/{userId}/unfinished/{userListManagementId}")]
        public async Task<IActionResult> HasUnfinishedRoute(int nutritionistId, int userId, int userListManagementId)
        {
            var hasUnfinishedRoute = await _nutritionRouteRepositories.HasUnfinishedRouteAsync(nutritionistId, userId, userListManagementId);
            return Ok(hasUnfinishedRoute);
        }


        [HttpGet("{nutritionistId}/user/{userId}/route/{userListManagementId}")]
        public async Task<ActionResult<IEnumerable<NutritionRouteDTO>>> GetNutritionRoutes(int nutritionistId, int userId, int userListManagementId)
        {
            var routes = await _nutritionRouteRepositories.GetNutritionRoutesAsync(nutritionistId, userId, userListManagementId);
            if (routes == null || !routes.Any())
            {
                // Trả về danh sách rỗng thay vì chuỗi
                return Ok(new List<NutritionRouteDTO>());
            }
            return Ok(routes);
        }

        [HttpGet("user/{userId}/diseases")]
        public async Task<ActionResult<IEnumerable<ListOfDiseaseDTO>>> GetDiseaseByUserId(int userId)
        {
            var diseases = await _nutritionRouteRepositories.GetDiseaseByUserIdAsync(userId);
            if (diseases == null || !diseases.Any())
            {
                return Ok(new List<ListOfDiseaseDTO>());
            }
            return Ok(diseases);
        }

        [HttpPost("AddDiseasesOfUser")]
        public async Task<IActionResult> CreateDisease([FromBody] AddDiseaseRequest data)
        {
            var isCreated = await _nutritionRouteRepositories.CreateDiseaseAsync(data.UserId, data.DiseaseId);
            if (!isCreated)
            {
                return BadRequest("Không thể thêm bệnh nền. Bệnh đã tồn tại hoặc dữ liệu không hợp lệ.");
            }
            return Ok("Thêm bệnh nền thành công.");
        }

        [HttpDelete("user/{userId}/diseases/{diseaseId}")]
        public async Task<IActionResult> DeleteDisease(int userId, int diseaseId)
        {
            var isDeleted = await _nutritionRouteRepositories.DeleteDiseaseAsync(userId, diseaseId);
            if (!isDeleted)
            {
                return BadRequest("Không thể xóa bệnh nền. Bệnh không tồn tại hoặc dữ liệu không hợp lệ.");
            }
            return Ok("Xóa bệnh nền thành công.");
        }

        [HttpPut("updateIsDone")]
        public async Task<IActionResult> UpdateIsDone([FromQuery] int createById, [FromQuery] int userId)
        {
            if (createById <= 0 || userId <= 0)
            {
                return BadRequest("CreateById hoặc UserId không hợp lệ.");
            }

            try
            {
                var isUpdated = await _nutritionRouteRepositories.UpdateIsDoneAsync(createById, userId);
                if (!isUpdated)
                {
                    return NotFound("Không tìm thấy lộ trình cần cập nhật hoặc tất cả lộ trình đã hoàn thành.");
                }

                return Ok("Cập nhật trạng thái IsDone thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi hệ thống: {ex.Message}");
            }
        }

        [HttpPut("rate")]
        public async Task<IActionResult> RateNutritionist([FromQuery] int userId, [FromQuery] int nutritionistId, [FromQuery] int userListManagementId, [FromQuery] short rate)
        {
            if (rate < 1 || rate > 5)
            {
                return BadRequest("Điểm đánh giá không hợp lệ. Điểm phải từ 1 đến 5.");
            }

            var isRated = await _nutritionRouteRepositories.RateNutritionistAsync(userId, nutritionistId, userListManagementId, rate);
            if (!isRated)
            {
                return BadRequest("Không thể đánh giá. Gói đăng ký chưa hoàn thành hoặc đã hết hạn đánh giá.");
            }

            return Ok("Đánh giá thành công.");
        }


        [HttpGet("nutritionist/{nutritionistId}/ratings")]
        public async Task<ActionResult<IEnumerable<UserListManagementDTO>>> GetRatingsByNutritionistId(int nutritionistId)
        {
            var ratings = await _nutritionRouteRepositories.GetRatingsByNutritionistIdAsync(nutritionistId);
            if (ratings == null || !ratings.Any())
            {
                return Ok(new List<UserListManagementDTO>()); // Trả về danh sách rỗng nếu không có đánh giá
            }
            return Ok(ratings);
        }

        [HttpGet("user/{userId}/details-premium")]
        public async Task<ActionResult<IEnumerable<UserListManagementDTO>>> GetDetailsAllPremiumUserByUser(int userId)
        {
            var details = await _nutritionRouteRepositories.GetDetailsAllPremiumUserByUserAsync(userId);
            if (details == null || !details.Any())
            {
                return Ok(new List<UserListManagementDTO>()); // Trả về danh sách rỗng nếu không có gói nào
            }
            return Ok(details);
        }


















    }


    public class AddDiseaseRequest
    {
        public int UserId { get; set; }
        public int DiseaseId { get; set; }
    }
}
