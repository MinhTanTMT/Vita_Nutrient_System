using AutoMapper;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Mapper
{
    public class MappingProfile: Profile
    {

        public MappingProfile()
        {
            CreateMap<FoodList, FoodListDTO>();
            CreateMap<KeyNote, KeyNoteDTO>();
            CreateMap<FoodType, FoodTypeDTO>();
            CreateMap<ScaleAmount, ScaleAmountDTO>();
            CreateMap<IngredientDetails100g, IngredientDetails100gDTO>();
        }


    }
}
