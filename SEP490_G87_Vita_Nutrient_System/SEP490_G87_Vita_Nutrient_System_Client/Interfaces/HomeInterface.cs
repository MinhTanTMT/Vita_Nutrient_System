using Microsoft.AspNetCore.Mvc;

namespace SEP490_G87_Vita_Nutrient_System_Client.Interfaces
{
    public interface HomeInterface
    {

        public IActionResult Index();
        public IActionResult Login();
        public Task<IActionResult> Login(string account, string password);
        public IActionResult Register();
        public Task<IActionResult> Register(string account, string password, string confirm);
        Task<bool> checkExsitAsync(string account);
        public Task<IActionResult> Logout();

    }
}
