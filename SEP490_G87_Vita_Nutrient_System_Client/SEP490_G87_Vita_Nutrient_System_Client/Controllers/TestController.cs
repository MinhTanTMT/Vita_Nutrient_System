using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
	public class TestController : Controller
	{
        [HttpGet]
        public IActionResult Login()
        {
            // Kích hoạt đăng nhập bằng Google
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = "/Test/InforGG" // Sau khi đăng nhập thành công, chuyển hướng về trang chủ
            }, GoogleDefaults.AuthenticationScheme);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Xóa thông tin đăng nhập và cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> InforGG()
        {
            var claims = User.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            ViewData["Name"] = name;
            ViewData["Email"] = email;

            return View();
        }

    }
}
