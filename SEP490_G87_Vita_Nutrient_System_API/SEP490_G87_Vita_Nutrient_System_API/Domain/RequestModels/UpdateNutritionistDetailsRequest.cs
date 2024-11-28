namespace SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels
{
    public record UpdateNutritionistDetailsRequest
    (int UserId,
        string? Describe,
        short Height,
        short Weight,
        short Age
        );
}
