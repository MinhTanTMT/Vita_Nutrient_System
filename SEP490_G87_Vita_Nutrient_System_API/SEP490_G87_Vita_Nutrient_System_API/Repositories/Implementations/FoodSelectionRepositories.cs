using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class FoodSelectionRepositories : IFoodSelectionRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        public FoodSelectionRepositories()
        {
        }

        public bool IsExist(int UserId, int FoodId)
        {
            FoodSelection fs = _context.FoodSelections
                .SingleOrDefault(f => f.UserId == UserId && f.FoodListId == FoodId);

            return fs is not null;
        }

        public void AddNew(int UserId, int FoodId)
        {
            FoodSelection foodSelection = new FoodSelection
            {
                UserId = UserId,
                FoodListId = FoodId,
                Rate = null,
                RecurringId = null,
                IsBlock = false,
                IsCollection = false,
                IsLike = false
            };

            _context.FoodSelections.Add(foodSelection);
            _context.SaveChanges();
        }

        public void UserBlockFood(int UserId, int FoodId)
        {
            if(!IsExist(UserId, FoodId))
            {
                AddNew(UserId, FoodId);
            }

            FoodSelection fs = _context.FoodSelections
                .SingleOrDefault(f => f.UserId == UserId && f.FoodListId == FoodId);

            fs.IsBlock = true;
            _context.Entry<FoodSelection>(fs).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void UserLikeOrUnlikeFood(int UserId, int FoodId)
        {
            if (!IsExist(UserId, FoodId))
            {
                AddNew(UserId, FoodId);
            }

            FoodSelection fs = _context.FoodSelections
                .SingleOrDefault(f => f.UserId == UserId && f.FoodListId == FoodId);

            if(fs.IsLike is null || fs.IsLike == false)
            {
                fs.IsLike = true;
            }
            else
            {
                fs.IsLike = false;
            }
            
            _context.Entry<FoodSelection>(fs).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void UserRecurFood(int UserId, int FoodId, short recurId)
        {
            if (!IsExist(UserId, FoodId))
            {
                AddNew(UserId, FoodId);
            }

            FoodSelection fs = _context.FoodSelections
                .SingleOrDefault(f => f.UserId == UserId && f.FoodListId == FoodId);

            fs.RecurringId = recurId == -1? null : recurId;

            _context.Entry<FoodSelection>(fs).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void UserSaveOrUnsaveFood(int UserId, int FoodId)
        {
            if (!IsExist(UserId, FoodId))
            {
                AddNew(UserId, FoodId);
            }

            FoodSelection fs = _context.FoodSelections
                .SingleOrDefault(f => f.UserId == UserId && f.FoodListId == FoodId);

            if (fs.IsCollection is null || fs.IsCollection == false)
            {
                fs.IsCollection = true;
            }
            else
            {
                fs.IsCollection = false;
            }

            _context.Entry<FoodSelection>(fs).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public FoodSelection Get(int UserId, int FoodId)
        {
            return _context.FoodSelections.SingleOrDefault(fs => fs.UserId == UserId && fs.FoodListId == FoodId);
        }

        public void UserRateFood(int UserId, int FoodId, short rate)
        {
            bool a = IsExist(UserId, FoodId);
            //add food selection if not existed
            if (!a)
            {
                AddNew(UserId, FoodId);
            }

            FoodSelection fs = _context.FoodSelections
                .SingleOrDefault(f => f.UserId == UserId && f.FoodListId == FoodId);

            //update food rate and number rate
            FoodList f = _context.FoodLists.Find(FoodId);

            if (!a)
            {
                f.Rate = ((f.Rate / 20 * f.NumberRate) + rate) / (f.NumberRate + 1) * 20;
                f.NumberRate += 1;
            }
            else
            {
                f.Rate = ((f.Rate / 20 * f.NumberRate) - fs.Rate + rate) / f.NumberRate * 20;
            }

            _context.Entry<FoodList>(f).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            //set rate
            fs.Rate = rate;

            _context.Entry<FoodSelection>(fs).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
