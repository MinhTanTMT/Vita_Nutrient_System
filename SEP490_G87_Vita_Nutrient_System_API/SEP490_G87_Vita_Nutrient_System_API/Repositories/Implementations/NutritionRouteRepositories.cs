using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class NutritionRouteRepositories : INutritionRouteRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();


        public async Task<IEnumerable<UserDTO>> GetAllUsersByCreateIdAsync(int createById)
        {
            return await _context.NutritionRoutes
                .Where(route => route.CreateById == createById)
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
                    Age = user.UserDetail.Age
                }).ToListAsync();
        }


        // Get nutrition route by CreateById and UserId
        public async Task<IEnumerable<NutritionRouteDTO>> GetNutritionRoutesByCreateByIdAndUserIdAsync(int createById, int userId)
        {
            return await _context.NutritionRoutes
                .Where(route => route.CreateById == createById && route.UserId == userId) // Lọc theo createById và userId
                .Include(route => route.CreateBy)
                .Include(route => route.User)
                .Select(route => new NutritionRouteDTO
                {
                    Id = route.Id,
                    UserId = route.UserId,
                    CreateById = route.CreateById,
                    FullName = route.User.FirstName + " " + route.User.LastName,
                    CreateByName = route.CreateBy.FirstName + " " + route.CreateBy.LastName,
                    Name = route.Name,
                    Describe = route.Describe,
                    StartDate = route.StartDate,
                    EndDate = route.EndDate,
                    IsDone = route.IsDone,
                    UrlImage = route.User.Urlimage
                }).ToListAsync();
        }



        // Get nutrition route by ID
        public async Task<NutritionRouteDTO> GetNutritionRouteByIdAsync(int id)
        {
            var route = await _context.NutritionRoutes
                .Include(r => r.User) // Bao gồm thông tin của User
                .FirstOrDefaultAsync(r => r.Id == id);

            if (route == null) return null;

            return new NutritionRouteDTO
            {
                Id = route.Id,
                UserId = route.UserId,
                CreateById = route.CreateById,
                Name = route.Name,
                Describe = route.Describe,
                StartDate = route.StartDate,
                EndDate = route.EndDate,
                IsDone = route.IsDone,
                FullName = route.User.FirstName + " " + route.User.LastName
            };
        }


        // Create a new nutrition route
        public async Task<bool> CreateNutritionRouteAsync(NutritionRouteDTO nutritionRouteDto)
        {
            // Kiểm tra người dùng với UserId
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == nutritionRouteDto.UserId);
            if (user == null)
            {
                return false; // Người dùng không tồn tại
            }

            // Create new NutritionRoute entity
            var route = new NutritionRoute
            {
                UserId = nutritionRouteDto.UserId, // ID của người dùng được truyền vào
                CreateById = nutritionRouteDto.CreateById,
                Name = nutritionRouteDto.Name,
                Describe = nutritionRouteDto.Describe,
                StartDate = nutritionRouteDto.StartDate,
                EndDate = nutritionRouteDto.EndDate,
                IsDone = nutritionRouteDto.IsDone
            };

            _context.NutritionRoutes.Add(route);
            await _context.SaveChangesAsync();
            return true;
        }


        // Update an existing nutrition route
        public async Task UpdateNutritionRouteAsync(NutritionRouteDTO nutritionRouteDto)
        {
            var route = await _context.NutritionRoutes
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == nutritionRouteDto.Id);

            if (route == null) return;

            // Cập nhật các thuộc tính có thể sửa đổi
            route.Name = nutritionRouteDto.Name;
            route.Describe = nutritionRouteDto.Describe;
            route.StartDate = nutritionRouteDto.StartDate;
            route.EndDate = nutritionRouteDto.EndDate;
            route.IsDone = nutritionRouteDto.IsDone;

            // Thiết lập UserName dựa trên thông tin từ cơ sở dữ liệu, không cho phép người dùng sửa
            nutritionRouteDto.FullName = route.User.FirstName + " " + route.User.LastName;

            _context.NutritionRoutes.Update(route);
            await _context.SaveChangesAsync();
        }


        // Delete a nutrition route
        public async Task DeleteNutritionRouteAsync(int id)
        {
            var route = await _context.NutritionRoutes.FindAsync(id);
            if (route == null) return;

            _context.NutritionRoutes.Remove(route);
            await _context.SaveChangesAsync();
        }
    }
}
