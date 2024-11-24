using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.PageResult;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using System;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class NutritionRepositories : INutritionRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        public NutritionRepositories()
        {
        }

        public async Task<UserDetail?> GetUserDetailsAsync(int userId)
        {
            return await _context.UserDetails.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<bool> SaveOrUpdateUserDetailsAsync(UserDetailsDTO userDetails)
        {
            var existingRecord = await _context.UserDetails
                .FirstOrDefaultAsync(u => u.UserId == userDetails.UserId);

            if (existingRecord != null)
            {
                existingRecord.DescribeYourself = userDetails.DescribeYourself;
                existingRecord.Height = userDetails.Height;
                existingRecord.Weight = userDetails.Weight;
                existingRecord.Age = userDetails.Age;
                existingRecord.WantImprove = userDetails.WantImprove;
                existingRecord.UnderlyingDisease = userDetails.UnderlyingDisease;
                existingRecord.InforConfirmGood = userDetails.InforConfirmGood;
                existingRecord.InforConfirmBad = userDetails.InforConfirmBad;
                existingRecord.IsPremium = userDetails.IsPremium;

                _context.UserDetails.Update(existingRecord);
            }
            else
            {
                var userEntity = new UserDetail
                {
                    UserId = userDetails.UserId,
                    DescribeYourself = userDetails.DescribeYourself,
                    Height = userDetails.Height,
                    Weight = userDetails.Weight,
                    Age = userDetails.Age,
                    WantImprove = userDetails.WantImprove,
                    UnderlyingDisease = userDetails.UnderlyingDisease,
                    InforConfirmGood = userDetails.InforConfirmGood,
                    InforConfirmBad = userDetails.InforConfirmBad,
                    IsPremium = userDetails.IsPremium
                };
                await _context.UserDetails.AddAsync(userEntity);
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<NutritionTargetsDaily> CalculateNutritionNeeds(UserDetailsDTO userDetails)
        {
            double bmr;
            var user = await _context.Users.FirstOrDefaultAsync(t => t.UserId == userDetails.UserId);
            if (user.Gender == true)
            {
                bmr = (double)(10 * userDetails.Weight + 6.25 * userDetails.Height - 5 * userDetails.Age + 5);
            }
            else
            {
                bmr = (double)(10 * userDetails.Weight + 6.25 * userDetails.Height - 5 * userDetails.Age - 161);
            }


            double activityFactor;
            switch (userDetails.DescribeYourself)
            {
                case "sedentary": activityFactor = 1.2; break;
                case "light": activityFactor = 1.375; break;
                case "moderate": activityFactor = 1.55; break;
                case "active": activityFactor = 1.725; break;
                case "very active": activityFactor = 1.9; break;
                default: activityFactor = 1.2; break;
            }

            short calories = (short)(bmr * activityFactor);

            short proteinMin = (short)(1.2 * userDetails.Weight);
            short proteinMax = (short)(1.8 * userDetails.Weight);

            short carbsMin = (short)((calories * 0.45) / 4);
            short carbsMax = (short)((calories * 0.65) / 4);

            short fatsMin = (short)((calories * 0.20) / 9);
            short fatsMax = (short)((calories * 0.35) / 9);

            return new NutritionTargetsDaily
            {
                UserId = userDetails.UserId,
                Calories = calories,
                ProteinMin = proteinMin,
                ProteinMax = proteinMax,
                CarbsMin = carbsMin,
                CarbsMax = carbsMax,
                FatsMin = fatsMin,
                FatsMax = fatsMax,
                IsActive = true
            };
        }

        // Sửa hàm này
        public async Task SaveNutritionTargetsAsync(NutritionTargetsDaily nutritionTargets)
        {
        //    bool exerciseIntensityExists = await _context.ExerciseIntensities
        //.AnyAsync(e => e.Id == nutritionTargets.ExerciseIntensityId);
        //    if (!exerciseIntensityExists)
        //    {
        //        throw new Exception("Invalid ExerciseIntensityId. Please ensure that the Exercise Intensity exists.");
        //    }
        //    _context.Add(nutritionTargets);
        //    await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<UserDTO>> GetUsers(int userId, string? search, int page = 1, int pageSize = 10)
        {
            //var nutritionist = await _context.NutritionistDetails.FirstOrDefaultAsync(t => t.Nutritionist.UserId == userId);
            //var query = await _context.Users
            //            .AsNoTracking()
            //            .Where(t => t.NutritionistDetail.NutritionistId == nutritionist.NutritionistId)
            //            .Select(u => new UserDTO
            //            {
            //                UserId = u.UserId,
            //                UrlImage = u.Urlimage,
            //                FirstName = u.FirstName,
            //                LastName = u.LastName,
            //                UserDetail = u.UserDetail != null ? new UserDetailsDTO
            //                {
            //                    UserId = u.UserDetail.UserId,
            //                    DescribeYourself = u.UserDetail.DescribeYourself,
            //                    Height = u.UserDetail.Height,
            //                    Weight = u.UserDetail.Weight,
            //                    Age = u.UserDetail.Age,
            //                    WantImprove = u.UserDetail.WantImprove,
            //                    UnderlyingDisease = u.UserDetail.UnderlyingDisease,
            //                    InforConfirmGood = u.UserDetail.InforConfirmGood,
            //                    InforConfirmBad = u.UserDetail.InforConfirmBad,
            //                    IsPremium = u.UserDetail.IsPremium
            //                } : null
            //            }).ToListAsync();

            //var userListManagement = await _context.UserListManagements.Where(t => t.EndDate > DateTime.UtcNow).ToListAsync();

            //foreach (var item in userListManagement)
            //{
            //    var userDel = await _context.Users.FirstOrDefaultAsync(t => t.UserId == item.UserId);
            //    if (userDel != null)
            //    {
            //        _context.Remove(userDel);
            //    }
            //}

            //await _context.SaveChangesAsync();

            //if (!string.IsNullOrEmpty(search))
            //{
            //    query = query.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search)).ToList();
            //}

            //int totalRecords = query.Count();
            //int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            //var paginatedUsers = query
            //    .Skip((page - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToList();

            return new PagedResult<UserDTO>
            {
                //Items = paginatedUsers,
                //TotalPages = totalPages,
                //CurrentPage = page
            };
        }

        public async Task<UserDTO> GetUserDetail(int userId)
        {
            var user = await _context.Users
                        .AsNoTracking()
                        .Where(t => t.UserId == userId)
                        .Select(u => new UserDTO
                        {
                            UserId = u.UserId,
                            UrlImage = u.Urlimage,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            UserDetail = u.UserDetail != null ? new UserDetailsDTO
                            {
                                UserId = u.UserDetail.UserId,
                                DescribeYourself = u.UserDetail.DescribeYourself,
                                Height = u.UserDetail.Height,
                                Weight = u.UserDetail.Weight,
                                Age = u.UserDetail.Age,
                                WantImprove = u.UserDetail.WantImprove,
                                UnderlyingDisease = u.UserDetail.UnderlyingDisease,
                                InforConfirmGood = u.UserDetail.InforConfirmGood,
                                InforConfirmBad = u.UserDetail.InforConfirmBad,
                                IsPremium = u.UserDetail.IsPremium
                            } : null
                        })
                        .FirstOrDefaultAsync(); // Đặt FirstOrDefaultAsync ở cuối truy vấn

            if (user == null)
            {
                throw new ApplicationException("Không tìm thấy!");
            }

            return user;
        }

        public async Task<int> DeleteUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.UserId == userId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<bool> UpdateUser(int userId, string inforConfirmBad, string inforConfirmGood)
        {
            var user = await _context.UserDetails.FirstOrDefaultAsync(t => t.UserId == userId);

            if (user == null) return false;

            user.InforConfirmBad = inforConfirmBad;
            user.InforConfirmGood = inforConfirmGood;

            _context.SaveChanges();

            return true;
        }

        public async Task<bool> CheckFoodDiseaseRelation(int diseaseId, int foodId)
        {
            var relation = _context.FoodAndDiseases
                .FirstOrDefault(fd => fd.ListOfDiseasesId == diseaseId && fd.FoodListId == foodId);

            if (relation == null) return false;

            if (relation.IsGoodOrBad == null)
            {
                relation.IsGoodOrBad = false;
            }

            return (bool)relation.IsGoodOrBad;
        }
    }

}
