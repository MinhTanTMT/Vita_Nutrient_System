using Microsoft.AspNetCore.Mvc;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
	public class NutritionistController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
