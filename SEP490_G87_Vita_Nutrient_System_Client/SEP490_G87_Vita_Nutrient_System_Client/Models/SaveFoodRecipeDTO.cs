namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class SaveFoodRecipeDTO
    {
        public int FoodId { get; set; }
        public List<RecipeDT> Recipes { get; set; }
    }
}
