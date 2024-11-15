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

        // GET: api/nutritionroute/user/{createById}
        [HttpGet("user/{createById}")]
        public async Task<ActionResult<IEnumerable<NutritionRouteDTO>>> GetAllNutritionRoutesByCreateById(int createById)
        {
            var routes = await _nutritionRouteRepositories.GetAllNutritionRoutesByCreateByIdAsync(createById);
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

        // POST: api/nutritionroute
        [HttpPost]
        public async Task<ActionResult> CreateNutritionRoute([FromBody] NutritionRouteDTO nutritionRouteDto, [FromQuery] string userPhoneNumber)
        {
            if (nutritionRouteDto == null || !ModelState.IsValid)
            {
                return BadRequest("Dữ liệu lộ trình dinh dưỡng không hợp lệ.");
            }

            if (string.IsNullOrEmpty(userPhoneNumber)) 
            {
                return BadRequest("Số điện thoại của người sử dụng không được để trống.");
            }

            var isCreated = await _nutritionRouteRepositories.CreateNutritionRouteAsync(nutritionRouteDto, userPhoneNumber);
            if (!isCreated)
            {
                return NotFound("Không tìm thấy người sử dụng với số điện thoại đã cung cấp.");
            }

            return Ok();
        }

        // PUT: api/nutritionroute/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNutritionRoute(int id, [FromBody] NutritionRouteDTO nutritionRouteDto)
        {
            if (id != nutritionRouteDto.Id)
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
                nutritionRouteDto.UserName = existingRoute.UserName;

                await _nutritionRouteRepositories.UpdateNutritionRouteAsync(nutritionRouteDto);
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
    }
}
