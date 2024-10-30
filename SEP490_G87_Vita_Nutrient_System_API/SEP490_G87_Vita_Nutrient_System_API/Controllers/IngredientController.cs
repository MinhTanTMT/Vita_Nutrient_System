using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels;
using SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private IFoodRepositories repositories = new FoodRepositories();
        private readonly IMapper _mapper;

        public IngredientController(IMapper mapper) => _mapper = mapper;

        [HttpGet("GetAllIngredients")]
        public async Task<ActionResult<List<IngredientResponse>>> GetAllIngredients()
        {
            List<IngredientDetails100g> list = repositories.GetIngredientDetails();

            List<IngredientResponse> result = list.Select(i => _mapper.Map<IngredientResponse>(i)).ToList();

            return result;
        }

        [HttpGet("GetIngredient/{id}")]
        public async Task<ActionResult<IngredientResponse>> GetIngredient(int id)
        {
            IngredientDetails100g ingredient = repositories.GetIngredientDetail(id);

            if (ingredient is null)
            {
                return NotFound("Ingredient not found!");
            }
            else
            {
                IngredientResponse result = _mapper.Map<IngredientResponse>(ingredient);
                return result;
            }
        }

        [HttpPost("AddIngredient")]
        public async Task<ActionResult> AddIngredient([FromBody] AddIngredientRequest request)
        {
            IngredientDetails100g ingredient = _mapper.Map<IngredientDetails100g>(request);

            repositories.AddIngredient(ingredient);

            return Ok("Add ingredient successfully!");
        }

        [HttpDelete("RemoveIngredient/{id}")]
        public async Task<ActionResult> RemoveIngredient(int id)
        {
            IngredientDetails100g ingredient = repositories.GetIngredientDetail(id);

            if (ingredient is null)
            {
                return NotFound("Ingredient not found!");
            }
            else
            {
                repositories.DeleteIngredientDetail(id);
                return Ok("Delete ingredient successfully!");
            }
        }
    }
}
