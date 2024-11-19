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
            NutritionTargetOfMeal nutritionTargetsDaily = null;
            NutritionTargetsDaily additionalNutritionData = null;

            try
            {
                // Gọi API để lấy thông tin dinh dưỡng chính của meal
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}/NutritionTargetsDaily/GetNutritionTargetsDailyOfMeal/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    nutritionTargetsDaily = JsonConvert.DeserializeObject<NutritionTargetOfMeal>(jsonData);
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể lấy dữ liệu từ API GetNutritionTargetsDailyOfMeal.";
                    return RedirectToAction("Error");
                }
                // Lấy danh sách DietType để hiển thị trong dropdown
                HttpResponseMessage foodTypeResponse = await client.GetAsync($"{client.BaseAddress}/Food/GetDietType");
                if (foodTypeResponse.IsSuccessStatusCode)
                {
                    var dietTypeData = await foodTypeResponse.Content.ReadAsStringAsync();
                    var dietType = JsonConvert.DeserializeObject<List<DietType>>(dietTypeData);
                    ViewBag.DietTypes = dietType;
                }

                HttpResponseMessage additionalResponse = await client.GetAsync($"{client.BaseAddress}/Meals/FindMealSettingsDetailByNutritionTargetsDailyId/{id}");
                if (additionalResponse.IsSuccessStatusCode)
                {
                    var additionalData = await additionalResponse.Content.ReadAsStringAsync();
                    additionalNutritionData = JsonConvert.DeserializeObject<NutritionTargetsDaily>(additionalData);

                    ViewBag.Calories = additionalNutritionData.Calories;
                    ViewBag.CarbsMin = additionalNutritionData.CarbsMin;
                    ViewBag.CarbsMax = additionalNutritionData.CarbsMax;
                    ViewBag.FatsMin = additionalNutritionData.FatsMin;
                    ViewBag.FatsMax = additionalNutritionData.FatsMax;
                    ViewBag.ProteinMin = additionalNutritionData.ProteinMin;
                    ViewBag.ProteinMax = additionalNutritionData.ProteinMax;
                    ViewBag.MinimumFiber = additionalNutritionData.MinimumFiber;
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể lấy dữ liệu từ API GetNutritionTargetsDaily.";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi trong quá trình gọi API: {ex.Message}";
                return RedirectToAction("MealSettingsDetailToList", "Meal");
            }

            if (nutritionTargetsDaily == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy nutritionTargetsDaily.";
                return RedirectToAction("Error");
            }

            // Trả về model chính cho view và dữ liệu bổ sung qua ViewBag
            return View(nutritionTargetsDaily);
        }


        [HttpPost]
        public async Task<IActionResult> EditNutritionTargetsDaily(int id, NutritionTargetOfMeal model)
        {
            try
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{client.BaseAddress}/NutritionTargetsDaily/UpdateNutritionTargetsDaily/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("MealSettingsDetailToList", "Meal");
                }
                else
                {
                    TempData["ErrorMessage"] = "Lỗi khi cập nhật thông tin bữa ăn";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi trong quá trình gọi API: {ex.Message}";
            }

            return View("EditMealSettingsDetail", model);
        }

       


    }
}
