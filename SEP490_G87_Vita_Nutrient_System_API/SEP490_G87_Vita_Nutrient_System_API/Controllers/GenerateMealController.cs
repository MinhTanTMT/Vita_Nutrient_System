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


            return Ok(0);
        }




        ///// <summary>
        ///// test Check Split
        ///// </summary>
        ///// <returns></returns>
                [HttpGet("APGenerateMealController")]
        public async Task<IActionResult> APIGenerateMealController()
        {

            GenerateMealRepositories generateMealRepositories = new GenerateMealRepositories();

            MealSettingsDetailDTO mealSettingsDetail = new MealSettingsDetailDTO()
            {
                Size = "Bữa lớn",
                WantCookingId = 1,
                TypeFavoriteFood = "2"
            };




            string[] result = await generateMealRepositories.SplitAndProcessFirst("#WantCooking=1#SlotOfTheDay:1;2;#Size=Bữa lớn");

            string checkKQ = "Null";

            foreach (var item in result)
            {

                if (item.Contains("="))
                {
                    Dictionary<string, string> dataProcess1 = await generateMealRepositories.SplitAndProcess1(item);

                    if (generateMealRepositories.IsNumeric(dataProcess1.Values.FirstOrDefault().ToString()))
                    {
                        if (dataProcess1.Keys.FirstOrDefault().Equals("WantCooking"))
                        {
                            if (dataProcess1.Values.FirstOrDefault().Equals(mealSettingsDetail.WantCookingId.ToString()))
                            {
                                checkKQ += "OK2" + mealSettingsDetail.WantCookingId;

                            }
                        }
                    }
                    else
                    {
                        if (dataProcess1.Keys.FirstOrDefault().Equals("Size"))
                        {

                            //return Ok(dataProcess1.Keys.FirstOrDefault().Equals("Size"));


                            if (dataProcess1.Values.FirstOrDefault().Equals(mealSettingsDetail.Size))
                            {
                                checkKQ += "OK1" + mealSettingsDetail.Size;
                            }
                        }
                    }
                }
                if (item.Contains(":"))
                {
                    Dictionary<string, int[]> dataProcess2 = await generateMealRepositories.SplitAndProcess2(item);
                    if (dataProcess2.Keys.FirstOrDefault().Equals("SlotOfTheDay"))
                    {
                        if (dataProcess2.Values.FirstOrDefault().Contains(Int32.Parse(mealSettingsDetail.TypeFavoriteFood)))
                        {
                            checkKQ += "OK3" + mealSettingsDetail.TypeFavoriteFood;
                        };
                    }
                }
            }

            return Ok(checkKQ);

        }

    }
}
