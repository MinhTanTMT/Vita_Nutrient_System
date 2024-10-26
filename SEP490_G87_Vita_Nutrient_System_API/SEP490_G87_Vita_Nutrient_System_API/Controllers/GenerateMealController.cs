using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Mapper;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using System.Collections.Generic;
using System;
using Castle.Core.Configuration;
using Humanizer;

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



        [HttpGet("APCheckGenerateMealController")]
        public async Task<IActionResult> APCheckGenerateMealController()
        {
            GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();


            FoodListDTO item1 = await generateMealRepositories.TotalAllTheIngredientsOfTheDish(await generateMealRepositories.TakeAllTheIngredientsOfTheDish(4));
            FoodListDTO item2 = await generateMealRepositories.TotalAllTheIngredientsOfTheDish(await generateMealRepositories.TakeAllTheIngredientsOfTheDish(4));
            FoodListDTO item3 = await generateMealRepositories.TotalAllTheIngredientsOfTheDish(await generateMealRepositories.TakeAllTheIngredientsOfTheDish(4));
            List<FoodListDTO> FoodListDTO = new List<FoodListDTO>();
            FoodListDTO.Add(item1);
            FoodListDTO.Add(item2);
            FoodListDTO.Add(item3);
            return Ok(await generateMealRepositories.CheckForUserMealSettingsDetails(await generateMealRepositories.TotalAllTheIngredientsOfTheDish(FoodListDTO), 1));
        }


        [HttpGet("APIGenerateMealController")]
        public async Task<IActionResult> APIGenerateMealController(int MealSettingsDetailsId)
        {
            GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();
            return Ok(await generateMealRepositories.GetTheListOfDishesByMealSettingsDetails(MealSettingsDetailsId));
        }


        [HttpGet("APIChangeTheDishToSuitTheTarget")]
        public async Task<IActionResult> APIChangeTheDishToSuitTheTarget(int[] idFoodOfListAlreadyExists, int MealSettingsDetailsId)
        {
            GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();


            return Ok(await generateMealRepositories.GetTheListOfDishesByMealSettingsDetails(MealSettingsDetailsId));
        }


        [HttpGet("APIRun")]
        public async Task<IActionResult> APIRun()
        {
            GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();
            await generateMealRepositories.FillInDishIdInDailyDish(1);

            return Ok();
        }



    }
}
