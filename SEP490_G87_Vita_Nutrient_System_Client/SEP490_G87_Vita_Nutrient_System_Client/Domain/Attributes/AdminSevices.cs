using System.Text;

namespace SEP490_G87_Vita_Nutrient_System_Client.Domain.Attributes
{
    public class AdminSevices
    {

        public string GeneratePassword(int length, bool includeUppercase = true, bool includeLowercase = true, bool includeNumbers = true, bool includeSpecialChars = false)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Password length must be greater than 0.");
            }

            // Các bộ ký tự có thể sử dụng
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string numberChars = "0123456789";
            const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?/";

            // Chuỗi ký tự được chọn để tạo mật khẩu
            string characterPool = "";

            if (includeUppercase)
            {
                characterPool += uppercaseChars;
            }

            if (includeLowercase)
            {
                characterPool += lowercaseChars;
            }

            if (includeNumbers)
            {
                characterPool += numberChars;
            }

            if (includeSpecialChars)
            {
                characterPool += specialChars;
            }

            if (string.IsNullOrEmpty(characterPool))
            {
                throw new ArgumentException("At least one character type must be selected.");
            }

            // Tạo mật khẩu
            var random = new Random();
            var passwordBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(characterPool.Length);
                passwordBuilder.Append(characterPool[randomIndex]);
            }

            return passwordBuilder.ToString();
        }

    }
}
