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
    public class IngredientController : ControllerBase
    {
        private IFoodRepositories repositories = new FoodRepositories();
        private readonly IMapper _mapper;

        public IngredientController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("GetAllIngredients")]
        public async Task<ActionResult<List<IngredientDetails100g>>> GetAllIngredients()
        {
            List<IngredientDetails100g> list = repositories.GetIngredientDetails();

            return list;
        }
    }
}
