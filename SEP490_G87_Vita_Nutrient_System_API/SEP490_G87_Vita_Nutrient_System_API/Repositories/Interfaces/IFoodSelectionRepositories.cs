using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IFoodSelectionRepositories
    {
        bool IsExist(int UserId, int FoodId);
        FoodSelection Get(int UserId, int FoodId);
        void AddNew(int UserId, int FoodId);
        void UserBlockFood(int UserId, int FoodId);
        void UserLikeOrUnlikeFood(int UserId, int FoodId);
        void UserSaveOrUnsaveFood(int UserId, int FoodId);
        void UserRecurFood(int UserId, int FoodId, short recurId);
        void UserRateFood(int UserId, int FoodId, short rate);
    }
}
