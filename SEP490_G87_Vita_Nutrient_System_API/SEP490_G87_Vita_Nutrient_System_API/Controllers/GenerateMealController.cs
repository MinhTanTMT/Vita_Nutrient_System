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
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateMealController : ControllerBase
    {

        Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext(); // xoa sau

        private IGenerateMealRepositories repositories = new GenerateMealRepositories();

        private MapperConfiguration config;
        private IMapper mapper;
        public GenerateMealController()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            mapper = config.CreateMapper();
        }



        [HttpGet("APIRun")]
        public async Task<IActionResult> APIRun()
        {


            //GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();
            //return Ok(await generateMealRepositories.ListMealOfTheDay(DateTime.ParseExact("30/10/2024 00:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), 1));


            List<DietWithFoodType> courseList = _context.DietTypes
            .SelectMany(x => x.FoodTypes, (a, b) => new DietWithFoodType
            {
                DietTypeId = a.DietTypeId,
                FoodTypeId = b.FoodTypeId
            })
            .ToList();



            return Ok(courseList);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>


        [HttpGet("APIListMealOfTheDay")]
        public async Task<ActionResult<IEnumerable<DataFoodListMealOfTheDay>>> APIListMealOfTheDay(DateTime myDay, int idUser)
        {

            await repositories.CreateMealSetting(idUser);

            IEnumerable<DataFoodListMealOfTheDay> dataFoodListMealOfTheDays;

            dataFoodListMealOfTheDays = await repositories.ListMealOfTheDay(myDay, idUser);

            if (dataFoodListMealOfTheDays.Count() > 0)
            {
                return Ok(dataFoodListMealOfTheDays);
            }
            else if (myDay.DayOfYear < DateTime.Now.DayOfYear)
            {
                return Ok(dataFoodListMealOfTheDays);
            }
            else
            {
                if (await repositories.FillInDishIdInDailyDishWithCondition(idUser, myDay))
                {
                    dataFoodListMealOfTheDays = await repositories.ListMealOfTheDay(myDay, idUser);
                    if (dataFoodListMealOfTheDays.Count() > 0)
                    {
                        return Ok(dataFoodListMealOfTheDays);
                    }
                    else
                    {
                        return Ok(dataFoodListMealOfTheDays);
                    }
                }
                else
                {
                    return Ok(dataFoodListMealOfTheDays);
                }
            }
        }


        [HttpGet("APIListMealOfTheWeek")]
        public async Task<ActionResult<IEnumerable<DataFoodAllDayOfWeek>>> APIListMealOfTheWeek(DateTime myDay, int idUser)
        {
            await repositories.CreateMealSetting(idUser);
            IEnumerable<DataFoodAllDayOfWeek> dataFoodAllDayOfWeek = await repositories.ListMealOfTheWeek(myDay, idUser);
            return Ok(dataFoodAllDayOfWeek);
        }



        [HttpGet("APIRefreshTheMeal")]
        public async Task<IActionResult> APIRefreshTheMealy(DateTime myDay, int idUser)
        {

            if (myDay.DayOfYear >= DateTime.Now.DayOfYear)
            {
                return Ok(await repositories.FillInDishIdInDailyDishWithCondition(idUser, myDay));
            }
            else
            {
                return Ok(false);
            }   
        }


        [HttpGet("APIRefreshTheAllMeal")]
        public async Task<IActionResult> APIRefreshTheAllMeal(DateTime myDay, int idUser)
        {
            return Ok(await repositories.RegenerateListMealOfTheWeek(myDay, idUser));

        }


        [HttpPost("APICompleteTheDish")]
        public async Task<ActionResult<IEnumerable<DataFoodListMealOfTheDay>>> APICompleteTheDish([FromBody] FoodStatusUpdateModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            
            // update vu tich trong 7 ngay
            if (model.MyDay.DayOfYear <= DateTime.Now.DayOfYear && model.MyDay.DayOfYear >= (DateTime.Now.DayOfYear-7))
            {
                if (model.StatusSymbol.Equals("-"))
                {
 
                    return Ok(await repositories.ModifiedCompleteTheDish(model, "+", null, null));
                }
                else if (model.StatusSymbol.Equals("+"))
                {
                    return Ok(await repositories.ModifiedCompleteTheDish(model, "-", null, null));
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }  
        }

        [HttpPost("APICreateListOfAlternativeDishes")]
        public async Task<ActionResult<IEnumerable<DataFoodListMealOfTheDay>>> APICreateListOfAlternativeDishes([FromBody] AlternativeDishesRequest request, [FromQuery] int foodSelectionType)
        {
            return Ok(await repositories.CreateListOfAlternativeDishes(request.ListIdFood, request.MealSettingsDetailsId, request.NumberOfCreation, foodSelectionType));
        }


        [HttpPost("APIgetThisListOfDishes")]
        public async Task<IActionResult> APIgetThisListOfDishes(
        [FromBody] DataFoodListMealOfTheDay dataFoodListMealOfTheDay,
        [FromQuery] int userId,
        [FromQuery] DateTime myDay)
        {
            return Ok(await repositories.GetThisListOfDishesInputMealDay(dataFoodListMealOfTheDay, userId, myDay));
        }


        [HttpPost("APISelectReplaceCurrentFood")]
        public async Task<IActionResult> APISelectReplaceCurrentFood(
        [FromBody] FoodStatusUpdateModel model,
        [FromQuery] int idFoodSelect)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await repositories.ModifiedCompleteTheDish(model, null, idFoodSelect, null));
        }


        [HttpGet("APISystemUserConfiguration")]
        public async Task<IActionResult> APISystemUserConfiguration(int idUser)
        {
            if (await repositories.CreateMealSetting(idUser))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
