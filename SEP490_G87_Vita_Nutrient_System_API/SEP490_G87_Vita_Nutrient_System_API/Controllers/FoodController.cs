﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels;
using SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
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
            if(food is null)
            {
                return BadRequest("Food not found!");
            }
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
            List<DietType> types = foodRepositories.GetDietTypes();
            return Ok(types);
        }


        [HttpPost("SaveFoodRecipes")]
        public async Task<string> SaveFoodRecipes([FromBody] SaveFoodRecipeDTO model)
        {
            return await foodRepositories.SaveFoodRecipe(model);
        }

        [HttpGet("GetFoodIngredient/{foodId}")]
        public async Task<ActionResult> GetFoodIngredient(int foodId)
        {
            if (foodRepositories.IsFoodExisted(foodId))
            {
                List<ScaleAmount> scaleAmounts = foodRepositories.GetIngredientByFoodId(foodId);

                var result = scaleAmounts.Select(s => new
                {
                    Id = s.IngredientDetailsId,
                    Name = s.IngredientDetails.Name,
                    Urlimage = s.IngredientDetails.Urlimage,
                    Describe = s.IngredientDetails.Describe,
                    Amount = s.ScaleAmount1
                }).ToList();

                return Ok(result);
            }
            else
            {
                return BadRequest("Food not found!");
            }
        }

        [HttpPost("AddIngredientToFood")]
        public async Task<ActionResult> AddIngredientToFood([FromBody] AddIngredientToFoodRequest request)
        {
            foodRepositories.AddIngredientToFood(request.FoodId, request.IngredientId, request.Amount);

            return Ok("Add successful!");
        }

        [HttpDelete("DeleteIngredientFromFood/{foodId}/{ingredientId}")]
        public async Task<ActionResult> DeleteIngredientFromFood(int foodId, int ingredientId)
        {
            foodRepositories.RemoveIngredientFromFood(foodId, ingredientId);

            return Ok("Remove successful!");
        }
    }
}

