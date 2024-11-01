using SEP490_G87_Vita_Nutrient_System_API.Dtos;
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
        Task<List<FoodList>> GetLikedFoods(GetLikeFoodDTO model);
        void LikeOrUnlikeFood(int userId, int foodId);
        void UnblockFood(int userId, int foodId);
        Task<List<FoodList>> GetBlockedFoods(GetLikeFoodDTO model);



        ////////////////////////////////////////////////////////////
        /// Dũng
        ////////////////////////////////////////////////////////////
        /// 123





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
