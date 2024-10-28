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

            GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();
            //return Ok(await generateMealRepositories.GetTheListOfDishesByMealSettingsDetails(1));


            //GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();
            //return Ok(await generateMealRepositories.TotalAllTheIngredientsOfTheDish(await generateMealRepositories.TakeAllTheIngredientsOfTheDish(1)));

            //GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();
            //return Ok(await generateMealRepositories.CheckForUserMealSettingsDetails(await generateMealRepositories.TotalAllTheIngredientsOfTheDish(await generateMealRepositories.TakeAllTheIngredientsOfTheDish(1)), 1));

            //MealSettingsDetail mealSettingsDetail = _context.MealSettingsDetails.Find(1);
            //NutritionTargetsDaily nutritionTargetsDaily = await _context.NutritionTargetsDailies.FindAsync(mealSettingsDetail.NutritionTargetsDailyId);

            //List<int> idFoodListSystem = await _context.FoodLists.Include(x => x.FoodSelections).Include(x => x.FoodAndDiseases).Where(x => x.FoodTypeId == nutritionTargetsDaily.FoodTypeIdWant && x.FoodSelections.FirstOrDefault(y => y.UserId == 1 && y.FoodListId == 1).IsBlock == true).Select(x => x.FoodListId).ToListAsync();
            //List<int> idFoodListSystem = _context.FoodLists.Include(x => x.FoodSelections).Where(x => x.FoodTypeId == nutritionTargetsDaily.FoodTypeIdWant && x.FoodSelections.FirstOrDefault(y => y.UserId == 1 && y.FoodListId == 1).IsBlock == false && x.FoodAndDiseases.FirstOrDefault(x => x.ListOfDiseasesId == 1 && x.FoodListId == 1).IsGoodOrBad == true).Select(x => x.FoodListId).ToList();



            //var mealSettingsDetail = _context.MealSettingsDetails.Include(x => x.MealSettings).FirstOrDefaultAsync(x => x.Id == 1).Select(x => new
            //{
            //    x.MealSettingsId,
            //    x.MealSettings.UserId,
            //    x.Id
            //});

            var mealSettingsDetail = _context.MealSettingsDetails.Include(x => x.MealSettings).FirstOrDefaultAsync(x => x.Id == 1);


            //return Ok(await generateMealRepositories.FilterTheTypeDiseaseBlockList(nutritionTargetsDaily));

            return Ok(mealSettingsDetail);

        }




        ///// <summary>
        ///// test Check Split
        ///// </summary>
        ///// <returns></returns>
        //        [HttpGet("APGenerateMealController")]
        //public async Task<IActionResult> APIGenerateMealController()
        //{

        //    GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();

        //    MealSettingsDetailDTO mealSettingsDetail = new MealSettingsDetailDTO()
        //    {
        //        Size = "Bữa lớn",
        //        WantCookingId = 1,
        //        TypeFavoriteFood = "2"
        //    };




        //    string[] result = await generateMealRepositories.SplitAndProcessFirst("#WantCooking=1#SlotOfTheDay:1;2;#Size=Bữa lớn");

        //    string checkKQ = "Null";

        //    foreach (var item in result)
        //    {

        //        if (item.Contains("="))
        //        {
        //            Dictionary<string, string> dataProcess1 = await generateMealRepositories.SplitAndProcess1(item);

        //            if (generateMealRepositories.IsNumeric(dataProcess1.Values.FirstOrDefault().ToString()))
        //            {
        //                if (dataProcess1.Keys.FirstOrDefault().Equals("WantCooking"))
        //                {
        //                    if (dataProcess1.Values.FirstOrDefault().Equals(mealSettingsDetail.WantCookingId.ToString()))
        //                    {
        //                        checkKQ += "OK2" + mealSettingsDetail.WantCookingId;

        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (dataProcess1.Keys.FirstOrDefault().Equals("Size"))
        //                {

        //                    //return Ok(dataProcess1.Keys.FirstOrDefault().Equals("Size"));


        //                    if (dataProcess1.Values.FirstOrDefault().Equals(mealSettingsDetail.Size))
        //                    {
        //                        checkKQ += "OK1" + mealSettingsDetail.Size;
        //                    }
        //                }
        //            }
        //        }
        //        if (item.Contains(":"))
        //        {
        //            Dictionary<string, int[]> dataProcess2 = await generateMealRepositories.SplitAndProcess2(item);
        //            if (dataProcess2.Keys.FirstOrDefault().Equals("SlotOfTheDay"))
        //            {
        //                if (dataProcess2.Values.FirstOrDefault().Contains(Int32.Parse(mealSettingsDetail.TypeFavoriteFood)))
        //                {
        //                    checkKQ += "OK3" + mealSettingsDetail.TypeFavoriteFood;
        //                };
        //            }
        //        }
        //    }

        //    return Ok(checkKQ);

        //}

    }
}
