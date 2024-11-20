using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class ExpertPackageRepositories : IExpertPackageRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();
        public ExpertPackageRepositories() { }
        

        public ExpertPackage GetExpertPackage(short id)
        {
            return _context.ExpertPackages.Find(id);
        }

        public List<ExpertPackage> GetExpertPackages()
        {
            return _context.ExpertPackages.ToList();
        }

        public void AddExpertPackage(ExpertPackage expertPackage)
        {
            _context.ExpertPackages.Add(expertPackage);
            _context.SaveChanges();
        }

        public void UpdateExpertPackage(ExpertPackage expertPackage)
        {
            _context.Entry<ExpertPackage>(expertPackage).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void RemoveExpertPackage(short id)
        {
            ExpertPackage package = _context.ExpertPackages.Find(id);
            if(package is not null)
            {
                _context.ExpertPackages.Remove(package);
                _context.SaveChanges();
            }
        }
    }
}
