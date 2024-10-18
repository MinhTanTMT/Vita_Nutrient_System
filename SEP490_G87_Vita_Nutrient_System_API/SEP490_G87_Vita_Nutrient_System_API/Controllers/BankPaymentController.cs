using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankPaymentController : ControllerBase
    {

        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///

        private IBankPaymentRepositories repositories = new BankPaymentRepositories();


        [HttpGet("APIGetAllRecentTransactions")]
        public async Task<ActionResult<Transaction>> APIGetAllRecentTransactions()
        {

            List<Transaction> data = await repositories.GetAllRecentTransactions();


            return Ok(data);

        }



    }
}
