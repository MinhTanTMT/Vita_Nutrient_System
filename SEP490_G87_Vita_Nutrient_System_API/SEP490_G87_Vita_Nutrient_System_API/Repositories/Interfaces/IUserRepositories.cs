using SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels;
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
        ///

        Task<string> DecryptPassword(string encryptedPassword); // se bo som

        Task<bool> CheckAccountUserNull(string account, string accGoogle);
        Task<bool> ForgotPassword(string emailGoogle);
        Task<UserLoginRegister> GetUserLogin(string account, string password);
        Task<UserLoginRegister> GetUserRegister(UserLoginRegister user);
        dynamic GetUserById(int id);
        dynamic ChangePassword(ChangePasswordDTO model);
        Task<UserLoginRegister> GetRegisterLoginGoogle(UserLoginRegister user);
        Task<String> EncryptPassword(string password);
        Task<string> GeneratePassword(int length, bool includeUppercase = true, bool includeLowercase = true, bool includeNumbers = true, bool includeSpecialChars = false);

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
        ExpertPackage GetNutritionistPackages(short id);

        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///

        Task<PagedResult<FoodList>> GetLikedFoods(int userId, GetLikeFoodDTO model);
        Task<string> LikeOrUnlikeFood(int userId, int foodId);
        Task<string> UnblockFood(int userId, int foodId);
        Task<PagedResult<FoodList>> GetBlockedFoods(int userId, GetLikeFoodDTO model);
        Task<PagedResult<FoodList>> ListCollectionFood(int userId, GetLikeFoodDTO model);
        Task<string> SaveCollection(int userId, int foodId);

    }
}
