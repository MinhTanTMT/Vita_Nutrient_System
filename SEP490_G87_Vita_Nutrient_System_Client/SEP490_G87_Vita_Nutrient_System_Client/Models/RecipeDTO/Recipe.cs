namespace SEP490_G87_Vita_Nutrient_System_Client.Models.RecipeDTO
{
    public class RecipeDT
    {
        public int RecipeId { get; set; }

        public int FoodListId { get; set; }

        public short? NumericalOrder { get; set; }

        public string? Describe { get; set; }

        public string? Urlimage { get; set; }
    }
}
