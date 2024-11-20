using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Dtos.Disease;
using SEP490_G87_Vita_Nutrient_System_API.Dtos.Food;
using SEP490_G87_Vita_Nutrient_System_API.Dtos.FoodAndDisease;
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

        public async Task<List<GetFoodListDTO>> GetFoodLists(string search)
        {
            return await _context.FoodLists.Include(ft => ft.FoodType).Include(kn => kn.KeyNote).Include(cd => cd.CookingDifficulty)
            .Where(f => (string.IsNullOrEmpty(search) || f.Name.Contains(search)) && f.IsActive == true).Select(fl => new GetFoodListDTO
            {
                FoodListId = fl.FoodListId,
                Name = fl.Name,
                KeyNoteId = fl.KeyNoteId,
                FoodTypeId = fl.FoodTypeId,
                CookingDifficultyId = fl.CookingDifficultyId,
                Describe = fl.Describe,
                Rate = fl.Rate,
                NumberRate = fl.NumberRate,
                Urlimage = fl.Urlimage,
                FoodTypeName = fl.FoodType.Name,
                KeyNoteName = fl.KeyNote.KeyList,
                IsActive = fl.IsActive,
                PreparationTime = fl.PreparationTime,
                CookingTime = fl.CookingTime,
                CookingDifficultyName = fl.CookingDifficulty.Name
            })
            .ToListAsync();
        }

        public async Task<GetFoodListDTO> GetFoodList(int id)
        {
            var foodList = await _context.FoodLists.Include(ft => ft.FoodType).Include(kn => kn.KeyNote).Include(cd => cd.CookingDifficulty).Select(fl => new GetFoodListDTO
            {
                FoodListId = fl.FoodListId,
                Name = fl.Name,
                Describe = fl.Describe,
                Rate = fl.Rate,
                NumberRate = fl.NumberRate,
                Urlimage = fl.Urlimage,
                FoodTypeName = fl.FoodType.Name,
                KeyNoteName = fl.KeyNote.KeyList,
                IsActive = fl.IsActive,
                PreparationTime = fl.PreparationTime,
                CookingTime = fl.CookingTime,
                CookingDifficultyName = fl.CookingDifficulty.Name
            }).FirstOrDefaultAsync(t => t.FoodListId == id);

            if (foodList == null)
            {
                return null;
            }

            return foodList;
        }

        public async Task<FoodList> SaveFoodList(SaveFoodDTO model)
        {
            FoodList food;

            if (model.FoodListId == 0)
            {
                food = new FoodList
                {
                    Name = model.Name,
                    Describe = model.Describe,
                    Rate = model.Rate,
                    NumberRate = model.NumberRate,
                    Urlimage = model.Urlimage,
                    FoodTypeId = model.FoodTypeId,
                    KeyNoteId = model.KeyNoteId,
                    IsActive = true,
                    PreparationTime = model.PreparationTime,
                    CookingTime = model.CookingTime,
                    CookingDifficultyId = model.CookingDifficultyId
                };
                await _context.AddAsync(food);
            }
            else
            {
                food = await _context.FoodLists.FirstOrDefaultAsync(t => t.FoodListId == model.FoodListId);
                if (food == null)
                {
                    return null;
                }

                // Cập nhật từng thuộc tính của food từ model
                food.Name = model.Name;
                food.Describe = model.Describe;
                food.Rate = model.Rate;
                food.NumberRate = model.NumberRate;
                food.Urlimage = model.Urlimage;
                food.FoodTypeId = model.FoodTypeId;
                food.KeyNoteId = model.KeyNoteId;
                food.IsActive = model.IsActive;
                food.PreparationTime = model.PreparationTime;
                food.CookingTime = model.CookingTime;
                food.CookingDifficultyId = model.CookingDifficultyId;

                _context.FoodLists.Update(food);
            }

            await _context.SaveChangesAsync();
            return food;
        }

        public async Task<int> DeleteFoodList(int id)
        {
            var foodList = await _context.FoodLists.FirstOrDefaultAsync(t => t.FoodListId == id);
            if (foodList == null)
            {
                return 0;
            }

            foodList.IsActive = false;
            await _context.SaveChangesAsync();

            return foodList.FoodListId;
        }

        public async Task<List<ListOfDisease>> GetDiseases(string search)
        {
            return await _context.ListOfDiseases
            .Where(d => string.IsNullOrEmpty(search) || d.Name.Contains(search))
            .ToListAsync();
        }

        public async Task<ListOfDisease> GetDiseases(int id)
        {
            var disease = await _context.ListOfDiseases.FirstOrDefaultAsync(t => t.Id == id);

            if (disease == null)
            {
                return null;
            }

            return disease;
        }

        public async Task<ListOfDisease> SaveDisease(SaveDiseaseDTO model)
        {
            ListOfDisease disease;

            if (model.Id == 0)
            {
                disease = new ListOfDisease
                {
                    Name = model.Name,
                    Describe = model.Describe
                };
                await _context.AddAsync(disease);
            }
            else
            {
                disease = await _context.ListOfDiseases.FirstOrDefaultAsync(t => t.Id == model.Id);
                if (disease == null)
                {
                    return null;
                }

                disease.Name = model.Name;
                disease.Describe = model.Describe;
            }

            await _context.SaveChangesAsync();
            return disease;
        }


        public async Task<ListFoodAndDiseaseDTO> GetFoodAndDiseases(int foodId, int diseaseId)
        {
            var foodAndDisease = await _context.FoodAndDiseases
            .Include(f => f.FoodList)
            .Include(d => d.ListOfDiseases).Select(fd => new ListFoodAndDiseaseDTO
            {
                FoodListId = fd.FoodListId,
                ListOfDiseasesId = fd.ListOfDiseasesId,
                DiseaseName = fd.ListOfDiseases.Name,
                DiseaseDescribe = fd.ListOfDiseases.Describe,
                FoodName = fd.FoodList.Name,
                FoodDescribe = fd.FoodList.Describe,
                Describe = fd.Describe,
                IsGoodOrBad = fd.IsGoodOrBad
            }).Where(t => _context.FoodLists.FirstOrDefault(b => b.IsActive == true && b.FoodListId == t.FoodListId) != null)
            .FirstOrDefaultAsync(fd => fd.FoodListId == foodId && fd.ListOfDiseasesId == diseaseId);

            return foodAndDisease;
        }

        public async Task<List<ListFoodAndDiseaseDTO>> GetFoodAndDiseases()
        {
            return await _context.FoodAndDiseases.Include(f => f.FoodList).Include(d => d.ListOfDiseases).Select(fd => new ListFoodAndDiseaseDTO
            {
                FoodListId = fd.FoodListId,
                ListOfDiseasesId = fd.ListOfDiseasesId,
                DiseaseName = fd.ListOfDiseases.Name,
                DiseaseDescribe = fd.ListOfDiseases.Describe,
                FoodName = fd.FoodList.Name,
                FoodDescribe = fd.FoodList.Describe,
                Describe = fd.Describe,
                IsGoodOrBad = fd.IsGoodOrBad
            }).Where(t => _context.FoodLists.FirstOrDefault(b => b.IsActive == true && b.FoodListId == t.FoodListId) != null).ToListAsync();
        }

        public async Task<FoodAndDisease> SaveFoodAndDiseases(SaveFoodAndDiseaseDTO model)
        {
            var check = await _context.FoodAndDiseases.FirstOrDefaultAsync(t => t.ListOfDiseasesId == model.ListOfDiseasesId && t.FoodListId == model.FoodListId);
            if (check != null)
            {
                check.ListOfDiseasesId = model.ListOfDiseasesId;
                check.FoodListId = model.FoodListId;
                check.Describe = model.Describe;
                check.IsGoodOrBad = model.IsGoodOrBad;

                _context.FoodAndDiseases.Update(check);
            }
            else
            {
                check = new FoodAndDisease()
                {
                    ListOfDiseasesId = model.ListOfDiseasesId,
                    FoodListId = model.FoodListId,
                    Describe = model.Describe,
                    IsGoodOrBad = model.IsGoodOrBad
                };
                await _context.AddAsync(check);
            }
            await _context.SaveChangesAsync();

            return check;
        }

        public async Task<int> DeleteFoodAndDisease(int foodId, int diseaseId)
        {
            var foodAndDisease = await _context.FoodAndDiseases
            .FirstOrDefaultAsync(fd => fd.FoodListId == foodId && fd.ListOfDiseasesId == diseaseId);

            if (foodAndDisease == null)
            {
                return 0;
            }

            _context.FoodAndDiseases.Remove(foodAndDisease);
            await _context.SaveChangesAsync();

            return foodAndDisease.ListOfDiseasesId;
        }
    }
}
