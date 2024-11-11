using Humanizer;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using static SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels.UserDetailResponse;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class MealsRepositories : IMealsRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        public MealsRepositories()
        {
        }

        public async Task<IEnumerable<MealSettingsDto>> GetAllMealSettingsAsync(int userId)
        {
            var mealSettings = await _context.MealSettings
                .Where(m => m.UserId == userId)
                .Include(m => m.MealSettingsDetails)
                .ThenInclude(d => d.DayOfTheWeek)
                .Include(m => m.MealSettingsDetails)
                .ThenInclude(d => d.SlotOfTheDay)
                .Include(m => m.MealSettingsDetails)
                .ThenInclude(d => d.CookingDifficulty)
                .Select(m => new MealSettingsDto
                {
                    Id = m.Id,
                    DayOfTheWeekStartId = m.DayOfTheWeekStartId,
                    SameScheduleEveryDay = m.SameScheduleEveryDay,
                    MealDetails = m.MealSettingsDetails.Select(d => new MealSettingsDetailsDto
                    {
                        Id = d.Id,
                        SlotOfTheDayId = d.SlotOfTheDayId,
                        DayOfTheWeekId = d.DayOfTheWeekId,
                        CookingDifficultyId = d.CookingDifficultyId,
                        Size = d.Size,
                        NutritionFocus = d.NutritionFocus,
                        TypeFavoriteFood = d.TypeFavoriteFood
                    }).ToList()
                })
                .ToListAsync();

            return mealSettings;
        }

        public async Task<MealSettingsDto> GetMealSettingByIdAsync(int mealSettingsId)
        {
            var mealSetting = await _context.MealSettings
                .Where(m => m.Id == mealSettingsId)
                .Include(m => m.MealSettingsDetails)
                .Select(m => new MealSettingsDto
                {
                    Id = m.Id,
                    DayOfTheWeekStartId = m.DayOfTheWeekStartId,
                    SameScheduleEveryDay = m.SameScheduleEveryDay,
                    MealDetails = m.MealSettingsDetails.Select(d => new MealSettingsDetailsDto
                    {
                        Id = d.Id,
                        SlotOfTheDayId = d.SlotOfTheDayId,
                        DayOfTheWeekId = d.DayOfTheWeekId,
                        CookingDifficultyId = d.CookingDifficultyId,
                        Size = d.Size,
                        NutritionFocus = d.NutritionFocus,
                        TypeFavoriteFood = d.TypeFavoriteFood
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return mealSetting ?? throw new KeyNotFoundException("Meal setting not found.");
        }

        public async Task<MealSettingsDto> AddMealSettingAsync(MealSettingsCreateDto newMealSetting)
        {
            var mealSetting = new MealSetting
            {
                UserId = newMealSetting.UserId,
                DayOfTheWeekStartId = newMealSetting.DayOfTheWeekStartId,
                SameScheduleEveryDay = newMealSetting.SameScheduleEveryDay
            };

            _context.MealSettings.Add(mealSetting);
            await _context.SaveChangesAsync();

            foreach (var detail in newMealSetting.MealDetails)
            {
                var mealDetail = new MealSettingsDetail
                {
                    MealSettingsId = mealSetting.Id,
                    SlotOfTheDayId = detail.SlotOfTheDayId,
                    DayOfTheWeekId = detail.DayOfTheWeekId,
                    CookingDifficultyId = detail.CookingDifficultyId,
                    Size = detail.Size,
                    NutritionFocus = detail.NutritionFocus,
                    TypeFavoriteFood = detail.TypeFavoriteFood
                };
                _context.MealSettingsDetails.Add(mealDetail);
            }
            await _context.SaveChangesAsync();

            return await GetMealSettingByIdAsync(mealSetting.Id);
        }

        public async Task<MealSettingsDto> UpdateMealSettingAsync(int id, MealSettingsUpdateDto updatedMealSetting)
        {
            var mealSetting = await _context.MealSettings
                .Include(m => m.MealSettingsDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mealSetting == null) throw new KeyNotFoundException("Meal setting not found.");

            mealSetting.DayOfTheWeekStartId = updatedMealSetting.DayOfTheWeekStartId;
            mealSetting.SameScheduleEveryDay = updatedMealSetting.SameScheduleEveryDay;

            foreach (var detailDto in updatedMealSetting.MealDetails)
            {
                var detail = mealSetting.MealSettingsDetails.FirstOrDefault(d => d.Id == detailDto.Id);
                if (detail != null)
                {
                    detail.SlotOfTheDayId = detailDto.SlotOfTheDayId;
                    detail.DayOfTheWeekId = detailDto.DayOfTheWeekId;
                    detail.CookingDifficultyId = detailDto.CookingDifficultyId;
                    detail.Size = detailDto.Size;
                    detail.NutritionFocus = detailDto.NutritionFocus;
                    detail.TypeFavoriteFood = detailDto.TypeFavoriteFood;
                }
            }
            await _context.SaveChangesAsync();

            return await GetMealSettingByIdAsync(id);
        }

        public async Task DeleteMealSettingAsync(int id)
        {
            var mealSetting = await _context.MealSettings
                .Include(m => m.MealSettingsDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mealSetting == null) throw new KeyNotFoundException("Meal setting not found.");

            _context.MealSettingsDetails.RemoveRange(mealSetting.MealSettingsDetails);
            _context.MealSettings.Remove(mealSetting);

            await _context.SaveChangesAsync();
        }


        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        private void CalculateMacrosAndFiber(NutritionTargetsDaily nutritionTarget, double totalCalories)
        {
            nutritionTarget.CarbsMax = (short)(totalCalories / 4);      // 4 calo mỗi gram cho carbs
            nutritionTarget.ProteinMax = (short)(totalCalories / 4);    // 4 calo mỗi gram cho protein
            nutritionTarget.FatsMax = (short)(totalCalories / 9);       // 9 calo mỗi gram cho chất béo
            nutritionTarget.MinimumFiber = (short)Math.Round((totalCalories / 1000) * 14); // Chất xơ tối thiểu
        }
        private void CalculateMacrosAndFiberForMeal(MealSettingsDetail mealSettingsDetail, NutritionTargetsDaily existingNutritionTarget, double totalCalories)
        {
            var mealSetting = _context.MealSettings.FirstOrDefault(x => x.Id == mealSettingsDetail.MealSettingsId);
            // Chỉ cập nhật mealSettingsDetail nếu giá trị tương ứng giống với NutritionTargetsDaily
                existingNutritionTarget.Calories = (short?)Math.Round(totalCalories);
                mealSettingsDetail.Calories = existingNutritionTarget.Calories;
            // Cập nhật CarbsMax
                existingNutritionTarget.CarbsMax = (short)(totalCalories / 4);
                mealSettingsDetail.CarbsMax = existingNutritionTarget.CarbsMax;
                existingNutritionTarget.ProteinMax = (short)(totalCalories / 4);
                mealSettingsDetail.ProteinMax = existingNutritionTarget.ProteinMax;
            // Cập nhật FatsMax
                existingNutritionTarget.FatsMax = (short)(totalCalories / 9);
                mealSettingsDetail.FatsMax = existingNutritionTarget.FatsMax;
            // Cập nhật MinimumFiber
                existingNutritionTarget.MinimumFiber = (short)Math.Round((totalCalories / 1000) * 14);
                mealSettingsDetail.MinimumFiber = existingNutritionTarget.MinimumFiber;

            // Cập nhật FoodWanId
            if(existingNutritionTarget.FoodTypeIdWant == null)
            {
                existingNutritionTarget.FoodTypeIdWant = mealSetting.FoodTypeIdWant;
            }
        }



        private async Task DistributeCaloriesForSameSlotMealsAsync(int index,short? slotOfTheDayId, MealSettingsDetail mealSettingsDetail, List<MealSettingsDetail> activeMeals, int userId, double totalCaloriesSlot)
         {
            var mealSetting = _context.MealSettings.FirstOrDefault(x => x.Id == mealSettingsDetail.MealSettingsId);
            // Lấy tất cả các bữa có cùng SlotOfTheDayId và thêm bữa mới
            var sameSlotMeals = activeMeals
                .Where(m => m.SlotOfTheDayId == slotOfTheDayId && m.NutritionTargetsDaily.IsActive == true)
                .ToList();
            if(mealSettingsDetail.SlotOfTheDayId == slotOfTheDayId && index==1)
            {
                sameSlotMeals.Add(mealSettingsDetail);
            }


            // Sắp xếp sameSlotMeals theo thứ tự ưu tiên: Bữa lớn, Bữa vừa, Bữa nhỏ
            // Nhóm các bữa theo loại Size (Bữa lớn, Bữa vừa, Bữa nhỏ)
            var groupedMeals = sameSlotMeals
                .GroupBy(m => m.Size)
                .OrderBy(g => g.Key == "Bữa lớn" ? 1 : g.Key == "Bữa vừa" ? 2 : 3)
                .ToList();

            // Xác định phần trăm calo dựa trên số lượng bữa
            double[] caloriePercentagesSlot;
            switch (groupedMeals.Count)
            {
                case 1:
                    caloriePercentagesSlot = new double[] { 1.0 };
                    break;
                case 2:
                    caloriePercentagesSlot = new double[] { 0.7, 0.3 };
                    break;
                case 3:
                    caloriePercentagesSlot = new double[] { 0.5, 0.3, 0.2 };
                    break;
                default:
                    caloriePercentagesSlot = new double[] { 0 };
                    break;
            }

                for (int i = 0; i < groupedMeals.Count; i++)
                {
                    var group = groupedMeals[i];
                    double caloriesForGroup = totalCaloriesSlot * caloriePercentagesSlot[i];

                    // Phân bổ calo đồng đều cho các bữa trong nhóm
                    double caloriesPerMeal = caloriesForGroup / group.Count();
                    foreach (var meal in group)
                    {
                        if (meal.NutritionTargetsDailyId.HasValue) // Kiểm tra nếu đã có NutritionTargetsDailyId
                        {
                            var existingNutritionTarget = await _context.NutritionTargetsDailies
                                .FirstOrDefaultAsync(nt => nt.Id == meal.NutritionTargetsDailyId);

                            if (existingNutritionTarget != null)
                            {
                                existingNutritionTarget.IsActive = true;
                                existingNutritionTarget.Calories = (short?)Math.Round(caloriesPerMeal);
                                CalculateMacrosAndFiberForMeal(meal, existingNutritionTarget, caloriesPerMeal);
                            _context.NutritionTargetsDailies.Update(existingNutritionTarget);
                            }
                        }
                        else
                        {
                            // Tạo mới NutritionTargetsDaily nếu chưa có
                            var newNutritionTarget = new NutritionTargetsDaily
                            {
                                UserId = userId,
                                Calories = (short)Math.Round(caloriesPerMeal),
                                FoodTypeIdWant = mealSetting.FoodTypeIdWant,
                                ExerciseIntensityId = 1,
                                IsActive = true,
                                CarbsMin = 0,
                                LimitDailySodium = false,
                                LimitDailyCholesterol = false,
                                ProteinMin = 0,
                                FatsMin = 0,
                                CarbsMax = (short)(caloriesPerMeal / 4),
                                ProteinMax = (short)(caloriesPerMeal / 4),
                                FatsMax = (short)(caloriesPerMeal / 9),
                                MinimumFiber = (short)Math.Round((caloriesPerMeal / 1000) * 14)
                            };
                            _context.NutritionTargetsDailies.Add(newNutritionTarget);
                            await _context.SaveChangesAsync();
                            meal.NutritionTargetsDailyId = newNutritionTarget.Id;
                            meal.NutritionTargetsDaily = newNutritionTarget;
                            meal.Calories = (short)Math.Round(caloriesPerMeal);
                            meal.CarbsMax = (short)(caloriesPerMeal / 4);      // 4 calo mỗi gram cho carbs
                            meal.ProteinMax = (short)(caloriesPerMeal / 4);    // 4 calo mỗi gram cho protein
                            meal.FatsMax = (short)(caloriesPerMeal / 9);       // 9 calo mỗi gram cho chất béo
                            meal.MinimumFiber = (short)Math.Round((caloriesPerMeal / 1000) * 14); // Chất xơ tối thiểu
                    }
                    }
            }
            await _context.SaveChangesAsync();         
        }

        private async Task DistributeCaloriesForSlots(int index,List<MealSettingsDetail> activeMeals, MealSettingsDetail mealSettingsDetail, int userId, double totalCalories)
         {
            var mealSetting = _context.MealSettings.FirstOrDefault(x => x.Id == mealSettingsDetail.MealSettingsId);
            // Thêm mealSettingsDetail vào danh sách activeMeals
            if(index == 1)
            {
                activeMeals.Add(mealSettingsDetail);
            }
            
            var slotPriority = new List<int> { 2, 4, 1, 5, 3 };
            activeMeals = activeMeals
                .OrderBy(m => slotPriority.IndexOf(m.SlotOfTheDayId ?? 0))
                .ToList();

            // Nhóm các bữa ăn theo SlotOfTheDayId
            var groupedMeals = activeMeals
                .Where(m => m.IsActive == true)
                .GroupBy(m => m.SlotOfTheDayId)
                .OrderBy(g => slotPriority.IndexOf(g.Key ?? 0))
                .ToList();

            // Xác định phần trăm calo dựa trên số lượng nhóm bữa ăn trong ngày
            double[] caloriePercentages;
            switch (groupedMeals.Count)
            {
                case 1:
                    caloriePercentages = new double[] { 1.0 };
                    break;
                case 2:
                    caloriePercentages = new double[] { 0.6, 0.4 };
                    break;
                case 3:
                    caloriePercentages = new double[] { 0.35, 0.35, 0.3 };
                    break;
                case 4:
                    caloriePercentages = new double[] { 0.35, 0.3, 0.2, 0.15 };
                    break;
                case 5:
                    caloriePercentages = new double[] { 0.3, 0.25, 0.2, 0.15, 0.1 };
                    break;
                default:
                    caloriePercentages = new double[] { 0 };
                    break;
            }

            double allocatedCalories = 0;

            for (int i = 0; i < groupedMeals.Count && i < caloriePercentages.Length; i++)
            {
                var slotGroup = groupedMeals[i];
                double caloriesForSlot = totalCalories * caloriePercentages[i];

                // Kiểm tra số lượng bữa trong cùng SlotOfTheDayId
                var sameSlotMeals = slotGroup.ToList();

                // Nếu slot có nhiều bữa, gọi hàm DistributeCaloriesForSameSlotMealsAsync để phân bổ
                if (sameSlotMeals.Count > 1)
                {  if(mealSettingsDetail.IsActive == false || index == 2)
                    {
                        await DistributeCaloriesForSameSlotMealsAsync(2, slotGroup.Key, mealSettingsDetail, sameSlotMeals, userId, caloriesForSlot);
                    }
                   else
                    {
                        await DistributeCaloriesForSameSlotMealsAsync(1, slotGroup.Key, mealSettingsDetail, sameSlotMeals, userId, caloriesForSlot);
                    }
                    
                }
                else
                {
                    // Phân bổ calo cho bữa duy nhất trong slot
                    var meal = sameSlotMeals[0];
                    if (meal.NutritionTargetsDailyId.HasValue) // Kiểm tra nếu đã có NutritionTargetsDailyId
                    {
                        var existingNutritionTarget = await _context.NutritionTargetsDailies
                            .FirstOrDefaultAsync(nt => nt.Id == meal.NutritionTargetsDailyId);

                        if (existingNutritionTarget != null)
                        {
                            existingNutritionTarget.IsActive = true;
                            existingNutritionTarget.Calories = (short)caloriesForSlot;
                            
                            CalculateMacrosAndFiberForMeal(meal, existingNutritionTarget, caloriesForSlot);
                            _context.NutritionTargetsDailies.Update(existingNutritionTarget);
                        }
                    }
                    else
                    {
                        var newNutritionTarget = new NutritionTargetsDaily
                        {
                            UserId = userId,
                            Calories = (short)caloriesForSlot,
                            FoodTypeIdWant =mealSetting.FoodTypeIdWant ,
                            IsActive = true,
                            CarbsMin = 0,
                            ProteinMin = 0,
                            FatsMin = 0,
                            LimitDailySodium = false,
                            LimitDailyCholesterol = false,
                            CarbsMax = (short)(caloriesForSlot / 4),
                            ProteinMax = (short)(caloriesForSlot / 4),
                            FatsMax = (short)(caloriesForSlot / 9),
                            MinimumFiber = (short)Math.Round((caloriesForSlot / 1000) * 14),

                        };
                        _context.NutritionTargetsDailies.Add(newNutritionTarget);
                        meal.NutritionTargetsDailyId = newNutritionTarget.Id;
                        meal.NutritionTargetsDaily = newNutritionTarget;
                        meal.Calories = (short)caloriesForSlot;
                        meal.CarbsMax = (short)(caloriesForSlot / 4);      // 4 calo mỗi gram cho carbs
                        meal.ProteinMax = (short)(caloriesForSlot / 4);    // 4 calo mỗi gram cho protein
                        meal.FatsMax = (short)(caloriesForSlot / 9);       // 9 calo mỗi gram cho chất béo
                        meal.MinimumFiber = (short)Math.Round((caloriesForSlot / 1000) * 14); // Chất xơ tối thiểu
                    }
                }
            }

            await _context.SaveChangesAsync();
        }


        public async Task<MealSettingsDetail> AddMealToListAsync(int mealId)
        {
            var mealSettingsDetail = await FindMealSettingsDetailByIdAsync(mealId);
            if (mealSettingsDetail.IsActive == true)
            {
                return mealSettingsDetail;
            }
            mealSettingsDetail.IsActive = true;
            var mealSettingUser = await _context.MealSettings.FirstOrDefaultAsync(m => m.Id == mealSettingsDetail.MealSettingsId);
            if (mealSettingUser == null)
            {
                throw new InvalidOperationException("User không tồn tại cho MealSettings.");
            }

            var userDetail = await _context.UserDetails.FirstOrDefaultAsync(u => u.UserId == mealSettingUser.UserId);

            var totalCalories = userDetail.Calo.Value;

            var activeMeals = await _context.MealSettingsDetails
                .Include(m => m.NutritionTargetsDaily)
                .Where(m => m.NutritionTargetsDaily != null
                            && m.NutritionTargetsDaily.UserId == mealSettingUser.UserId
                            && m.DayOfTheWeekId == mealSettingsDetail.DayOfTheWeekId
                            && m.IsActive == true
                            && m.NutritionTargetsDaily.IsActive == true
                            )
                .ToListAsync();
            // Lấy danh sách các SlotOfTheDayId hiện có trong activeMeals với IsActive là true
            var existingSlots = activeMeals
                .Where(m => m.IsActive == true)
                .Select(m => m.SlotOfTheDayId)
                .ToList();

            // Kiểm tra nếu SlotOfTheDayId của bữa mới đã tồn tại trong danh sách activeMeals
            if (existingSlots.Contains(mealSettingsDetail.SlotOfTheDayId))
            {
                double totalCaloriesSlot = activeMeals
                .Where(m => m.SlotOfTheDayId == mealSettingsDetail.SlotOfTheDayId)
                .Sum(m => m.NutritionTargetsDaily?.Calories ?? 0);

                await DistributeCaloriesForSameSlotMealsAsync(1,mealSettingsDetail.SlotOfTheDayId, mealSettingsDetail, activeMeals, mealSettingUser.UserId, totalCaloriesSlot);

            }
            else
            {
                await DistributeCaloriesForSlots(1,activeMeals, mealSettingsDetail, mealSettingUser.UserId, totalCalories);

            }
            var maxOrderNumber = activeMeals
                .Where(m => m.SlotOfTheDayId == mealSettingsDetail.SlotOfTheDayId
                            && m.DayOfTheWeekId == mealSettingsDetail.DayOfTheWeekId
                            && m.IsActive == true)
                .Max(m => (int?)m.OrderNumber) ?? 0;

            mealSettingsDetail.OrderNumber = (short?)(maxOrderNumber + 1);
            _context.MealSettingsDetails.Update(mealSettingsDetail);
            await _context.SaveChangesAsync();

            return mealSettingsDetail;
        }

        public async Task AddMealSettingsDetailAsync(MealSettingsDetail mealSettingsDetail)
        {
            await _context.MealSettingsDetails.AddAsync(mealSettingsDetail);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateMealSettingDetailAsync(MealSettingsDetail mealSettingsDetail, MealSettingsDetailDTO model)
        {
            mealSettingsDetail.MealSettingsId = model.MealSettingsId;
            mealSettingsDetail.SlotOfTheDayId = model.SlotOfTheDayId;
            mealSettingsDetail.NutritionTargetsDailyId = model.NutritionTargetsDailyId;
            mealSettingsDetail.DayOfTheWeekId = model.DayOfTheWeekId;
            mealSettingsDetail.SkipCreationProcess = model.SkipCreationProcess;
            mealSettingsDetail.Size = model.Size;
            mealSettingsDetail.NutritionFocus = model.NutritionFocus;
            mealSettingsDetail.NumberOfDishes = model.NumberOfDishes;
            mealSettingsDetail.TypeFavoriteFood = model.TypeFavoriteFood;
            mealSettingsDetail.WantCookingId = model.WantCookingId;
            mealSettingsDetail.TimeAvailable = model.TimeAvailable;
            mealSettingsDetail.CookingDifficultyId = model.CookingDifficultyId;
            mealSettingsDetail.IsActive = model.IsActive;
            mealSettingsDetail.OrderNumber = model.OrderNumber;
            mealSettingsDetail.Name = model.Name;
            mealSettingsDetail.Calories = (short)model.Calories;
            _context.MealSettingsDetails.Update(mealSettingsDetail);
            await _context.SaveChangesAsync();
        }
        ///// Update
        public async Task<MealSettingsDetail> EditMealSettingsDetailAsync(int id, MealSettingsDetailDTO model)
        {
            var mealSettingsDetail = await FindMealSettingsDetailByIdAsync(id);
            if (mealSettingsDetail == null) return null;
            //// Cap nhat lai ordernumber
            if(model.OrderNumber != null)
            {
                if (model.SlotOfTheDayId != mealSettingsDetail.SlotOfTheDayId || model.DayOfTheWeekId != mealSettingsDetail.DayOfTheWeekId)
                {
                    var mealsInOldSlot = await _context.MealSettingsDetails
                        .Where(m => m.MealSettingsId == mealSettingsDetail.MealSettingsId
                                    && m.SlotOfTheDayId == mealSettingsDetail.SlotOfTheDayId
                                    && m.DayOfTheWeekId == mealSettingsDetail.DayOfTheWeekId
                                    && m.OrderNumber > mealSettingsDetail.OrderNumber)
                        .OrderBy(m => m.OrderNumber)
                        .ToListAsync();

                    foreach (var meal in mealsInOldSlot)
                    {
                        meal.OrderNumber = (short?)(meal.OrderNumber - 1);
                        _context.MealSettingsDetails.Update(meal);
                    }
                    var maxOrderNumberInNewSlot = await _context.MealSettingsDetails
                        .Where(m => m.MealSettingsId == mealSettingsDetail.MealSettingsId
                                    && m.SlotOfTheDayId == model.SlotOfTheDayId
                                    && m.DayOfTheWeekId == model.DayOfTheWeekId)
                        .MaxAsync(m => (int?)m.OrderNumber) ?? 0;

                    model.OrderNumber = (short)(maxOrderNumberInNewSlot + 1);
                }
            }
            await UpdateMealSettingDetailAsync(mealSettingsDetail, model);
            
            await _context.SaveChangesAsync();
            return mealSettingsDetail;
        }

        public async Task UpdateCalo(int id)
        {
            var mealSettingsDetail = await FindMealSettingsDetailByIdAsync(id);
            var mealSettingUser = await _context.MealSettings.FirstOrDefaultAsync(m => m.Id == mealSettingsDetail.MealSettingsId);
            var userDetail = await _context.UserDetails.FirstOrDefaultAsync(u => u.UserId == mealSettingUser.UserId);

            double totalCalories = userDetail.Calo.Value;

            for (int day = 1; day <= 8; day++)
            {
                // Lọc các bữa ăn active của người dùng theo từng ngày
                var activeMeals = await _context.MealSettingsDetails
                    .Include(m => m.NutritionTargetsDaily)
                    .Where(m => m.NutritionTargetsDaily != null
                                && m.NutritionTargetsDaily.UserId == mealSettingUser.UserId
                                && m.DayOfTheWeekId == day
                                && m.IsActive == true
                                && m.NutritionTargetsDaily.IsActive == true)
                    .ToListAsync();

                // Nếu có các bữa ăn active trong ngày, phân bổ lại calo cho từng slot của ngày đó
                if (activeMeals.Count > 0)
                {
                    await DistributeCaloriesForSlots(2, activeMeals, mealSettingsDetail, mealSettingUser.UserId, totalCalories);
                }
            }

            await _context.SaveChangesAsync();
        }



        public async Task UpdateMealSettingForMealAsync(int userId, MealSettingDTO dto)
        {
            var mealSetting = await _context.MealSettings.FirstOrDefaultAsync(ms => ms.UserId == userId);
            if (mealSetting != null)
            {
                // Cập nhật các thuộc tính từ DTO
                mealSetting.FoodTypeIdWant = dto.FoodTypeIdWant ?? 0;
                mealSetting.DayOfTheWeekStartId = dto.DayOfTheWeekStartId;
                mealSetting.SameScheduleEveryDay = dto.SameScheduleEveryDay;

                _context.MealSettings.Update(mealSetting);
                await _context.SaveChangesAsync();
            }
        }


        ///// Find 
        public async Task<MealSettingsDetail> FindMealSettingsDetailByNutritionTargetsDailyIdAsync(int nutritionTargetsDailyId)
        {
            return await _context.MealSettingsDetails
                .FirstOrDefaultAsync(m => m.NutritionTargetsDailyId == nutritionTargetsDailyId);
        }
        public async Task<MealSettingsDetail> FindMealSettingsDetailByIdAsync(int id)
        {
            return await _context.MealSettingsDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MealSetting> GetMealSettingByUserIdAsync(int userId)
        {
            return await _context.MealSettings.FirstOrDefaultAsync(ms => ms.UserId == userId);
        }
        public async Task<MealSettingsDetail> GetMealSettingDetailByMealSettingIdAsync(int mealSettingsId)
        {
            return await _context.MealSettingsDetails.FirstOrDefaultAsync(ms => ms.MealSettingsId == mealSettingsId);
        }
        public async Task<List<CreateMealSettingsDetailDto>> GetAllMealSettingByUserIdAsync(int userId)
        {
            var mealSetting = await _context.MealSettings.FirstOrDefaultAsync(ms => ms.UserId == userId);
            if (mealSetting != null)
            {
                return await _context.MealSettingsDetails
                                     .Where(msd => msd.MealSettingsId == mealSetting.Id)
                                     .Select(d => new CreateMealSettingsDetailDto
                                     {
                                         Id = d.Id,
                                         MealSettingsId = d.MealSettingsId,
                                         SlotOfTheDayId = d.SlotOfTheDayId,
                                         NutritionTargetsDailyId = d.NutritionTargetsDailyId,
                                         DayOfTheWeekId = d.DayOfTheWeekId,
                                         SkipCreationProcess = d.SkipCreationProcess,
                                         Size = d.Size,
                                         NutritionFocus = d.NutritionFocus,
                                         NumberOfDishes = d.NumberOfDishes,
                                         TypeFavoriteFood = d.TypeFavoriteFood,
                                         WantCookingId = d.WantCookingId,
                                         TimeAvailable = d.TimeAvailable,
                                         CookingDifficultyId = d.CookingDifficultyId,
                                         Name = d.Name,
                                         IsActive = d.IsActive,
                                         OrderNumber = d.OrderNumber
                                     }).ToListAsync();
            }
            return new List<CreateMealSettingsDetailDto>();
        }



        public async Task<List<CreateMealSettingsDetailDto>> GetAllMealSettingBySelectedAsync(int userId)
{
    var mealSetting = await _context.MealSettings.FirstOrDefaultAsync(ms => ms.UserId == userId);
    if (mealSetting != null)
    {
        return await _context.MealSettingsDetails
                             .Where(msd => msd.MealSettingsId == mealSetting.Id && msd.IsActive == true)
                             .Select(d => new CreateMealSettingsDetailDto
                             {
                                 Id = d.Id,
                                 MealSettingsId = d.MealSettingsId,
                                 SlotOfTheDayId = d.SlotOfTheDayId,
                                 NutritionTargetsDailyId = d.NutritionTargetsDailyId,
                                 DayOfTheWeekId = d.DayOfTheWeekId,
                                 SkipCreationProcess = d.SkipCreationProcess,
                                 Size = d.Size,
                                 NutritionFocus = d.NutritionFocus,
                                 NumberOfDishes = d.NumberOfDishes,
                                 TypeFavoriteFood = d.TypeFavoriteFood,
                                 WantCookingId = d.WantCookingId,
                                 TimeAvailable = d.TimeAvailable,
                                 CookingDifficultyId = d.CookingDifficultyId,
                                 Name = d.Name,
                                 IsActive = d.IsActive,
                                 OrderNumber = d.OrderNumber
                             }).ToListAsync();
    }
    return new List<CreateMealSettingsDetailDto>();
}

        public async Task<List<MealSettingsDetail>> GetAllMealAsync()
        {
            return await _context.MealSettingsDetails.ToListAsync();
        }

        ///// Delete
        public async Task DeleteMealSettingsDetailAsync(int id)
        {
            var mealSettingsDetail = await _context.MealSettingsDetails
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mealSettingsDetail == null) throw new KeyNotFoundException("MealSettingsDetail not found.");

            var mealSettingUser = await _context.MealSettings.FirstOrDefaultAsync(m => m.Id == mealSettingsDetail.MealSettingsId);
            if (mealSettingUser == null)
            {
                throw new InvalidOperationException("User không tồn tại cho MealSettings.");
            }

            var userDetail = await _context.UserDetails.FirstOrDefaultAsync(u => u.UserId == mealSettingUser.UserId);

            var totalCalories = userDetail.Calo.Value;

            var activeMeals = await _context.MealSettingsDetails
                .Include(m => m.NutritionTargetsDaily)
                .Where(m => m.NutritionTargetsDaily != null
                            && m.NutritionTargetsDaily.UserId == mealSettingUser.UserId
                            && m.DayOfTheWeekId == mealSettingsDetail.DayOfTheWeekId
                            && m.IsActive == true
                            && m.NutritionTargetsDaily.IsActive == true
                            )
                .ToListAsync();

            if (activeMeals.Count != 0)
            {
                // Lấy danh sách các SlotOfTheDayId hiện có trong activeMeals với IsActive là true
                var existingSlots = activeMeals
                    .Where(m => m.IsActive == true && m.SlotOfTheDayId == mealSettingsDetail.SlotOfTheDayId)
                    .Count();

                // Kiểm tra nếu SlotOfTheDayId của bữa mới đã tồn tại trong danh sách activeMeals
                if (existingSlots > 1)
                {
                    double totalCaloriesSlot = activeMeals
                    .Where(m => m.SlotOfTheDayId == mealSettingsDetail.SlotOfTheDayId)
                    .Sum(m => m.NutritionTargetsDaily?.Calories ?? 0);

                    await DeactivateMealAndUpdateOrderAsync(mealSettingsDetail);
                    await DistributeCaloriesForSameSlotMealsAsync(3, mealSettingsDetail.SlotOfTheDayId, mealSettingsDetail, activeMeals, mealSettingUser.UserId, totalCaloriesSlot);
                    await RemoveMealSeettingDetailAndNutritionTargetsDaily(mealSettingsDetail);
                }
                else
                {
                    await DeactivateMealAndUpdateOrderAsync(mealSettingsDetail);
                    await DistributeCaloriesForSlots(3, activeMeals, mealSettingsDetail, mealSettingUser.UserId, totalCalories);
                    await RemoveMealSeettingDetailAndNutritionTargetsDaily(mealSettingsDetail);
                }
            }
            else
            {
                await RemoveMealSeettingDetailAndNutritionTargetsDaily(mealSettingsDetail);
            }

            await _context.SaveChangesAsync();
        }
        public async Task RemoveMealSeettingDetailAndNutritionTargetsDaily(MealSettingsDetail mealSettingsDetail)
        {
            if (mealSettingsDetail.NutritionTargetsDailyId.HasValue)
            {
                var nutritionTarget = await _context.NutritionTargetsDailies
                    .FirstOrDefaultAsync(nt => nt.Id == mealSettingsDetail.NutritionTargetsDailyId);

                if (nutritionTarget != null)
                {
                    _context.NutritionTargetsDailies.Remove(nutritionTarget);
                }
            }
            _context.MealSettingsDetails.Remove(mealSettingsDetail);
             await _context.SaveChangesAsync();
        }
        public async Task DeactivateMealAndUpdateOrderAsync(MealSettingsDetail mealSettingsDetail)
        {
            var mealSetting = _context.MealSettings.FirstOrDefault(x => x.Id == mealSettingsDetail.MealSettingsId);
            mealSettingsDetail.IsActive = false;
            var removedOrderNumber = mealSettingsDetail.OrderNumber;
            mealSettingsDetail.OrderNumber = null;
            mealSettingsDetail.Calories = 0;
            mealSettingsDetail.CarbsMin = 0;
            mealSettingsDetail.CarbsMax = 0;
            mealSettingsDetail.ProteinMax = 0;
            mealSettingsDetail.ProteinMin = 0;
            mealSettingsDetail.FatsMin = 0;
            mealSettingsDetail.FatsMax = 0;
            mealSettingsDetail.MinimumFiber = 0;
            _context.MealSettingsDetails.Update(mealSettingsDetail);

            // Cập nhật trạng thái IsActive của NutritionTargetsDaily nếu tồn tại
            if (mealSettingsDetail.NutritionTargetsDailyId.HasValue)
            {
                var nutritionTarget = await _context.NutritionTargetsDailies
                    .FirstOrDefaultAsync(nt => nt.Id == mealSettingsDetail.NutritionTargetsDailyId);

                if (nutritionTarget != null)
                {
                    nutritionTarget.FoodTypeIdWant = mealSetting.FoodTypeIdWant;
                    nutritionTarget.LimitDailyCholesterol = false;
                    nutritionTarget.LimitDailySodium = false;
                    nutritionTarget.IsActive = false;
                    nutritionTarget.Calories = 0;
                    nutritionTarget.MinimumFiber = 0;
                    nutritionTarget.FatsMax = 0;
                    nutritionTarget.CarbsMax = 0;
                    nutritionTarget.ProteinMax = 0;
                    _context.NutritionTargetsDailies.Update(nutritionTarget);
                }
            }

            // Cập nhật OrderNumber cho các bữa ăn còn lại trong cùng SlotOfTheDayId và DayOfTheWeekId
            var mealsToUpdate = await _context.MealSettingsDetails
                .Where(m => m.MealSettingsId == mealSettingsDetail.MealSettingsId
                            && m.SlotOfTheDayId == mealSettingsDetail.SlotOfTheDayId
                            && m.IsActive == true
                            && m.DayOfTheWeekId == mealSettingsDetail.DayOfTheWeekId
                            && m.OrderNumber > removedOrderNumber)
                .OrderBy(m => m.OrderNumber)
                .ToListAsync();

            foreach (var meal in mealsToUpdate)
            {
                meal.OrderNumber = (short?)(meal.OrderNumber - 1);
                _context.MealSettingsDetails.Update(meal);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<MealSettingsDetail> RemoveMealToListAsync(int mealId)
        {
            // Tìm bữa ăn cần xóa
            var mealSettingsDetail = await _context.MealSettingsDetails.FirstOrDefaultAsync(x => x.Id == mealId);
            if (mealSettingsDetail == null)
            {
                return null;
            }
            var mealSettingUser = await _context.MealSettings.FirstOrDefaultAsync(m => m.Id == mealSettingsDetail.MealSettingsId);
            if (mealSettingUser == null)
            {
                throw new InvalidOperationException("User không tồn tại cho MealSettings.");
            }

            var userDetail = await _context.UserDetails.FirstOrDefaultAsync(u => u.UserId == mealSettingUser.UserId);

            var totalCalories = userDetail.Calo.Value;

            var activeMeals = await _context.MealSettingsDetails
                .Include(m => m.NutritionTargetsDaily)
                .Where(m => m.NutritionTargetsDaily != null
                            && m.NutritionTargetsDaily.UserId == mealSettingUser.UserId
                            && m.DayOfTheWeekId == mealSettingsDetail.DayOfTheWeekId
                            && m.IsActive == true
                            && m.NutritionTargetsDaily.IsActive == true
                            )
                .ToListAsync();
            
            if(activeMeals.Count != 0)
            {
                // Lấy danh sách các SlotOfTheDayId hiện có trong activeMeals với IsActive là true
                var existingSlots = activeMeals
                    .Where(m => m.IsActive == true && m.SlotOfTheDayId == mealSettingsDetail.SlotOfTheDayId)
                    .Count();

                // Kiểm tra nếu SlotOfTheDayId của bữa mới đã tồn tại trong danh sách activeMeals
                if (existingSlots > 1)
                {
                    double totalCaloriesSlot = activeMeals
                    .Where(m => m.SlotOfTheDayId == mealSettingsDetail.SlotOfTheDayId)
                    .Sum(m => m.NutritionTargetsDaily?.Calories ?? 0);

                    await DeactivateMealAndUpdateOrderAsync(mealSettingsDetail);

                    await DistributeCaloriesForSameSlotMealsAsync(2, mealSettingsDetail.SlotOfTheDayId, mealSettingsDetail, activeMeals, mealSettingUser.UserId, totalCaloriesSlot);

                }
                else
                {
                    await DeactivateMealAndUpdateOrderAsync(mealSettingsDetail);
                    await DistributeCaloriesForSlots(2, activeMeals, mealSettingsDetail, mealSettingUser.UserId, totalCalories);

                }
            }
            else
            {
                mealSettingsDetail.IsActive = false;
                await _context.SaveChangesAsync();
                return mealSettingsDetail;
            }
            await _context.SaveChangesAsync();
            return mealSettingsDetail;
        }



        //// Change
        public async Task<bool> ChangeOrderNumberAsync(int mealId, string direction)
        {
            // Truy vấn lại đối tượng MealSettingsDetail từ cơ sở dữ liệu để cập nhật
            var mealSettingsDetail = await _context.MealSettingsDetails.FirstOrDefaultAsync(x => x.Id == mealId);
            if (mealSettingsDetail == null) return false;

            // Lấy danh sách các MealSettingsDetail cần thiết từ cơ sở dữ liệu để cập nhật
            var existingMeals = await _context.MealSettingsDetails
                .Where(m => m.MealSettingsId == mealSettingsDetail.MealSettingsId
                            && m.SlotOfTheDayId == mealSettingsDetail.SlotOfTheDayId
                            && m.IsActive == true
                            && m.DayOfTheWeekId == mealSettingsDetail.DayOfTheWeekId)
                .OrderBy(m => m.OrderNumber)
                .ToListAsync();

            // Tìm vị trí hiện tại của meal trong danh sách đã sắp xếp
            int currentIndex = existingMeals.FindIndex(m => m.Id == mealId);
            if (currentIndex == -1) return false;

            int targetIndex = direction == "up" ? currentIndex - 1 : currentIndex + 1;

            if (targetIndex >= 0 && targetIndex < existingMeals.Count)
            {
                // Truy vấn đối tượng MealSettingsDetail đích để thực hiện hoán đổi OrderNumber
                var targetMeal = existingMeals[targetIndex];
                short? tempOrder = mealSettingsDetail.OrderNumber;
                mealSettingsDetail.OrderNumber = targetMeal.OrderNumber;
                targetMeal.OrderNumber = tempOrder;

                // Cập nhật các đối tượng MealSettingsDetail trong cơ sở dữ liệu
                _context.MealSettingsDetails.Update(mealSettingsDetail);
                _context.MealSettingsDetails.Update(targetMeal);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }


    }
}
