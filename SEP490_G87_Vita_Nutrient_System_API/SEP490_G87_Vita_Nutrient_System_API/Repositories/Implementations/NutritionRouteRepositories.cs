using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class NutritionRouteRepositories : INutritionRouteRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();


        public async Task<IEnumerable<UserDTO>> GetAllPremiumUserAsync(int nutritionistId)
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
        public async Task<IEnumerable<UserListManagementDTO>> GetPremiumUserByNutritionistIdAndUserIdAsync(int nutritionistId, int userId)
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
                    Rate = route.Rate,
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
                StartDate = route.StartDate?.Date,
                EndDate = route.EndDate?.Date,
                IsDone = route.IsDone,
                FullName = route.User.FirstName + " " + route.User.LastName
            };
        }


        public async Task<bool> CreateNutritionRouteAsync(NutritionRouteDTO nutritionRouteDto, int userListManagementId)
        {
            // Kiểm tra UserListManagement (gói đăng ký nâng cao)
            var relatedUserListManagement = await _context.UserListManagements
                .FirstOrDefaultAsync(ul => ul.Id == userListManagementId
                                           && ul.NutritionistId == nutritionRouteDto.CreateById
                                           && ul.UserId == nutritionRouteDto.UserId);

            if (relatedUserListManagement == null)
            {
                return false; // Gói đăng ký không tồn tại
            }

            // Kiểm tra thời gian của lộ trình nằm trong khoảng thời gian gói đăng ký
            if (nutritionRouteDto.StartDate.Value.Date < relatedUserListManagement.StartDate.Value.Date ||
                nutritionRouteDto.EndDate.Value.Date > relatedUserListManagement.EndDate.Value.Date)
            {
                return false; // Thời gian không hợp lệ
            }

            // Tạo mới NutritionRoute
            var route = new NutritionRoute
            {
                UserId = nutritionRouteDto.UserId,
                CreateById = nutritionRouteDto.CreateById,
                Name = nutritionRouteDto.Name,
                Describe = nutritionRouteDto.Describe,
                StartDate = nutritionRouteDto.StartDate,
                EndDate = nutritionRouteDto.EndDate?.Date,
                IsDone = nutritionRouteDto.IsDone
            };

            _context.NutritionRoutes.Add(route);
            await _context.SaveChangesAsync();
            return true;
        }



        // Update an existing nutrition route
        public async Task<bool> UpdateNutritionRouteAsync(NutritionRouteDTO nutritionRouteDto, int userListManagementId)
        {
            // Kiểm tra UserListManagement (gói đăng ký nâng cao)
            var relatedUserListManagement = await _context.UserListManagements
                .FirstOrDefaultAsync(ul => ul.Id == userListManagementId
                                           && ul.NutritionistId == nutritionRouteDto.CreateById
                                           && ul.UserId == nutritionRouteDto.UserId);

            if (relatedUserListManagement == null)
            {
                return false; // Gói đăng ký không tồn tại
            }

            // Kiểm tra thời gian của lộ trình nằm trong khoảng thời gian gói đăng ký
            if (nutritionRouteDto.StartDate.Value.Date < relatedUserListManagement.StartDate.Value.Date ||
                nutritionRouteDto.EndDate.Value.Date > relatedUserListManagement.EndDate.Value.Date)
            {
                return false; // Thời gian không hợp lệ
            }

            // Lấy lộ trình cần cập nhật
            var route = await _context.NutritionRoutes.FirstOrDefaultAsync(r => r.Id == nutritionRouteDto.Id);

            if (route == null)
            {
                return false; // Lộ trình không tồn tại
            }

            // Cập nhật thông tin
            route.Name = nutritionRouteDto.Name;
            route.Describe = nutritionRouteDto.Describe;
            route.StartDate = nutritionRouteDto.StartDate?.Date;
            route.EndDate = nutritionRouteDto.EndDate?.Date;
            route.IsDone = nutritionRouteDto.IsDone;

            _context.NutritionRoutes.Update(route);
            await _context.SaveChangesAsync();
            return true;
        }



        // Delete a nutrition route
        public async Task DeleteNutritionRouteAsync(int id)
        {
            var route = await _context.NutritionRoutes
                .Include(r => r.MealOfTheDays)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (route == null) return;

            // Xóa tất cả các MealOfTheDay liên quan
            _context.MealOfTheDays.RemoveRange(route.MealOfTheDays);

            // Xóa NutritionRoute
            _context.NutritionRoutes.Remove(route);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasUnfinishedRouteAsync(int nutritionistId, int userId, int userListManagementId)
        {
            // Lấy thông tin gói đăng ký
            var userListManagement = await _context.UserListManagements
                .FirstOrDefaultAsync(ul => ul.UserId == userId
                                           && ul.NutritionistId == nutritionistId
                                           && ul.Id == userListManagementId);

            if (userListManagement == null)
            {
                return false; // Không tìm thấy gói đăng ký
            }

            // Kiểm tra tất cả lộ trình thuộc gói đăng ký có trạng thái chưa hoàn thành
            return await _context.NutritionRoutes
                .AnyAsync(route => route.UserId == userId
                                   && route.CreateById == nutritionistId
                                   && route.StartDate >= userListManagement.StartDate
                                   && route.EndDate <= userListManagement.EndDate
                                   && route.IsDone == false);
        }


        public async Task<IEnumerable<NutritionRouteDTO>> GetNutritionRoutesAsync(int nutritionistId, int userId, int userListManagementId)
        {
            // Xác định UserListManagement (gói đăng ký nâng cao)
            var relatedUserListManagement = await _context.UserListManagements
                .FirstOrDefaultAsync(ul => ul.Id == userListManagementId
                                           && ul.NutritionistId == nutritionistId
                                           && ul.UserId == userId);

            if (relatedUserListManagement == null)
            {
                return new List<NutritionRouteDTO>(); // Trả về danh sách rỗng nếu không tìm thấy gói
            }

            // Lọc lộ trình dinh dưỡng thuộc về gói đăng ký nâng cao này
            var routes = await _context.NutritionRoutes
                .Where(route => route.UserId == relatedUserListManagement.UserId
                                && route.CreateById == relatedUserListManagement.NutritionistId
                                && route.StartDate.Value.Date >= relatedUserListManagement.StartDate.Value.Date
                                && route.EndDate.Value.Date <= relatedUserListManagement.EndDate.Value.Date) // Điều kiện thời gian khớp với gói
                .Select(route => new NutritionRouteDTO
                {
                    Id = route.Id,
                    UserId = route.UserId,
                    CreateById = route.CreateById,
                    Name = route.Name,
                    Describe = route.Describe,
                    StartDate = route.StartDate,
                    EndDate = route.EndDate,
                    IsDone = route.IsDone,
                    FullName = route.User.FirstName + " " + route.User.LastName,
                    CreateByName = route.CreateBy.FirstName + " " + route.CreateBy.LastName,
                    UrlImage = route.User.Urlimage
                }).ToListAsync();

            // Kiểm tra ngày hiện tại và cập nhật trạng thái IsDone
            foreach (var route in routes)
            {
                // Nếu ngày hiện tại lớn hơn ngày kết thúc lộ trình, thì đánh dấu là hoàn thành
                if (DateTime.Now > route.EndDate)
                {
                    route.IsDone = true;
                }
                else
                {
                    route.IsDone = false;
                }
            }

            // Sau khi cập nhật trạng thái IsDone, bạn có thể chọn lưu lại vào database nếu cần
            // Nếu muốn lưu các thay đổi vào database, bạn có thể thực hiện thêm các lệnh cập nhật như sau:

            // Để cập nhật các thay đổi vào database, bạn có thể cần lưu lại
            foreach (var route in routes)
            {
                var routeEntity = await _context.NutritionRoutes
                    .FirstOrDefaultAsync(r => r.Id == route.Id);

                if (routeEntity != null)
                {
                    routeEntity.IsDone = route.IsDone;
                    _context.NutritionRoutes.Update(routeEntity);
                }
            }

            // Lưu thay đổi vào database
            await _context.SaveChangesAsync();

            return routes;
        }


        public async Task<IEnumerable<ListOfDiseaseDTO>> GetDiseaseByUserIdAsync(int userId)
        {
            var userDetail = await _context.UserDetails
                .FirstOrDefaultAsync(ud => ud.UserId == userId);

            if (userDetail == null || string.IsNullOrEmpty(userDetail.UnderlyingDisease))
            {
                return new List<ListOfDiseaseDTO>(); // Không có bệnh nền
            }

            var diseaseIds = userDetail.UnderlyingDisease.Split(';')
                .Select(id =>
                {
                    int.TryParse(id, out var parsedId);
                    return parsedId;
                })
                .Where(parsedId => parsedId > 0) // Loại bỏ các giá trị không hợp lệ
                .ToList();

            return await _context.ListOfDiseases
                .Where(d => diseaseIds.Contains(d.Id))
                .Select(d => new ListOfDiseaseDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Describe = d.Describe
                }).ToListAsync();
        }

        public async Task<bool> CreateDiseaseAsync(int userId, int diseaseId)
        {
            // Kiểm tra xem bệnh có tồn tại không
            var disease = await _context.ListOfDiseases.FindAsync(diseaseId);
            if (disease == null)
            {
                return false; // Bệnh không tồn tại
            }

            // Lấy thông tin UserDetail
            var userDetail = await _context.UserDetails.FirstOrDefaultAsync(ud => ud.UserId == userId);
            if (userDetail == null)
            {
                return false; // Không tìm thấy UserDetail
            }

            // Cập nhật danh sách bệnh
            var currentDiseases = string.IsNullOrEmpty(userDetail.UnderlyingDisease)
                ? new List<int>()
                : userDetail.UnderlyingDisease.Split(';').Select(int.Parse).ToList();

            if (currentDiseases.Contains(diseaseId))
            {
                return false; // Bệnh đã tồn tại
            }

            currentDiseases.Add(diseaseId);
            userDetail.UnderlyingDisease = string.Join(";", currentDiseases);

            _context.UserDetails.Update(userDetail);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteDiseaseAsync(int userId, int diseaseId)
        {
            // Lấy thông tin UserDetail
            var userDetail = await _context.UserDetails.FirstOrDefaultAsync(ud => ud.UserId == userId);
            if (userDetail == null || string.IsNullOrEmpty(userDetail.UnderlyingDisease))
            {
                return false; // Không tìm thấy UserDetail hoặc không có bệnh nền
            }

            // Cập nhật danh sách bệnh
            var currentDiseases = userDetail.UnderlyingDisease.Split(';').Select(int.Parse).ToList();
            if (!currentDiseases.Contains(diseaseId))
            {
                return false; // Bệnh không tồn tại trong danh sách
            }

            currentDiseases.Remove(diseaseId);
            userDetail.UnderlyingDisease = currentDiseases.Any()
                ? string.Join(";", currentDiseases)
                : null; // Nếu không còn bệnh nào thì đặt null

            _context.UserDetails.Update(userDetail);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateIsDoneAsync(int createById, int userId)
        {
            // Lấy lộ trình cần cập nhật
            var route = await _context.NutritionRoutes
                .FirstOrDefaultAsync(r => r.CreateById == createById && r.UserId == userId && r.IsDone == false);

            if (route == null)
            {
                return false; // Không tìm thấy lộ trình hoặc tất cả lộ trình đã hoàn thành
            }

            // Cập nhật IsDone
            route.IsDone = true;
            route.EndDate = DateTime.Today;
            _context.NutritionRoutes.Update(route);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> RateNutritionistAsync(int userId, int nutritionistId, int userListManagementId, short rate)
        {
            if (rate < 1 || rate > 5)
            {
                return false; // Điểm đánh giá không hợp lệ
            }

            // Lấy gói đăng ký
            var userList = await _context.UserListManagements
                .FirstOrDefaultAsync(ul => ul.Id == userListManagementId
                                           && ul.UserId == userId
                                           && ul.NutritionistId == nutritionistId);

            if (userList == null)
            {
                return false; // Không tìm thấy gói đăng ký
            }

            // Kiểm tra trạng thái hoàn thành
            if (userList.IsDone != true)
            {
                return false; // Gói chưa hoàn thành, không được đánh giá
            }

            // Kiểm tra thời gian (chỉ trong vòng 7 ngày sau khi hết thời hạn)
            if (userList.EndDate == null || userList.EndDate.Value.AddDays(7) < DateTime.Now)
            {
                return false; // Quá hạn đánh giá
            }

            // Cập nhật điểm đánh giá
            userList.Rate = rate;
            _context.UserListManagements.Update(userList);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<IEnumerable<UserListManagementDTO>> GetRatingsByNutritionistIdAsync(int nutritionistId)
        {
            return await _context.UserListManagements
                .Where(ul => ul.NutritionistId == nutritionistId && ul.Rate.HasValue)
                .Select(ul => new UserListManagementDTO
                {
                    Id = ul.Id,
                    NutritionistId = ul.NutritionistId,
                    UserId = ul.UserId,
                    Rate = ul.Rate,
                    UserName = ul.User.FirstName + " " + ul.User.LastName,
                    StartDate = ul.StartDate,
                    EndDate = ul.EndDate,
                    Describe = ul.Describe
                }).ToListAsync();
        }

        public async Task<IEnumerable<UserListManagementDTO>> GetDetailsAllPremiumUserByUserAsync(int userId)
        {
            return await _context.UserListManagements
                .Where(ul => ul.UserId == userId) // Lọc theo userId
                .Include(ul => ul.Nutritionist)  // Bao gồm thông tin chuyên gia dinh dưỡng
                .Select(ul => new UserListManagementDTO
                {
                    Id = ul.Id,
                    NutritionistId = ul.NutritionistId,
                    UserId = ul.UserId,
                    Describe = ul.Describe,
                    StartDate = ul.StartDate,
                    EndDate = ul.EndDate,
                    Rate = ul.Rate,
                    IsDone = ul.IsDone,
                    UserName = ul.User.FirstName + " " + ul.User.LastName,
                    NutritionistName = ul.Nutritionist.FirstName + " " + ul.Nutritionist.LastName,
                    UrlImage = ul.Nutritionist.Urlimage // Hình ảnh của chuyên gia
                })
                .ToListAsync();
        }


    }

}
