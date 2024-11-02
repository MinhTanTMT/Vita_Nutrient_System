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
using SEP490_G87_Vita_Nutrient_System_API.Domain.DataFoodList;

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
            FoodListDTO item3 = await generateMealRepositories.TotalAllTheIngredientsOfTheDish(await generateMealRepositories.TakeAllTheIngredientsOfTheDish(3));
            List<FoodListDTO> FoodListDTO = new List<FoodListDTO>();
            FoodListDTO.Add(item1);
            FoodListDTO.Add(item2);
            FoodListDTO.Add(item3);
            return Ok(await generateMealRepositories.CheckForUserMealSettingsDetailsIsSmallerThanNeeded(await generateMealRepositories.TotalAllTheIngredientsOfTheDish(FoodListDTO), 1));
        }


        [HttpGet("APIGenerateMealController")]
        public async Task<IActionResult> APIGenerateMealController(int MealSettingsDetailsId)
        {
            GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();


            List<int> ints = new List<int>()
            {
                3,1, 5
            };
            return Ok(await generateMealRepositories.GetTheListOfDishesByMealSettingsDetails(ints, MealSettingsDetailsId));

        }


        [HttpGet("APIChangeTheDishToSuitTheTarget")]
        public async Task<IActionResult> APIChangeTheDishToSuitTheTarget(int[] idFoodOfListAlreadyExists, int MealSettingsDetailsId)
        {
            GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();

            List<int> ints = new List<int>()
            {
                4,4, 3
            };
            return Ok(await generateMealRepositories.GetTheListOfDishesByMealSettingsDetails(ints, MealSettingsDetailsId));
        }


        [HttpGet("APIRun")]
        public async Task<IActionResult> APIRun()
        {
            GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();
            return Ok(await generateMealRepositories.ListMealOfTheDay(DateTime.ParseExact("30/10/2024 00:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), 1));

        }


        [HttpGet("APIListMealOfTheDay")]
        public async Task<ActionResult<IEnumerable<DataFoodListMealOfTheDay>>> APIListMealOfTheDay(DateTime myDay, int idUser)
        {
            GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();

            IEnumerable<DataFoodListMealOfTheDay> dataFoodListMealOfTheDays;

            dataFoodListMealOfTheDays = await generateMealRepositories.ListMealOfTheDay(myDay, idUser);

            if (dataFoodListMealOfTheDays.Count() > 0)
            {
                return Ok(dataFoodListMealOfTheDays);
            }
            else
            {
                if (await generateMealRepositories.FillInDishIdInDailyDish(idUser, myDay))
                {
                    dataFoodListMealOfTheDays = await generateMealRepositories.ListMealOfTheDay(myDay, idUser);
                    if (dataFoodListMealOfTheDays.Count() > 0)
                    {
                        return Ok(dataFoodListMealOfTheDays);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet("APIRefreshTheMeal")]
        public async Task<ActionResult<IEnumerable<DataFoodListMealOfTheDay>>> APIRefreshTheMealy(DateTime myDay, int idUser)
        {
            GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();
            return Ok(await generateMealRepositories.FillInDishIdInDailyDish(idUser, myDay));
        }
        



    }
}
