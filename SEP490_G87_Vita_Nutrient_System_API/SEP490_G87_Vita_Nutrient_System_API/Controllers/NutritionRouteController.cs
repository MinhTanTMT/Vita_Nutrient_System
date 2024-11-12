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

        // GET: api/NutritionRoute
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NutritionRouteDTO>>> GetAllNutritionRoutes()
        {
            var routes = await _nutritionRouteRepositories.GetAllNutritionRoutesAsync();
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
        public async Task<ActionResult> CreateNutritionRoute([FromBody] NutritionRouteDTO nutritionRouteDto)
        {
            if (nutritionRouteDto == null || !ModelState.IsValid)
            {
                return BadRequest("Dữ liệu lộ trình dinh dưỡng không hợp lệ.");
            }

            if (nutritionRouteDto.UserId == 0)
            {
                return BadRequest("UserId không hợp lệ.");
            }

            await _nutritionRouteRepositories.CreateNutritionRouteAsync(nutritionRouteDto);
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
