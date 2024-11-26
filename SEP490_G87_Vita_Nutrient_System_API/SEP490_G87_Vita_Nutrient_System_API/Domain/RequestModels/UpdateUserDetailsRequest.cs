namespace SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels
{
    public record UpdateUserDetailsRequest(
        int UserId,
        string? Describe,
        short Height,
        short Weight,
        short Age,
        string? WantImprove,
        string? UnderlyingDisease 
        );
}
