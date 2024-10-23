using SEP490_G87_Vita_Nutrient_System_API.DTO.User;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IUserRepositories
    {

        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///

        dynamic GetUserLogin(string account, string password);
        bool CheckExitAccountUser(string account);
        dynamic GetUserRegister(User user);
        dynamic GetUserById(int id);
        dynamic ChangePassword(ChangePasswordDTO model);



        ////////////////////////////////////////////////////////////
        /// Dũng
        ////////////////////////////////////////////////////////////
        ///





        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///





        ////////////////////////////////////////////////////////////
        /// Sơn
        ////////////////////////////////////////////////////////////
        ///

        IQueryable<User> GetAllUsers();
        IQueryable<User> GetUsersByRole(int roleId);
        User? GetUserDetailsInfo(int id);
        void UpdateUser(User user);
        User? GetNutritionistDetailsInfo(int id);
        IQueryable<ExpertPackage> GetNutritionistPackages(int id);

        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///

    }
}
