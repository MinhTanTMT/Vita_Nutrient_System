using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Mapper;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateMealController : ControllerBase
    {

        Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();


        private MapperConfiguration config;
        private IMapper mapper;
        public GenerateMealController()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            mapper = config.CreateMapper();
        }



        [HttpGet("APGenerateMealController")]
        public async Task<IActionResult> APIGenerateMealController()
        {

            //List<FoodList> foodLists;

            var result = _context.FoodLists
            .GroupJoin(
                _context.ScaleAmounts,
                fl => fl.FoodListId,
                sa => sa.FoodListId,
                (fl, saList) => new
                {
                    FoodListId = fl.FoodListId,
                    FoodListName = fl.Name,
                    ScaleAmounts = saList.Select(sa => new
                    {
                        sa.FoodListId,
                        sa.IngredientDetailsId,
                        sa.ScaleAmount1,
                        IngredientDetails = sa.FoodList.Name // Truy cập IngredientDetails100g
                    }).ToList()
                })
            .ToList().Select(data => new FoodListDTO
            {
                FoodListId = data.FoodListId,
                Name = data.FoodListName,
                ScaleAmounts = data.ScaleAmounts
            .Select(sa => mapper.Map<ScaleAmountDTO>(sa))
            .ToList()
            }).ToList(); ;




            //if (foodLists == null)
            //{
            //    return NotFound(); // Response with status code: 404
            //}
            //List<FoodListDTO> foodListsDTOs = foodLists.Select(p => mapper.Map<FoodList, FoodListDTO>(p)).ToList();

            return Ok(result);
        }

    }
}
