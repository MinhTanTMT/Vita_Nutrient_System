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
    }
}
