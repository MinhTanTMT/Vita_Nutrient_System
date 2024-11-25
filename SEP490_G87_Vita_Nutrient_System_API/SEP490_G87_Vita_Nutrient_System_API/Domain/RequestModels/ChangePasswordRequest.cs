namespace SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels
{
    public record ChangePasswordRequest(int UserId, string OldPassword, string NewPassword, string ConfirmPassword);
}
