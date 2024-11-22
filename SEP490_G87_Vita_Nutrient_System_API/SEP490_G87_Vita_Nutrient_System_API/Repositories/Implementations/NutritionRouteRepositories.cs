using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class NutritionRouteRepositories : INutritionRouteRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();


        public async Task<IEnumerable<UserDTO>> GetAllUsersByCreateIdAsync(int nutritionistId)
        {
            return await _context.UserListManagements
                .Where(route => route.NutritionistId == nutritionistId)
                .Select(route => route.User) // Chỉ lấy thông tin người dùng
                .Distinct() // Loại bỏ trùng lặp nếu một người dùng thuộc nhiều lộ trình
                .Select(user => new UserDTO
                {
                    UserId = user.UserId,
                    UrlImage = user.Urlimage,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FirstName + " " + user.LastName,
                    Address = user.Address,
                    Phone = user.Phone,
                    Height = user.UserDetail.Height,
                    Weight = user.UserDetail.Weight,
                    Age = user.Dob.HasValue ? (short)(DateTime.Now.Year - user.Dob.Value.Year - (DateTime.Now.DayOfYear < user.Dob.Value.DayOfYear ? 1 : 0)) : (short?)null,
                    /*UnderlyingDisease = user.UserDetail.UnderlyingDisease,*/
                    UnderlyingDiseaseNames = _context.ListOfDiseases
                    .Where(disease => user.UserDetail.UnderlyingDisease != null &&
                                      user.UserDetail.UnderlyingDisease.Contains(disease.Id.ToString()))
                    .Select(disease => disease.Name)
                    .ToList()
                }).ToListAsync();
        }



        // Get nutrition route by NutritionistId and UserId
        public async Task<IEnumerable<UserListManagementDTO>> GetNutritionRoutesByNutritionistIdAndUserIdAsync(int nutritionistId, int userId)
        {
            return await _context.UserListManagements
                .Where(route => route.NutritionistId == nutritionistId && route.UserId == userId) // Lọc theo NutritionistId và userId
                .Include(route => route.Nutritionist)
                .Include(route => route.User)
                .Select(route => new UserListManagementDTO
                {
                    Id = route.Id,
                    UserId = route.UserId,
                    NutritionistId = route.NutritionistId,
                    UserName = route.User.FirstName + " " + route.User.LastName,
                    NutritionistName = route.Nutritionist.FirstName + " " + route.Nutritionist.LastName,
                    Describe = route.Describe,
                    StartDate = route.StartDate,
                    EndDate = route.EndDate,
                    IsDone = route.IsDone,
                    UrlImage = route.User.Urlimage
                }).ToListAsync();
        }



        // Get nutrition route by ID
        public async Task<UserListManagementDTO> GetNutritionRouteByIdAsync(int id)
        {
            var route = await _context.UserListManagements
                .Include(route => route.Nutritionist)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (route == null) return null;

            return new UserListManagementDTO
            {
                Id = route.Id,
                UserId = route.UserId,
                NutritionistId = route.NutritionistId,
                Describe = route.Describe,
                StartDate = route.StartDate,
                EndDate = route.EndDate,
                IsDone = route.IsDone,
                NutritionistName = route.Nutritionist.FirstName + " " + route.Nutritionist.LastName,
                UserName = route.User.FirstName + " " + route.User.LastName

            };
        }


        // Create a new nutrition route
        public async Task<bool> CreateNutritionRouteAsync(UserListManagementDTO userListManagementDto)
        {
            // Kiểm tra người dùng với UserId
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userListManagementDto.UserId);
            if (user == null)
            {
                return false; // Người dùng không tồn tại
            }

            // Create new NutritionRoute entity
            var route = new UserListManagement
            {
                UserId = userListManagementDto.UserId, // ID của người dùng được truyền vào
                NutritionistId = userListManagementDto.NutritionistId,
                Describe = userListManagementDto.Describe,
                StartDate = userListManagementDto.StartDate,
                EndDate = userListManagementDto.EndDate,
                IsDone = userListManagementDto.IsDone
            };

            _context.UserListManagements.Add(route);
            await _context.SaveChangesAsync();
            return true;
        }


        // Update an existing nutrition route
        public async Task UpdateNutritionRouteAsync(UserListManagementDTO userListManagementDto)
        {
            var route = await _context.UserListManagements
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == userListManagementDto.Id);

            if (route == null) return;

            // Cập nhật các thuộc tính có thể sửa đổi
            route.Describe = userListManagementDto.Describe;
            route.StartDate = userListManagementDto.StartDate;
            route.EndDate = userListManagementDto.EndDate;
            route.IsDone = userListManagementDto.IsDone;

            // Thiết lập UserName dựa trên thông tin từ cơ sở dữ liệu, không cho phép người dùng sửa
            userListManagementDto.UserName = route.User.FirstName + " " + route.User.LastName;

            _context.UserListManagements.Update(route);
            await _context.SaveChangesAsync();
        }


        // Delete a nutrition route
        public async Task DeleteNutritionRouteAsync(int id)
        {
            var route = await _context.UserListManagements
                .FirstOrDefaultAsync(r => r.Id == id);

            if (route == null) return;

            // Xóa NutritionRoute
            _context.UserListManagements.Remove(route);
            await _context.SaveChangesAsync();
        }

        /*public async Task<IEnumerable<UserListManagementDTO>> GetUsersWithUnfinishedRoutesAsync(int nutritionistId, int userId)
        {
            return await _context.UserListManagements
               .Where(route => route.NutritionistId == nutritionistId && route.UserId == userId &&route.IsDone == false) // Lọc theo NutritionistId và userId
               .Include(route => route.Nutritionist)
               .Include(route => route.User)
               .Select(route => new UserListManagementDTO
               {
                   Id = route.Id,
                   UserId = route.UserId,
                   NutritionistId = route.NutritionistId,
                   UserName = route.User.FirstName + " " + route.User.LastName,
                   NutritionistName = route.Nutritionist.FirstName + " " + route.Nutritionist.LastName,
                   Describe = route.Describe,
                   StartDate = route.StartDate,
                   EndDate = route.EndDate,
                   IsDone = route.IsDone,
                   UrlImage = route.User.Urlimage
               }).ToListAsync();
        }*/
        public async Task<bool> HasUnfinishedRouteAsync(int nutritionistId , int userId)
        {
            // Kiểm tra xem có lộ trình nào chưa hoàn thành cho người dùng này không
            return await _context.UserListManagements
                .AnyAsync(route => route.UserId == userId && route.NutritionistId == nutritionistId && route.IsDone == false);
        }

    }

}
