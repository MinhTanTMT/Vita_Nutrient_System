namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class ChangePasswordDTO
    {
        public string Account { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
