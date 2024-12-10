using Humanizer;

namespace SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels
{
    public record UpdateUserInfoRequest(
        int UserId,
        string FirstName,
        string LastName,
        DateTime? DOB,
        bool Gender,
        string? Address,
        string? Phone
        );

    public record UploadAvatarRequest(int UserId, string ImageURL);
}
