using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Net.Http.Headers;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SEP490_G87_Vita_Nutrient_System_Client
{
    public class UserController : Controller
    {
        private readonly HttpClient client = null;
        public UserController()
        {
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient();
            client.BaseAddress = URIBase;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }


        [HttpGet, Authorize(Roles = "User, UserPremium")]
        public async Task<IActionResult> ProfileUserAsync()
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + "/Users/GetUserById/" + userId);

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent content = res.Content;
                string data = await content.ReadAsStringAsync();

                TempData["user"] = data;

                return View();
            }
            return RedirectToAction("Error");
        }
        
    }
}
