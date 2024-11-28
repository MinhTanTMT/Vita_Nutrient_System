namespace SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels
{
    public record AddIngredientToFoodRequest
    (int FoodId, int IngredientId, double Amount);
}
