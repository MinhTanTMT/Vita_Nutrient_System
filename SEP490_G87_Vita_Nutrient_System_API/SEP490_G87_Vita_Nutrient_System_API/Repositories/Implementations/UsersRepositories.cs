
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
using System.Security.Cryptography;
using System.Text;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class UsersRepositories : IUserRepositories
    {
        private readonly string EncryptionKey = "StrongKey16Chars";
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


        public async Task<string> EncryptPassword(string password)
        {
            // Kiểm tra nếu mật khẩu không rỗng hoặc null
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            // Hash mật khẩu bằng bcrypt (bcrypt sẽ tự động sinh salt)
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Trả về hash của mật khẩu
            return hashedPassword;
        }

        public async Task<bool> VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            // So sánh mật khẩu người dùng nhập với mật khẩu đã hash lưu trữ
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
        }


        //public async Task<string> EncryptPassword(string password)
        //{
        //if (string.IsNullOrEmpty(password))
        //    throw new ArgumentException("Password cannot be null or empty", nameof(password));

        //using (Aes aes = Aes.Create())
        //{
        //    aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);

        //    // Tạo IV động
        //    aes.GenerateIV();
        //    byte[] iv = aes.IV;

        //    using (var encryptor = aes.CreateEncryptor(aes.Key, iv))
        //    using (var ms = new MemoryStream())
        //    {
        //        // Lưu IV vào đầu dữ liệu
        //        ms.Write(iv, 0, iv.Length);

        //        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        //        using (var writer = new StreamWriter(cs))
        //        {
        //            writer.Write(password);
        //        }

        //        return Convert.ToBase64String(ms.ToArray());
        //    }
        //}

        //    return password;
        //}

        /// <summary>
        /// Giải mã mật khẩu.
        /// </summary>
        public async Task<string> DecryptPassword(string encryptedPassword)
        {
            //if (string.IsNullOrEmpty(encryptedPassword))
            //    throw new ArgumentException("Encrypted password cannot be null or empty", nameof(encryptedPassword));

            //byte[] cipherBytes = Convert.FromBase64String(encryptedPassword);

            //using (Aes aes = Aes.Create())
            //{
            //    aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);

            //    // Đọc IV từ đầu dữ liệu mã hóa
            //    byte[] iv = new byte[16];
            //    Array.Copy(cipherBytes, 0, iv, 0, iv.Length);
            //    aes.IV = iv;

            //    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            //    using (var ms = new MemoryStream(cipherBytes, iv.Length, cipherBytes.Length - iv.Length))
            //    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            //    using (var reader = new StreamReader(cs))
            //    {
            //        return reader.ReadToEnd();
            //    }
            //}

            return encryptedPassword; // luc lam moi database them sau
        }


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


        public async Task<string> GeneratePassword(int length, bool includeUppercase = true, bool includeLowercase = true, bool includeNumbers = true, bool includeSpecialChars = false)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Password length must be greater than 0.");
            }

            // Các bộ ký tự có thể sử dụng
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string numberChars = "0123456789";
            const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?/";

            // Chuỗi ký tự được chọn để tạo mật khẩu
            string characterPool = "";

            if (includeUppercase)
            {
                characterPool += uppercaseChars;
            }

            if (includeLowercase)
            {
                characterPool += lowercaseChars;
            }

            if (includeNumbers)
            {
                characterPool += numberChars;
            }

            if (includeSpecialChars)
            {
                characterPool += specialChars;
            }

            if (string.IsNullOrEmpty(characterPool))
            {
                throw new ArgumentException("At least one character type must be selected.");
            }

            // Tạo mật khẩu
            var random = new Random();
            var passwordBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(characterPool.Length);
                passwordBuilder.Append(characterPool[randomIndex]);
            }

            return passwordBuilder.ToString();
        }


        public async Task<bool> ForgotPassword(string emailGoogle)
        {
            try
            {
                var inforAccount = await _context.Users.FirstOrDefaultAsync(x => x.AccountGoogle.Equals(emailGoogle));
                if (inforAccount != null)
                {
                    string newPass = await GeneratePassword(12);
                    inforAccount.Password = await EncryptPassword(newPass);
                    string? Passwork = $"Mật khẩu hiện tại của bạn: {newPass}";
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

        public async Task<UserLoginRegister> GetUserLogin(string account, string password)
        {
            if (_context.Users == null)
            {
                return null;
            }

            await modifyPremiumAccount(account, null);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Account.Equals(account) && x.IsActive == true);

            if (user == null)
            {
                return null;
            }
            else if (await VerifyPassword(password, user.Password))
            {
                var getDataRole = await _context.Roles.FindAsync(user.Role);

                if (getDataRole != null)
                {
                    UserLoginRegister UserLogin = new UserLoginRegister()
                    {
                        FullName = user.FirstName + " " + user.LastName,
                        Urlimage = user.Urlimage,
                        Account = user.Account,
                        RoleName = getDataRole.RoleName,
                        UserId = user.UserId
                    };
                    return UserLogin;
                }
            }
            return null;

        }

        public async Task<bool> CheckAccountUserNull(string account, string accGoogle)
        {
            if (_context.Users == null)
            {
                return false;
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Account.Equals(account) || u.AccountGoogle.Equals(accGoogle));
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<UserLoginRegister> GetUserRegister(UserLoginRegister user)
        {
            User modifiUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Account = user.Account,
                AccountGoogle = user.AccountGoogle,
                Password =  await EncryptPassword(user.Password),
                IsActive = true
            };

            await _context.Users.AddAsync(modifiUser);
            await _context.SaveChangesAsync();
            user.UserId = modifiUser.UserId;
            return user;
        }


        public async Task<bool> modifyPremiumAccount(string? Account, string? AccountGoogle)
        {
            short roleUser = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<short>("roleUser");
            short roleUserPremium = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<short>("roleUserPremium");

            if (AccountGoogle != null)
            {
                var accGoogle = await _context.Users.FirstOrDefaultAsync(x => x.AccountGoogle.Equals(AccountGoogle));
                if (accGoogle != null && accGoogle.Role == roleUserPremium)
                {
                    var data = await _context.UserListManagements.FirstOrDefaultAsync(x =>
                    x.UserId == accGoogle.UserId
                    && x.StartDate <= DateTime.Now
                    && x.EndDate >= DateTime.Now && x.IsDone == false);

                    if (data == null)
                    {
                        accGoogle.Role = roleUser;
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
            }
            else
            {
                var accUser = await _context.Users.FirstOrDefaultAsync(x => x.Account.Equals(Account));
                if (accUser != null && accUser.Role == roleUserPremium)
                {
                    var data = await _context.UserListManagements.FirstOrDefaultAsync(x =>
                    x.UserId == accUser.UserId
                    && x.StartDate <= DateTime.Now
                    && x.EndDate >= DateTime.Now && x.IsDone == false);

                    if (data == null)
                    {
                        accUser.Role = roleUser;
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
            }
            return true;
        }


        public async Task<UserLoginRegister> GetRegisterLoginGoogle(UserLoginRegister user)
        {
            await modifyPremiumAccount(null, user.AccountGoogle);
            var accGoogle = await _context.Users.FirstOrDefaultAsync(x => x.AccountGoogle.Equals(user.AccountGoogle));

            if (accGoogle == null)
            {

                User modifiUser = new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role,
                    Account = user.Account,
                    AccountGoogle = user.AccountGoogle,
                    Password = await EncryptPassword(user.Password),
                    IsActive = true
                };

                await _context.Users.AddAsync(modifiUser);
                await _context.SaveChangesAsync();

                var getDataRole = await _context.Roles.FindAsync(modifiUser.Role);
                if (getDataRole != null)
                {
                    UserLoginRegister UserLogin = new UserLoginRegister()
                    {
                        FullName = modifiUser.FirstName + " " + modifiUser.LastName,
                        Urlimage = modifiUser.Urlimage,
                        Account = modifiUser.Account,
                        RoleName = getDataRole.RoleName,
                        UserId = modifiUser.UserId
                    };
                    return UserLogin;
                }
            }
            else if (accGoogle != null && accGoogle.IsActive == true)
            {
                var getDataRole = await _context.Roles.FindAsync(accGoogle.Role);

                if (getDataRole != null)
                {
                    UserLoginRegister UserLogin = new UserLoginRegister()
                    {
                        FullName = accGoogle.FirstName + " " + accGoogle.LastName,
                        Urlimage = accGoogle.Urlimage,
                        Account = accGoogle.Account,
                        RoleName = getDataRole.RoleName,
                        UserId = accGoogle.UserId
                    };
                    return UserLogin;
                }

            }
            return null;
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

        public ExpertPackage GetNutritionistPackages(short id)
        {
            return _context.ExpertPackages.Find(id);
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
                return null;
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

        public async Task<PagedResult<FoodList>> ListCollectionFood(int userId, GetLikeFoodDTO model)
        {
            var query = _context.FoodSelections
                .Where(fs => fs.UserId == userId && (bool)fs.IsCollection)
                .Join(_context.FoodLists, fs => fs.FoodListId, f => f.FoodListId, (fs, f) => f);

            if (query == null)
            {
                return null;
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

        public async Task<string> SaveCollection(int userId, int foodId)
        {
            var foodSelection = await _context.FoodSelections
                .FirstOrDefaultAsync(fs => fs.UserId == userId && fs.FoodListId == foodId);

            if (foodSelection == null) return "Not found";

            foodSelection.IsCollection = !foodSelection.IsCollection;
            _context.Update(foodSelection);
            await _context.SaveChangesAsync();

            return "Success";
        }

    }
}