    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Newtonsoft.Json;
    using SEP490_G87_Vita_Nutrient_System_Client.Models;
    using System;
using System.Net;
using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Text;

    namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
    {
        public class MealController : Controller
        {
            private readonly HttpClient client = null;
            public MealController()
            {
                Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
                client = new HttpClient();
                client.BaseAddress = URIBase;
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
            }


        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///


        [HttpPost]
        public async Task<IActionResult> AddMealToList(int mealId)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsync($"{client.BaseAddress}/Meals/AddMealToList/{mealId}", null);

                if (response.IsSuccessStatusCode)
                {
                    // Gọi UpdateCalo sau khi cập nhật bữa ăn thành công
                    HttpResponseMessage updateCaloResponse = await client.PutAsync($"{client.BaseAddress}/Meals/UpdateCalo/{mealId}", null);

                    if (updateCaloResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("MealSettingsDetailToList");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Lỗi khi cập nhật calo cho bữa ăn.";
                    }
                }          
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    return RedirectToAction("MealSettingsDetailToList");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Lỗi khi thêm bữa ăn: {error}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi trong quá trình gọi API: {ex.Message}";
            }
            return RedirectToAction("MealSettingsDetailToList");
        }



        [HttpPost]
        public async Task<IActionResult> RemoveMealToList(int mealId)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsync($"{client.BaseAddress}/Meals/RemoveMealToList/{mealId}", null);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("MealSettingsDetailToList");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Lỗi khi xóa bữa ăn: {error}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi trong quá trình gọi API: {ex.Message}";
            }

            return RedirectToAction("MealSettingsDetailToList");
        }



        [HttpPost]
        public async Task<IActionResult> ChangeOrderNumber(int mealId, string direction)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsync($"{client.BaseAddress}/Meals/ChangeOrderNumber/{mealId}?direction={direction}", null);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = error });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi trong quá trình gọi API: {ex.Message}" });
            }
        }



        [HttpGet]
        public async Task<IActionResult> MealSettingsDetailToList()
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            List<CreateMealSettingsDetail> activeMeals = new List<CreateMealSettingsDetail>();
            List<CookingDifficulty> cookingDifficulties = new List<CookingDifficulty>();
            List<WantCooking> wantCookings = new List<WantCooking>();
            List<SlotOfTheDay> slotOfTheDays = new List<SlotOfTheDay>();
            List<SelectListItem> daysOfWeek = new List<SelectListItem>();
            List<SelectListItem> foodTypes = new List<SelectListItem>();
            short foodTypeIdWant = 0;
            short dayOfTheWeekStartId = 0;
            bool sameScheduleEveryDay = false;
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}/Meals/GetAllMealSettingBySelected/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                     activeMeals = JsonConvert.DeserializeObject<List<CreateMealSettingsDetail>>(jsonData);
                    
                }

                HttpResponseMessage mealSettingResponse = await client.GetAsync($"{client.BaseAddress}/Meals/GetMealSettingByUserId/{userId}");
                if (mealSettingResponse.IsSuccessStatusCode)
                {
                    var mealSettingData = await mealSettingResponse.Content.ReadAsStringAsync();
                    var mealSetting = JsonConvert.DeserializeObject<MealSetting>(mealSettingData);

                    foodTypeIdWant = mealSetting.FoodTypeIdWant;
                    sameScheduleEveryDay = mealSetting.SameScheduleEveryDay;
                    sameScheduleEveryDay = mealSetting.SameScheduleEveryDay;

                    if (sameScheduleEveryDay)
                    {
                        dayOfTheWeekStartId = 8;                                                  
                        if (mealSetting.DayOfTheWeekStartId != 8)
                        {
                            mealSetting.DayOfTheWeekStartId = 8;
                            var content = new StringContent(JsonConvert.SerializeObject(mealSetting), Encoding.UTF8, "application/json");
                            await client.PutAsync($"{client.BaseAddress}/Meals/UpdateMealSetting/{mealSetting.UserId}", content);
                        }
                    }
                    else
                    {
                        dayOfTheWeekStartId = mealSetting.DayOfTheWeekStartId;
                    }
                }
                HttpResponseMessage foodTypeResponse = await client.GetAsync($"{client.BaseAddress}/Food/GetDietType");
                if (foodTypeResponse.IsSuccessStatusCode)
                {
                    var foodTypeData = await foodTypeResponse.Content.ReadAsStringAsync();
                    var foodTypesList = JsonConvert.DeserializeObject<List<DietType>>(foodTypeData);
                    foodTypes = foodTypesList.Select(ft => new SelectListItem
                    {
                        Value = ft.DietTypeId.ToString(),
                        Text = ft.Name,
                        Selected = ft.DietTypeId == foodTypeIdWant 
                    }).ToList();
                }

                HttpResponseMessage dayOfTheWeekResponse = await client.GetAsync($"{client.BaseAddress}/DayOfTheWeek/GetAllDayOfTheWeek");
                if (dayOfTheWeekResponse.IsSuccessStatusCode)
                {
                    var jsonData = await dayOfTheWeekResponse.Content.ReadAsStringAsync();
                    var days = JsonConvert.DeserializeObject<List<DayOfTheWeek>>(jsonData);

                    daysOfWeek = days
                    .Where(day => sameScheduleEveryDay || day.Id != 8)
                    .Select(day => new SelectListItem
                    {
                        Value = day.Id.ToString(),
                        Text = day.Name,
                        Selected = day.Id == dayOfTheWeekStartId
                    }).ToList();
                }
                HttpResponseMessage cookingDifficultyResponse = await client.GetAsync($"{client.BaseAddress}/CookingDifficulties/GetAllCookingDifficulties");
                if (cookingDifficultyResponse.IsSuccessStatusCode)
                {
                    var cookingData = await cookingDifficultyResponse.Content.ReadAsStringAsync();
                    cookingDifficulties = JsonConvert.DeserializeObject<List<CookingDifficulty>>(cookingData);
                }
                HttpResponseMessage wantCookingResponse = await client.GetAsync($"{client.BaseAddress}/WantCookings/GetAllWantCookings");
                if (wantCookingResponse.IsSuccessStatusCode)
                {
                    var wantCookingData = await wantCookingResponse.Content.ReadAsStringAsync();
                    wantCookings = JsonConvert.DeserializeObject<List<WantCooking>>(wantCookingData);
                }
                HttpResponseMessage slotOfTheDayResponse = await client.GetAsync($"{client.BaseAddress}/SlotOfTheDays/GetAllSlotOfTheDays");
                if (slotOfTheDayResponse.IsSuccessStatusCode)
                {
                    var slotOfTheDayData = await slotOfTheDayResponse.Content.ReadAsStringAsync();
                    slotOfTheDays = JsonConvert.DeserializeObject<List<SlotOfTheDay>>(slotOfTheDayData);
                }
                foreach (var meal in activeMeals)
                {
                    meal.CookingDifficulty = cookingDifficulties.FirstOrDefault(cd => cd.Id == meal.CookingDifficultyId)?.Name;
                    meal.SlotOfTheDay = slotOfTheDays.FirstOrDefault(s => s.Id == meal.SlotOfTheDayId)?.Slot;
                    meal.WantCooking = wantCookings.FirstOrDefault(wc => wc.Id == meal.WantCookingId)?.Name;
                }
                ViewBag.FoodTypes = foodTypes;
                ViewBag.FoodTypeIdWant = foodTypeIdWant;
                ViewBag.DaysOfWeek = daysOfWeek;
                ViewData["DayOfTheWeekStartId"] = dayOfTheWeekStartId;
                ViewBag.SameScheduleEveryDay = sameScheduleEveryDay;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi trong quá trình gọi API: {ex.Message}";
            }
            return View(activeMeals);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDietType([FromBody] MealSetting dto)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            try
            {
                HttpResponseMessage mealSettingResponse = await client.GetAsync($"{client.BaseAddress}/Meals/GetMealSettingByUserId/{userId}");
                if (mealSettingResponse.IsSuccessStatusCode)
                {
                    var mealSettingData = await mealSettingResponse.Content.ReadAsStringAsync();
                    var mealSetting = JsonConvert.DeserializeObject<MealSetting>(mealSettingData);

                    mealSetting.FoodTypeIdWant = dto.FoodTypeIdWant;

                    var content = new StringContent(JsonConvert.SerializeObject(mealSetting), Encoding.UTF8, "application/json");
                    HttpResponseMessage updateResponse = await client.PutAsync($"{client.BaseAddress}/Meals/UpdateMealSetting/{mealSetting.UserId}", content);

                    if (updateResponse.IsSuccessStatusCode)
                    {
                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi trong quá trình cập nhật: {ex.Message}" });
            }

            return Json(new { success = false, message = "Cập nhật không thành công." });
        }


        [HttpGet]
        public async Task<IActionResult> DietTypeList()
        {
            List<DietType> dietTypes = new List<DietType>();
            short currentDietTypeId = 0; // Biến lưu chế độ ăn hiện tại

            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            try
            {
                // Lấy danh sách các chế độ ăn
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}/Food/GetDietType");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    dietTypes = JsonConvert.DeserializeObject<List<DietType>>(jsonData);
                }

                // Lấy chế độ ăn hiện tại của người dùng
                HttpResponseMessage mealSettingResponse = await client.GetAsync($"{client.BaseAddress}/Meals/GetMealSettingByUserId/{userId}");
                if (mealSettingResponse.IsSuccessStatusCode)
                {
                    var mealSettingData = await mealSettingResponse.Content.ReadAsStringAsync();
                    var mealSetting = JsonConvert.DeserializeObject<MealSetting>(mealSettingData);
                    currentDietTypeId = mealSetting.FoodTypeIdWant; // Lưu chế độ ăn hiện tại
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi trong quá trình gọi API: {ex.Message}";
            }

            ViewBag.CurrentDietTypeId = currentDietTypeId; // Truyền chế độ ăn hiện tại qua ViewBag
            return View(dietTypes);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateDayOfTheWeek([FromBody] DayOfTheWeek dto)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            try
            {
                HttpResponseMessage mealSettingResponse = await client.GetAsync($"{client.BaseAddress}/Meals/GetMealSettingByUserId/{userId}");
                if (mealSettingResponse.IsSuccessStatusCode)
                {
                    var mealSettingData = await mealSettingResponse.Content.ReadAsStringAsync();
                    var mealSetting = JsonConvert.DeserializeObject<MealSetting>(mealSettingData);

                    mealSetting.DayOfTheWeekStartId = dto.Id;

                    var content = new StringContent(JsonConvert.SerializeObject(mealSetting), Encoding.UTF8, "application/json");
                    HttpResponseMessage updateResponse = await client.PutAsync($"{client.BaseAddress}/Meals/UpdateMealSetting/{mealSetting.UserId}", content);

                    if (updateResponse.IsSuccessStatusCode)
                    {
                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi trong quá trình cập nhật: {ex.Message}" });
            }

            return Json(new { success = false, message = "Cập nhật không thành công." });
        }





        [HttpPost]
        public async Task<IActionResult> UpdateSameScheduleEveryDay(bool SameScheduleEveryDay)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            try
            {
                HttpResponseMessage mealSettingResponse = await client.GetAsync($"{client.BaseAddress}/Meals/GetMealSettingByUserId/{userId}");
                if (mealSettingResponse.IsSuccessStatusCode)
                {
                    var mealSettingData = await mealSettingResponse.Content.ReadAsStringAsync();
                    var mealSetting = JsonConvert.DeserializeObject<MealSetting>(mealSettingData);

                    mealSetting.SameScheduleEveryDay = SameScheduleEveryDay;
                    if(SameScheduleEveryDay == false)
                    {
                        mealSetting.DayOfTheWeekStartId = 1;
                    }
                    else
                    {
                        mealSetting.DayOfTheWeekStartId = 8;
                    }
                    var content = new StringContent(JsonConvert.SerializeObject(mealSetting), Encoding.UTF8, "application/json");
                    HttpResponseMessage updateResponse = await client.PutAsync($"{client.BaseAddress}/Meals/UpdateMealSetting/{mealSetting.UserId}", content);

                    if (updateResponse.IsSuccessStatusCode)
                    {
                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi trong quá trình cập nhật: {ex.Message}" });
            }

            return Json(new { success = false, message = "Cập nhật không thành công." });
        }





        [HttpPost]
        public async Task<IActionResult> CreateMealSettingsDetailAsync(CreateMealSettingsDetail model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropDownLists();
                return View(model);
            }

            try
            {
                // Gửi dữ liệu lên API
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{client.BaseAddress}/Meals/CreateMealSettingsDetail", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("MealList", new { dayOfTheWeekId = model.DayOfTheWeekId });
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Lỗi khi tạo bữa ăn: {error}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi trong quá trình gọi API: {ex.Message}";
            }

            await LoadDropDownLists();
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> CreateMealSettingsDetailAsync(short dayOfTheWeekId)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            if (userId == null)
            {
                ViewBag.ErrorMessage = "Không tìm thấy UserId.";
                return RedirectToAction("MealSettingsDetailToList");
            }
            var model = new CreateMealSettingsDetail
            {
                SkipCreationProcess = false,
                NutritionFocus = false,
                DayOfTheWeekId = dayOfTheWeekId,
                NutritionTargetsDailyId = null,
            };
            HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}/Meals/GetMealSettingByUserId/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var mealSetting = JsonConvert.DeserializeObject<MealSetting>(jsonData);
                model.MealSettingsId = mealSetting.Id;
            }
            else
            {
                ViewBag.ErrorMessage = "Không thể lấy MealSettingsId.";
                return View();
            }

            await LoadDropDownLists();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> MealList(short dayOfTheWeekId)
        {
            List<CreateMealSettingsDetail> meals = new List<CreateMealSettingsDetail>();
            List<CookingDifficulty> cookingDifficulties = new List<CookingDifficulty>();
            List<WantCooking> wantCookings = new List<WantCooking>();
            List<SlotOfTheDay> slotOfTheDays = new List<SlotOfTheDay>();

            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            if (userId == null )
            {
                ViewBag.ErrorMessage = "Không tìm thấy UserId.";
                return View();
            }
            try
            {
                
                 HttpResponseMessage mealListResponse = await client.GetAsync($"{client.BaseAddress}/Meals/GetAllMealSettingByUserId/{userId}");
                    if (mealListResponse.IsSuccessStatusCode)
                    {
                        var jsonData = await mealListResponse.Content.ReadAsStringAsync();
                        meals = JsonConvert.DeserializeObject<List<CreateMealSettingsDetail>>(jsonData);
                        meals = meals.Where(m => m.DayOfTheWeekId == dayOfTheWeekId).ToList();
                }
                    else
                    {
                        ViewBag.ErrorMessage = "Không thể lấy MealSetting từ API.";
                    }
                HttpResponseMessage cookingDifficultyResponse = await client.GetAsync(client.BaseAddress + "/CookingDifficulties/GetAllCookingDifficulties");
                if (cookingDifficultyResponse.IsSuccessStatusCode)
                {
                    var cookingData = await cookingDifficultyResponse.Content.ReadAsStringAsync();
                    cookingDifficulties = JsonConvert.DeserializeObject<List<CookingDifficulty>>(cookingData);
                }
                HttpResponseMessage wantCookingResponse = await client.GetAsync(client.BaseAddress + "/WantCookings/GetAllWantCookings");
                if (wantCookingResponse.IsSuccessStatusCode)
                {
                    var wantCookingData = await wantCookingResponse.Content.ReadAsStringAsync();
                    wantCookings = JsonConvert.DeserializeObject<List<WantCooking>>(wantCookingData);
                }
                HttpResponseMessage slotOfTheDayResponse = await client.GetAsync(client.BaseAddress + "/SlotOfTheDays/GetAllSlotOfTheDays");
                if (slotOfTheDayResponse.IsSuccessStatusCode)
                {
                    var slotOfTheDayData = await slotOfTheDayResponse.Content.ReadAsStringAsync();
                    slotOfTheDays = JsonConvert.DeserializeObject<List<SlotOfTheDay>>(slotOfTheDayData);
                }
                foreach (var meal in meals)
                {
                    meal.CookingDifficulty = cookingDifficulties.FirstOrDefault(cd => cd.Id == meal.CookingDifficultyId)?.Name;
                    meal.SlotOfTheDay = slotOfTheDays.FirstOrDefault(s => s.Id == meal.SlotOfTheDayId)?.Slot;
                    meal.WantCooking = wantCookings.FirstOrDefault(wc => wc.Id == meal.WantCookingId)?.Name;
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi trong quá trình gọi API: {ex.Message}";
            }

            ViewBag.DayOfTheWeekId = dayOfTheWeekId;
            return View(meals);
        }

        [HttpPost]
        public async Task<IActionResult> EditMealSettingsDetailAsync(int id, CreateMealSettingsDetail model)
        {
            try
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{client.BaseAddress}/Meals/EditMealSettingsDetail/{id}", content);

                if (response.IsSuccessStatusCode)
                {

                    // Gọi UpdateCalo sau khi cập nhật bữa ăn thành công
                    HttpResponseMessage updateCaloResponse = await client.PutAsync($"{client.BaseAddress}/Meals/UpdateCalo/{id}", null);

                    if (updateCaloResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("MealList", new { dayOfTheWeekId = model.DayOfTheWeekId });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Lỗi khi cập nhật calo cho bữa ăn.";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = $"Lỗi khi cập nhật thông tin bữa ăn";
                }
            }
            catch (Exception ex)
            { 
                ViewBag.ErrorMessage = $"Lỗi trong quá trình gọi API: {ex.Message}";
            }

            await LoadDropDownLists(); 
            return View("EditMealSettingsDetail", model);
        }


        [HttpGet]
            public async Task<IActionResult> EditMealSettingsDetail(int id)
            {
                CreateMealSettingsDetail mealSettingsDetail = null;
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}/Meals/FindMealSettingsDetailById/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonData = await response.Content.ReadAsStringAsync();
                        mealSettingsDetail = JsonConvert.DeserializeObject<CreateMealSettingsDetail>(jsonData);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Không thể lấy dữ liệu từ API.";
                        return RedirectToAction("Error");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Lỗi trong quá trình gọi API: {ex.Message}";
                    return RedirectToAction("Meal");
                }

                if (mealSettingsDetail == null)
                {
                    ViewBag.ErrorMessage = "Không tìm thấy MealSettingsDetail.";
                    return RedirectToAction("Error");
                }

                await LoadDropDownLists();

                return View(mealSettingsDetail);


            }


        [HttpGet]
        public async Task<IActionResult> EditMealSettingsDetailActive(int id)
        {
            CreateMealSettingsDetail mealSettingsDetail = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}/Meals/FindMealSettingsDetailById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    mealSettingsDetail = JsonConvert.DeserializeObject<CreateMealSettingsDetail>(jsonData);
                }
                else
                {
                    ViewBag.ErrorMessage = "Không thể lấy dữ liệu từ API.";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi trong quá trình gọi API: {ex.Message}";
                return RedirectToAction("Meal");
            }

            if (mealSettingsDetail == null)
            {
                ViewBag.ErrorMessage = "Không tìm thấy MealSettingsDetail.";
                return RedirectToAction("Error");
            }

            await LoadDropDownLists();

            return View(mealSettingsDetail);
        }

        [HttpPost]
        public async Task<IActionResult> EditMealSettingsDetailActiveAsync(int id, CreateMealSettingsDetail model)
        {
            try
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{client.BaseAddress}/Meals/EditMealSettingsDetail/{id}", content);

                if (response.IsSuccessStatusCode)
                {

                        return RedirectToAction("MealSettingsDetailToList");
                }
                else
                {
                    ViewBag.ErrorMessage = $"Lỗi khi cập nhật thông tin bữa ăn";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi trong quá trình gọi API: {ex.Message}";
            }

            await LoadDropDownLists();
            return View("EditMealSettingsDetail", model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMealSettingsDetail(int id, short dayOfTheWeekId)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"{client.BaseAddress}/Meals/DeleteMealSettingsDetail/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Lỗi khi xóa bữa ăn: {error}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi trong quá trình gọi API: {ex.Message}";
            }

            return RedirectToAction("MealList", new { dayOfTheWeekId });
        }


        private async Task LoadDropDownLists()
            {
                
                List<CookingDifficulty> cookingDifficulties = new List<CookingDifficulty>();
                List<WantCooking> wantCookings = new List<WantCooking>();
                List<SlotOfTheDay> slotOfTheDays = new List<SlotOfTheDay>();
                List<NutritionTargetsDaily> nutritionTargetsDaily = new List<NutritionTargetsDaily>();
                List<DayOfTheWeek> dayOfTheWeek = new List<DayOfTheWeek>();

                try
                {
                    HttpResponseMessage cookingDifficultyResponse = await client.GetAsync($"{client.BaseAddress}/CookingDifficulties/GetAllCookingDifficulties");
                    if (cookingDifficultyResponse.IsSuccessStatusCode)
                    {
                        var jsonData = await cookingDifficultyResponse.Content.ReadAsStringAsync();
                        cookingDifficulties = JsonConvert.DeserializeObject<List<CookingDifficulty>>(jsonData);
                    }

                    HttpResponseMessage wantCookingResponse = await client.GetAsync($"{client.BaseAddress}/WantCookings/GetAllWantCookings");
                    if (wantCookingResponse.IsSuccessStatusCode)
                    {
                        var jsonData = await wantCookingResponse.Content.ReadAsStringAsync();
                        wantCookings = JsonConvert.DeserializeObject<List<WantCooking>>(jsonData);
                    }

                    HttpResponseMessage slotOfTheDayResponse = await client.GetAsync($"{client.BaseAddress}/SlotOfTheDays/GetAllSlotOfTheDays");
                    if (slotOfTheDayResponse.IsSuccessStatusCode)
                    {
                        var jsonData = await slotOfTheDayResponse.Content.ReadAsStringAsync();
                        slotOfTheDays = JsonConvert.DeserializeObject<List<SlotOfTheDay>>(jsonData);
                    }

                    HttpResponseMessage nutritionTargetsDailyResponse = await client.GetAsync($"{client.BaseAddress}/NutritionTargetsDaily/GetAllNutritionTargetsDailies");
                    if (nutritionTargetsDailyResponse.IsSuccessStatusCode)
                    {
                        var jsonData = await nutritionTargetsDailyResponse.Content.ReadAsStringAsync();
                        nutritionTargetsDaily = JsonConvert.DeserializeObject<List<NutritionTargetsDaily>>(jsonData);
                    }

                    HttpResponseMessage dayOfTheWeekResponse = await client.GetAsync($"{client.BaseAddress}/DayOfTheWeek/GetAllDayOfTheWeek");
                    if (dayOfTheWeekResponse.IsSuccessStatusCode)
                    {
                        var jsonData = await dayOfTheWeekResponse.Content.ReadAsStringAsync();
                        dayOfTheWeek = JsonConvert.DeserializeObject<List<DayOfTheWeek>>(jsonData);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Lỗi trong quá trình gọi API: {ex.Message}";
                }
                ViewBag.CookingDifficulties = new SelectList(cookingDifficulties, "Id", "Name");
                ViewBag.WantCookings = new SelectList(wantCookings, "Id", "Name");
                ViewBag.SlotOfTheDays = new SelectList(slotOfTheDays, "Id", "Slot");
                ViewBag.NutritionTargetsDaily = new SelectList(nutritionTargetsDaily, "Id", "Title");
                ViewBag.DaysOfWeek = new SelectList(dayOfTheWeek, "Id", "Name");
            }



        }
    }

