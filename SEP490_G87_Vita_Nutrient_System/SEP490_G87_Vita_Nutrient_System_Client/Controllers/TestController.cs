using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
	public class TestController : Controller
	{
		[HttpGet, Authorize(Roles = "Admin")]
		public IActionResult MyTest()
		{

			return View();
		}

	}
}
