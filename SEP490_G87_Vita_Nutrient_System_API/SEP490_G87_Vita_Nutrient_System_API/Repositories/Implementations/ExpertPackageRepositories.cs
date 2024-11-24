using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Versioning;
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

        public List<User> GetUsersByPackage(short id)
        {
            ExpertPackage e = _context.ExpertPackages
                .Include(p => p.NutritionistDetails).SingleOrDefault(p => p.Id == id);

            List<User> users = new List<User>();
            
            foreach(var n in e.NutritionistDetails)
            {
                users.Add(_context.Users.Find(n.NutritionistId));
            }

            return users;
        }

        public void AddNutritionistToPackage(int nId, short pId)
        {
            NutritionistDetail nutritionistDetail = new NutritionistDetail
            {
                NutritionistId = nId,
                ExpertPackagesId = pId
            };

            _context.NutritionistDetails.Add(nutritionistDetail);
            _context.SaveChanges();
        }

        public bool RemoveNutritionistFromPackage(int nId, short pId)
        {
            NutritionistDetail nutritionistDetail = _context.NutritionistDetails.SingleOrDefault
                (x=> x.NutritionistId == nId && x.ExpertPackagesId == pId);

            if(nutritionistDetail is null)
            {
                return false;
            }
            else
            {
                _context.NutritionistDetails.Remove(nutritionistDetail);
                _context.SaveChanges ();
                return true;
            }
        }
    }
}
