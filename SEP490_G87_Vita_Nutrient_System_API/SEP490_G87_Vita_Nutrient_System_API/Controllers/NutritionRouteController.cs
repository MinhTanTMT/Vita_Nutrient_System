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
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllPremiumUser(int nutritionistId)
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
                return Ok("Không tìm thấy lộ trình nào cho gói đăng ký này.");
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

    }

    public class AddDiseaseRequest
    {
        public int UserId { get; set; }
        public int DiseaseId { get; set; }
    }
}
