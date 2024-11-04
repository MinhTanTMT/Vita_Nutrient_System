using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Net.Http.Headers;
using System.Text;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
    public class NutritionTargetsDailyController : Controller
    {
        private readonly HttpClient client = null;
        public NutritionTargetsDailyController()
        {
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient();
            client.BaseAddress = URIBase;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [HttpGet]
        public async Task<IActionResult> EditNutritionTargetsDaily(int id)
        {

            NutritionTargetsDaily nutritionTargetsDaily = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}/NutritionTargetsDaily/GetNutritionTargetsDaily/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    nutritionTargetsDaily = JsonConvert.DeserializeObject<NutritionTargetsDaily>(jsonData);
                    HttpResponseMessage mealResponse = await client.GetAsync($"{client.BaseAddress}/Meals/FindMealSettingsDetailByNutritionTargetsDailyId/{nutritionTargetsDaily.Id}");

                        if (mealResponse.IsSuccessStatusCode)
                        {
                            var mealData = await mealResponse.Content.ReadAsStringAsync();
                            var mealSettingsDetail = JsonConvert.DeserializeObject<CreateMealSettingsDetail>(mealData);

                            // Gán calo từ MealSettingsDetail vào ViewBag
                            ViewBag.MealCalories = mealSettingsDetail?.Calo;
                        }
                        else
                        {
                            ViewBag.MealCalories = "N/A"; // Không tìm thấy
                        }
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

            if (nutritionTargetsDaily == null)
            {
                ViewBag.ErrorMessage = "Không tìm thấy nutritionTargetsDaily.";
                return RedirectToAction("Error");
            }
            // Gán giá trị calo vào ViewBag
            ViewBag.NutritionCalories = nutritionTargetsDaily?.Calories; 
            return View(nutritionTargetsDaily);


        }

        [HttpPost]

        public async Task<IActionResult> EditNutritionTargetsDaily(int id, CreateMealSettingsDetail model)
        {
            try
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{client.BaseAddress}/NutritionTargetsDaily/UpdateNutritionTargetAsync/{model.Id}", content);

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

            return View("EditMealSettingsDetail", model);
        }

    }
}
