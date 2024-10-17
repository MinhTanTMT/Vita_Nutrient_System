using Microsoft.EntityFrameworkCore;
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

        public IQueryable<User> GetAllUsers()
        {
            return _context.Users;
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





        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///


    }
}
