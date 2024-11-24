
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using SEP490_G87_Vita_Nutrient_System_API.Domain.Enums;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.PageResult;
using AutoMapper;
using SEP490_G87_Vita_Nutrient_System_API.Mapper;
using System.Net.Mail;
using System.Net;
using SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class UsersRepositories : IUserRepositories
    {

        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();
        private MapperConfiguration config;
        private IMapper mapper;

        public UsersRepositories()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            mapper = config.CreateMapper();
        }

        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///

        public async Task<bool> SendMail(string emailAccount, string subject, string contentSend)
        {

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var emailSettings = config.GetSection("EmailSettings");
            string fromEmail = emailSettings["FromEmail"];
            string emailPassword = emailSettings["Password"];
            string smtpHost = emailSettings["Host"];
            int smtpPort = int.Parse(emailSettings["Port"]);

            var fromAddress = new MailAddress(fromEmail);
            var toAddress = new MailAddress(emailAccount);

            var smtp = new SmtpClient
            {
                Host = smtpHost,
                Port = smtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, emailPassword),
                Timeout = 30000
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = contentSend,
            })
            {
                smtp.Send(message);
            }

            return true;
        }



        public async Task<bool> ForgotPassword(string emailGoogle)
        {
            try
            {
                var inforAccount = await _context.Users.FirstOrDefaultAsync(x => x.AccountGoogle.Equals(emailGoogle));
                if (inforAccount != null)
                {
                    string? Passwork = $"Mật khẩu hiện tại của bạn: {inforAccount.Password}";
                    await SendMail(emailGoogle, "Mật khẩu của bạn", Passwork);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public dynamic GetUserLogin(string account, string password)
        {
            if (_context.Users == null)
            {
                return null;
            }
            modifyPremiumAccount(account, null);
            var user = _context.Users.Include(u => u.RoleNavigation).FirstOrDefault(u => u.Account == account && u.Password == password && u.IsActive == true);
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
            return user;
        }


        private bool modifyPremiumAccount(string? Account, string? AccountGoogle)
        {
            short roleUser = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<short>("roleUser");
            short roleUserPremium = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<short>("roleUserPremium");
            
            if (AccountGoogle != null)
            {
                var accGoogle = _context.Users.FirstOrDefault(x => x.AccountGoogle.Equals(AccountGoogle));
                if(accGoogle != null)
                {
                    var data = _context.UserListManagements.FirstOrDefault(x =>
                    x.UserId == accGoogle.UserId
                    && x.StartDate <= DateTime.Now
                    && x.EndDate >= DateTime.Now && x.IsDone == false);

                    if (data == null)
                    {
                        accGoogle.Role = roleUser;
                        _context.SaveChanges();
                        return true;
                    }
                }
            }
            else
            {
                var accGoogle = _context.Users.FirstOrDefault(x => x.Account.Equals(Account));
                if (accGoogle != null)
                {
                    var data = _context.UserListManagements.FirstOrDefault(x =>
                    x.UserId == accGoogle.UserId
                    && x.StartDate <= DateTime.Now
                    && x.EndDate >= DateTime.Now && x.IsDone == false);

                    if (data == null)
                    {
                        accGoogle.Role = roleUser;
                        _context.SaveChanges();
                        return true;
                    }
                }
            }
            return true;
        }


        public dynamic GetRegisterLoginGoogle(User user)
        {
            modifyPremiumAccount(null, user.AccountGoogle);
            var accGoogle = _context.Users.FirstOrDefault(x => x.AccountGoogle == user.AccountGoogle);

            if (accGoogle == null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                string roleName = _context.Roles.Find(user.Role).RoleName;
                var UserLogin = new
                {
                    FullName = user.FirstName + " " + user.LastName,
                    user.Urlimage,
                    user.Account,
                    RoleName = roleName,
                    user.UserId,
                    user.AccountGoogle
                };
                return UserLogin;
            }
            else
            {
                string roleName = _context.Roles.Find(accGoogle.Role).RoleName;     
                var UserLogin = new
                {
                    FullName = user.FirstName + " " + user.LastName,
                    accGoogle.Urlimage,
                    accGoogle.Account,
                    RoleName = roleName,
                    accGoogle.UserId,
                    user.AccountGoogle
                };
                return UserLogin;
            }
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
        public async Task<UserDetail> GetUserDetailByUserIdAsync(int userId)
        {
            return await _context.UserDetails
                .FirstOrDefaultAsync(ud => ud.UserId == userId);
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
            //var nutritionist = _context.Users
            //    .Include(u => u.RoleNavigation)
            //    .Include(u => u.NutritionistDetail)
            //    .FirstOrDefault(u => u.UserId == id);

            //if (nutritionist.Role != (int)UserRole.NUTRITIONIST)
            //    return null;

            //return nutritionist;


            return null; // Bổ sung tránh lỗi

        }

        public void UpdateUser(User user)
        {
            _context.Entry<User>(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<ExpertPackage> GetNutritionistPackages(int id)
        {
            //return _context.ExpertPackages.Where(p => p.NutritionistDetailsId == id);

            return null; // Bổ sung tránh lỗi

        }



        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///
        public dynamic ChangePassword(ChangePasswordDTO model)
        {
            var user = _context.Users.FirstOrDefault(t => t.Account == model.Account);
            if (user == null)
            {
                throw new ApplicationException("Account does not exist");
            }
            if (model.CurrentPassword != user.Password)
            {
                throw new ApplicationException("Your current password is not match!");
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                throw new ApplicationException("Your new password and confirm password is not match!");
            }
            user.Password = model.NewPassword;
            _context.Update(user);
            _context.SaveChanges();

            return user;
        }

        public async Task<PagedResult<FoodList>> GetLikedFoods(int userId, GetLikeFoodDTO model)
        {
            var query = _context.FoodSelections
                        .Where(fs => fs.UserId == userId && (bool)fs.IsLike)
                        .Join(_context.FoodLists, fs => fs.FoodListId, f => f.FoodListId, (fs, f) => f);

            if (query == null)
            {
                throw new ApplicationException("Not found!");
            }

            if (!string.IsNullOrEmpty(model.Search))
            {
                query = query.Where(f => f.Name.Contains(model.Search));
            }

            int totalRecords = query.Count();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)model.PageSize);

            var paginatedFoods = await query
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToListAsync();
            return new PagedResult<FoodList>
            {
                Items = paginatedFoods,
                TotalPages = totalPages,
                CurrentPage = model.Page
            };
        }

        public async Task<string> LikeOrUnlikeFood(int userId, int foodId)
        {
            var foodSelection = await _context.FoodSelections
            .FirstOrDefaultAsync(fs => fs.UserId == userId && fs.FoodListId == foodId);

            if (foodSelection == null) return "Not found";

            if (foodSelection.IsLike == null)
            {
                foodSelection.IsLike = true;
            }

            foodSelection.IsLike = !foodSelection.IsLike;
            _context.Update(foodSelection);
            await _context.SaveChangesAsync();

            return "Success";
        }

        public async Task<PagedResult<FoodList>> GetBlockedFoods(int userId, GetLikeFoodDTO model)
        {
            var query = _context.FoodSelections
                .Where(fs => fs.UserId == userId && (bool)fs.IsBlock)
                .Join(_context.FoodLists, fs => fs.FoodListId, f => f.FoodListId, (fs, f) => f);

            if (query == null)
            {
                throw new ApplicationException("Not found!");
            }

            if (!string.IsNullOrEmpty(model.Search))
            {
                query = query.Where(f => f.Name.Contains(model.Search));
            }

            int totalRecords = query.Count();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)model.PageSize);

            var paginatedFoods = await query
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToListAsync();

            return new PagedResult<FoodList>
            {
                Items = paginatedFoods,
                TotalPages = totalPages,
                CurrentPage = model.Page
            };
        }

        public async Task<string> UnblockFood(int userId, int foodId)
        {
            var foodSelection = await _context.FoodSelections
                .FirstOrDefaultAsync(fs => fs.UserId == userId && fs.FoodListId == foodId);

            if (foodSelection == null) return "Not found";

            foodSelection.IsBlock = false;
            _context.Update(foodSelection);
            await _context.SaveChangesAsync();

            return "Success";
        }
    }
}