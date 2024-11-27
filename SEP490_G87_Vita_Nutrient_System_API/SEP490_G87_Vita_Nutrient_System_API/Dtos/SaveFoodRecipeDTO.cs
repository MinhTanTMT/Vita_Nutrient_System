namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class SaveFoodRecipeDTO
    {
        public int FoodId { get; set; }
        public List<RecipeDTO> Recipes { get; set; }
    }
}
