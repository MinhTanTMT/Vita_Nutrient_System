using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Net.Http.Headers;
using System.Security.Principal;
using static System.Net.WebRequestMethods;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
	public class AdminController : Controller
	{
        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///

        private readonly HttpClient client = null;
        public AdminController()
        {
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient();
            client.BaseAddress = URIBase;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }


        //[HttpGet, Authorize]
        public async Task<IActionResult> QRCodePaymentPageAsync()
        {
            try
            {

                string? accountNumber = HttpContext.Session.GetString("accountNumberQRPay");
                int? limit = Int32.Parse(HttpContext.Session.GetString("limitQRPay"));
                decimal? amountInPay = decimal.Parse(HttpContext.Session.GetString("amountInPayQRPay"));
                string? contentBankPay = HttpContext.Session.GetString("contentBankPayQRPay");
                decimal? amountInImg = decimal.Parse(HttpContext.Session.GetString("amountInImgQRPay"));
                string? contentBankImg = HttpContext.Session.GetString("contentBankImgQRPay");

                if (accountNumber != null && limit != null && amountInPay != null && contentBankPay != null && amountInImg != null && contentBankImg != null)
                {
                    HttpContext.Session.Remove("accountNumberQRPay");
                    HttpContext.Session.Remove("limitQRPay");
                    HttpContext.Session.Remove("amountInPayQRPay");
                    HttpContext.Session.Remove("contentBankPayQRPay");
                    HttpContext.Session.Remove("amountInImgQRPay");
                    HttpContext.Session.Remove("contentBankImgQRPay");

                    string checkQRPaySuccess = client.BaseAddress + $"/BankPayment/APICheckQRPaySuccessful?accountNumber={accountNumber}&limit={limit}&content={contentBankPay}&amountIn={amountInPay}";
                    HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/BankPayment/APIGetQRPayDefaultSystem?amount={amountInImg}&content={contentBankImg}");

                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContent content = res.Content;
                        string data = await content.ReadAsStringAsync();
                        string linkQRImage = JsonConvert.DeserializeObject<string>(data);

                        ViewBag.CheckQRPaySuccess = checkQRPaySuccess;
                        ViewBag.LinkQRImage = linkQRImage;
                        return View();
                    }
                    
                }
                ViewBag.AlertMessage = "Error";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return View();
            }
        }

        //[HttpGet, Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AdminStatistics()
        {



            return View();
        }


        [HttpGet]
        public async Task<IActionResult> AdminDashboardAsync()
        {
            try
            {
                HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/BankPayment/APIGetAllTransactionsSystem?userMainId=1");

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    IEnumerable<TransactionsSystem> transactionsSystemData = JsonConvert.DeserializeObject<IEnumerable<TransactionsSystem>>(data);

                    var MoneyInThisMonth = transactionsSystemData.Sum(t => t.AmountIn ?? 0);
                    var MoneyOutThisMonth = transactionsSystemData.Sum(x => x.AmountOut ?? 0);
                    var BalanceThisMonth = MoneyInThisMonth - MoneyOutThisMonth;

                    ViewBag.MoneyInThisMonth = MoneyInThisMonth;
                    ViewBag.MoneyOutThisMonth = MoneyOutThisMonth;
                    ViewBag.BalanceThisMonth = BalanceThisMonth;
                    //TempData["transactionsSystemData"] = data;

                    return View(transactionsSystemData);
                }

                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return View();

            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return View();
            }  
        }



        ////////////////////////////////////////////////////////////
        /// Dũng
        ////////////////////////////////////////////////////////////
        ///






        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///





        ////////////////////////////////////////////////////////////
        /// Sơn
        ////////////////////////////////////////////////////////////
        ///





        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///

    }
}
