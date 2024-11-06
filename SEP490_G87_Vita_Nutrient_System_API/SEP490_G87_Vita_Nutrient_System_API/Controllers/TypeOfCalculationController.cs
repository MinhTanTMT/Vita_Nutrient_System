using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfCalculationController : ControllerBase
    {
        private IFoodRepositories repositories = new FoodRepositories();
        private readonly IMapper _mapper;

        public TypeOfCalculationController(IMapper mapper) => _mapper = mapper;

        [HttpGet("GetTypesOfCalculation")]
        public async Task<ActionResult<List<TypeOfCalculationResponse>>> GetTypesOfCalculation()
        {
            return repositories.GetTypesOfCalculation().Select(t => _mapper.Map<TypeOfCalculationResponse>(t)).ToList();
        }

        [HttpGet("GetTypeOfCalculation/{id}")]
        public async Task<ActionResult<TypeOfCalculationResponse>> GetTypeOfCalculation(short id)
        {
            var typeOfCal = repositories.GetTypeOfCalculation(id);
            if(typeOfCal == null)
            {
                return NotFound("Type of calculation not found!");
            }
            else
            {
                return _mapper.Map<TypeOfCalculationResponse>(typeOfCal);
            }
        }
    }
}
