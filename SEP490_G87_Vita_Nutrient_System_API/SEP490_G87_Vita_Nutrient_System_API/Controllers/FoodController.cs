using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private IFoodRepositories repositories = new FoodRepositories();
        private readonly IMapper _mapper;

        public FoodController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("GetFoods")]
        public async Task<ActionResult<List<FoodResponse>>> GetFoods(int? foodTypeId)
        {
            List<FoodList> foods = foodTypeId.HasValue switch
            {
                true => repositories.GetFoodsByType(foodTypeId.Value),
                _ => repositories.GetFoods(),
            };

            List<FoodResponse> result = foods.Select(f => _mapper.Map<FoodResponse>(f)).ToList();

            return Ok(result);
        }

        [HttpGet("GetFoodById/{foodId}")]
        public async Task<ActionResult<FoodResponse>> GetFood(int foodId)
        {
            FoodList food = repositories.GetFood(foodId);
            return _mapper.Map<FoodResponse>(food);
        }

        [HttpGet("GetFoodRecipe/{foodId}")]
        public async Task<ActionResult<List<Recipe>>> GetFoodRecipe(int foodId)
        {
            return repositories.GetFoodRecipe(foodId);
        }

        [HttpGet("GetFoodTypes")]
        public async Task<ActionResult<List<FoodTypeResponse>>> GetFoodTypes()
        {
            List<FoodType> types = repositories.GetFoodTypes();
            return types.Select(t => _mapper.Map<FoodTypeResponse>(t)).ToList();
        }
    }
}
