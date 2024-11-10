using System.Text.RegularExpressions;
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

        [HttpGet("GetPreparationIngredientsByFoodId/{foodId}")]
        public async Task<ActionResult<dynamic>> GetIngredientsByFoodId(int foodId)
        {
            List<ScaleAmount> scaleAmounts = repositories.GetIngredientByFoodId(foodId);

            return scaleAmounts.Select(s => new
            {
                Id = s.IngredientDetailsId,
                Name = s.IngredientDetails.Name,
                Urlimage = s.IngredientDetails.Urlimage,
                Amount = CalculateQuantity(
                    repositories.GetTypeOfCalculation(s.IngredientDetails.TypeOfCalculationId).CalculationForm, 
                    s.ScaleAmount1 ?? 0)
            }).ToList();

        }

        public static string CalculateQuantity(string input, double x)
        {
            var parts = input.Split('#');
            var typeOfCalculation = parts[1].Split(':')[1];

            var matches = Regex.Matches(typeOfCalculation, @"([\D]+?)-(\d+)");
            var calculations = new List<(string Unit, double Amount)>();

            string result = "";

            foreach (Match match in matches)
            {
                string unit = match.Groups[1].Value.Trim();
                double amount = double.Parse(match.Groups[2].Value);
                calculations.Add((unit, amount));
            }

            var distinctUnits = calculations.Select(c => c.Unit).Distinct().Count();

            if (distinctUnits == 1)
            {
                double minAmount = calculations.Min(c => c.Amount);
                var unit = calculations.First().Unit;
                double quantity = x / minAmount;
                result = $"{Math.Round(quantity, 1)} {unit}";
            }
            else
            {
                var divisibleQuantities = calculations.Where(c => x % c.Amount == 0).OrderBy(c => c.Amount).ToList();

                if (divisibleQuantities.Any())
                {
                    var bestCalculation = divisibleQuantities.First();
                    double quantity = x / bestCalculation.Amount;
                    result = $"{Math.Round(quantity, 1)} {bestCalculation.Unit}";
                }
                else
                {
                    var largerThanOne = calculations.Where(c => x / c.Amount > 1).OrderBy(c => x / c.Amount).ToList();

                    if (largerThanOne.Any())
                    {
                        var bestCalculation = largerThanOne.First();
                        double quantity = x / bestCalculation.Amount;
                        result = $"{Math.Round(quantity, 1)} {bestCalculation.Unit}";
                    }
                    else
                    {
                        var minCalculation = calculations.OrderBy(c => c.Amount).First();
                        double quantity = x / minCalculation.Amount;
                        result = $"{Math.Round(quantity, 1)} {minCalculation.Unit}";
                    }
                }
            }

            return result.Length > 0? result.Replace(";", "") : "Không thể tính toán";
        }

        [HttpPost("AddIngredient")]
        public async Task<ActionResult> AddIngredient([FromBody] AddIngredientRequest request)
        {
            IngredientDetails100g ingredient = _mapper.Map<IngredientDetails100g>(request);

            repositories.AddIngredient(ingredient);

            return Ok("Add ingredient successfully!");
        }

        [HttpPut("UpdateIngredient")]
        public async Task<ActionResult> UpdateIngredient([FromBody] UpdateIngredientRequest request)
        {
            if (!repositories.IsIngredientExisted(request.Id))
            {
                return NotFound("Ingredient not found!");
            }
            else
            {
                IngredientDetails100g ingredient = _mapper.Map<IngredientDetails100g>(request);
                repositories.UpdateIngredient(ingredient);
                return Ok("Update ingredient successfully!");
            }
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
