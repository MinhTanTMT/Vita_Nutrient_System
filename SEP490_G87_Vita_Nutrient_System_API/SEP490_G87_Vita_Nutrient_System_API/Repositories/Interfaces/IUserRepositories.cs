using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.PageResult;

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
        Task<PagedResult<FoodList>> GetLikedFoods(int userId, GetLikeFoodDTO model);
        Task<string> LikeOrUnlikeFood(int userId, int foodId);
        Task<string> UnblockFood(int userId, int foodId);
        Task<PagedResult<FoodList>> GetBlockedFoods(int userId, GetLikeFoodDTO model);



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
        Task<UserDetail> GetUserDetailByUserIdAsync(int userId);
        void UpdateUser(User user);
        User? GetNutritionistDetailsInfo(int id);
        ExpertPackage GetNutritionistPackages(int id);

        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///

    }
}
