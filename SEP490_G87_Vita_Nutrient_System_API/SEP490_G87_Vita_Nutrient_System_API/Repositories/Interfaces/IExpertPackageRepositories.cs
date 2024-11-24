using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IExpertPackageRepositories
    {
        List<ExpertPackage> GetExpertPackages();
        ExpertPackage GetExpertPackage(short id);
        void AddExpertPackage(ExpertPackage expertPackage);
        void RemoveExpertPackage(short id);
        void UpdateExpertPackage(ExpertPackage expertPackage);
        List<User> GetUsersByPackage(short id);
        void AddNutritionistToPackage(int nId, short pId);
        bool RemoveNutritionistFromPackage(int nId, short pId);
    }
}
