using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsersByCreateId(int nutritionistId)
        {
            var users = await _nutritionRouteRepositories.GetAllUsersByCreateIdAsync(nutritionistId);
            return Ok(users);
        }



        // GET: api/nutritionroute/{nutritionistId}/user/{userId}
        [HttpGet("{nutritionistId}/user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserListManagementDTO>>> GetNutritionRoutesByCreateByIdAndUserId(int nutritionistId, int userId)
        {
            var routes = await _nutritionRouteRepositories.GetNutritionRoutesByNutritionistIdAndUserIdAsync(nutritionistId, userId);
            return Ok(routes);
        }

        // GET: api/nutritionroute/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserListManagementDTO>> GetNutritionRouteById(int id)
        {
            var route = await _nutritionRouteRepositories.GetNutritionRouteByIdAsync(id);
            if (route == null)
            {
                return NotFound("Không tìm thấy lộ trình dinh dưỡng.");
            }
            return Ok(route);
        }

        // POST: api/nutritionroute
        [HttpPost]
        public async Task<ActionResult> CreateNutritionRoute([FromBody] UserListManagementDTO userListManagementDto)
        {
            if (userListManagementDto == null || !ModelState.IsValid)
            {
                return BadRequest("Dữ liệu lộ trình dinh dưỡng không hợp lệ.");
            }

            var isCreated = await _nutritionRouteRepositories.CreateNutritionRouteAsync(userListManagementDto);
            if (!isCreated)
            {
                return NotFound("Không tìm thấy người sử dụng với số điện thoại đã cung cấp.");
            }

            return Ok("Tạo lộ trình dinh dưỡng thành công.");
        }

        // PUT: api/nutritionroute/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNutritionRoute(int id, [FromBody] UserListManagementDTO userListManagementDto)
        {
            if (id != userListManagementDto.Id)
            {
                return BadRequest("ID lộ trình dinh dưỡng không khớp.");
            }

            try
            {
                // Lấy NutritionRoute hiện tại từ database để cập nhật thông tin UserName và các trường khác
                var existingRoute = await _nutritionRouteRepositories.GetNutritionRouteByIdAsync(id);

                if (existingRoute == null)
                {
                    return NotFound("Không tìm thấy lộ trình dinh dưỡng.");
                }

                // Chỉ cập nhật các trường cần thiết, giữ nguyên UserName
                userListManagementDto.UserName = existingRoute.UserName;

                await _nutritionRouteRepositories.UpdateNutritionRouteAsync(userListManagementDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Không tìm thấy lộ trình dinh dưỡng.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật lộ trình dinh dưỡng: {ex.Message}");
            }

            return Ok();
        }


        // DELETE: api/NutritionRoute/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNutritionRoute(int id)
        {
            await _nutritionRouteRepositories.DeleteNutritionRouteAsync(id);
            return NoContent();
        }

        /*[HttpGet("{nutritionistId}/user/{userId}/unfinished")]
        public async Task<IActionResult> HasUnfinishedRoute(int nutritionistId, int userId)
        {
            var routes = await _nutritionRouteRepositories.GetUsersWithUnfinishedRoutesAsync(nutritionistId, userId);
            return Ok(routes); 
        }*/

        [HttpGet("{nutritionistId}/user/{userId}/unfinished")]
        public async Task<IActionResult> HasUnfinishedRoute(int nutritionistId, int userId)
        {
            var routes = await _nutritionRouteRepositories.HasUnfinishedRouteAsync(nutritionistId, userId);
            return Ok(routes);
        }

    }
}
