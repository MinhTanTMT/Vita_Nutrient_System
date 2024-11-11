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
        private IFoodRepositories foodRepositories = new FoodRepositories();
        private ISlotOfTheDayRepositories slotOfTheDayRepositories = new SlotOfTheDayRepositories();
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
                true => foodRepositories.GetFoodsByType(foodTypeId.Value),
                _ => foodRepositories.GetFoods(),
            };

            List<FoodResponse> result = foods.Select(f => _mapper.Map<FoodResponse>(f)).ToList();

            return Ok(result);
        }

        [HttpGet("GetFoodById/{foodId}")]
        public async Task<ActionResult<dynamic>> GetFood(int foodId)
        {
            //get food
            FoodList food = foodRepositories.GetFood(foodId);

            //get slot of the day of food
            string keyList = foodRepositories.GetKeynote((int)food.KeyNoteId)?.KeyList;

            short[] slotsOfTheDay = keyList.Split('#')[2].Substring("SlotOfTheDay:".Length)
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(short.Parse)
                .ToArray();

            List<dynamic> slots = new();
            foreach (var item in slotsOfTheDay)
            {
                SlotOfTheDay s = await slotOfTheDayRepositories.GetSlotOfTheDayAsyncById(item);
                slots.Add(new
                {
                    id = s.Id,
                    slot = s.Slot,
                    startAt = s.StartAt,
                    endAt = s.EndAt
                });
            }

            return new
            {
                food = _mapper.Map<FoodResponse>(food),
                slots = slots
            };
        }

        [HttpGet("GetFoodRecipe/{foodId}")]
        public async Task<ActionResult<List<Recipe>>> GetFoodRecipe(int foodId)
        {
            return foodRepositories.GetFoodRecipe(foodId);
        }

        [HttpGet("GetFoodTypes")]
        public async Task<ActionResult<List<FoodTypeResponse>>> GetFoodTypes()
        {
            List<FoodType> types = foodRepositories.GetFoodTypes();
            return types.Select(t => _mapper.Map<FoodTypeResponse>(t)).ToList();
        }

        [HttpGet("GetBlockFoodOfUser/{userId}")]
        public async Task<ActionResult<List<int>>> GetBlockFoodOfUser(int userId)
        {
            List<int> result = new List<int>();
            List<FoodSelection> foodSelections = foodRepositories.GetFoodSelectionsByUserId(userId);

            foreach (var foodSelection in foodSelections)
            {
                if(foodSelection.IsBlock == true)
                {
                    result.Add(foodSelection.FoodListId);
                }
            }

            return result;
        }

        [HttpGet("GetDietType")]
        public async Task<IActionResult> GetDietTypes()
        {
            List<DietType> types = repositories.GetDietTypes();
            return Ok(types);
        }

    }
}

