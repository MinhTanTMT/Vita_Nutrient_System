using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Domain.Enums;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class UsersRepositories : IUserRepositories
    {

        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        public UsersRepositories()
        {
        }

        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///
        public dynamic GetUserLogin(string account, string password)
        {
            if (_context.Users == null)
            {
                return null;
            }
            var user = _context.Users.Include(u => u.RoleNavigation).FirstOrDefault(u => u.Account == account && u.Password == password);
            if (user == null)
            {
                return null;
            }
            var UserLogin = new
            {
                FullName = user.FirstName + " " + user.LastName,
                user.Urlimage,
                user.Account,
                user.RoleNavigation.RoleName,
                user.UserId
            };
            return UserLogin;
        }

        public bool CheckExitAccountUser(string account)
        {
            if (_context.Users == null)
            {
                return false;
            }
            var user = _context.Users.FirstOrDefault(u => u.Account.Equals(account));
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public dynamic GetUserRegister(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            //var userReturn = await _context.Users.FirstOrDefaultAsync(u => u.Account.Equals(user.Account) && u.Password.Equals(user.Password));
            //if (userReturn == null)
            //{
            //    return NotFound();
            //}

            return user;
        }

        public dynamic GetUserById(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }


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
        public IQueryable<User> GetAllUsers()
        {
            return _context.Users.Include(u => u.RoleNavigation);
        }

        public IQueryable<User> GetUsersByRole(int roleId)
        {
            return _context.Users.Include(u => u.RoleNavigation).Where(u => u.Role == roleId);
        }

        public User? GetUserDetailsInfo(int id)
        {
            var user = _context.Users
                .Include(u => u.RoleNavigation)
                .Include(u => u.UserDetail)
                .FirstOrDefault(u => u.UserId == id);

            if (user.Role != (int)UserRole.USERPREMIUM && user.Role != (int)UserRole.USER)
                return null;

                return user;
        }

        public User? GetNutritionistDetailsInfo(int id)
        {
            var nutritionist = _context.Users
                .Include(u => u.RoleNavigation)
                .Include(u => u.NutritionistDetail)
                .FirstOrDefault(u => u.UserId == id);

            if (nutritionist.Role != (int)UserRole.NUTRITIONIST)
                return null;

            return nutritionist;
        }

        public void UpdateUser(User user)
        {
            _context.Entry<User>(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<ExpertPackage> GetNutritionistPackages(int id)
        {
            return _context.ExpertPackages.Where(p => p.NutritionistDetailsId == id);
        }



        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///


    }
}
