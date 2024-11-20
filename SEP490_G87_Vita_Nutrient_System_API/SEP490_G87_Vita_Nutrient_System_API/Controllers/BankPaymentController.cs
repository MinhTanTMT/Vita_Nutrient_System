using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Threading;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            IEnumerable<Transaction> data = await repositories.GetAllRecentTransactions();
            return Ok(data);
        }


        [HttpGet("APIGetQRPayDefaultSystem")]
        public async Task<ActionResult<string>> APIGetQRPayDefaultSystem(int? idBankInformation, decimal amount, string content)
        {
            return Ok(await repositories.GetQRPayImage(idBankInformation, amount, content));
        }


        [HttpGet("APITest")]
        public async Task<ActionResult<string>> APITest()
        {

            return Ok(await repositories.GetTheLastTransactionsOfBankAccountNumber("0569000899", 20));

        }


        [HttpGet("APICheckQRPaySuccessful")]
        public async Task<ActionResult<dynamic>> APICheckQRPaySuccessful(string accountNumber, int limit, string content, decimal amountIn)
        {
            //if (await repositories.CheckQRPaySuccessfulByContent(accountNumber, limit, content, amountIn))
            //{

            //    return Ok("Successful");
            //}
            //else
            //{

            //    return BadRequest("Error");
            //}

            return Ok("Successful");
        }
        
        
        [HttpGet("APIGetAllTransactionsSystemOfMonth")]
        public async Task<ActionResult<IEnumerable<TransactionsSystem>>> APIGetAllTransactionsSystemOfMonth(int month, int year, int userMainId)
        {
            IEnumerable<TransactionsSystem> transactionsSystems = await repositories.GetAllTransactionsSystemOfMonth( month, year, userMainId);

            if (transactionsSystems.Count() > 0)
            {

                return Ok(transactionsSystems);
            }
            else
            {
                List<TransactionsSystem> transactionsSystemsNull = new List<TransactionsSystem>();
                return Ok(transactionsSystemsNull);
            }
        }
        
        
        [HttpGet("APIGetAllTransactionsSystemForGraphData")]
        public async Task<ActionResult<dynamic>> APIGetAllTransactionsSystemForGraphData(int year, int userMainId)
        {

            Decimal[][] graphData = await repositories.GetAllTransactionsSystemForGraphData(year, userMainId);

            return Ok(graphData);
        }

        [HttpGet("APISendMail")]
        public async Task<ActionResult<dynamic>> APISendMail()
        {
            return Ok(repositories.SendMail());
        }



        [HttpGet("APIGetAllNutritionistServices")]
        public async Task<ActionResult<IEnumerable<ExpertPackage>>> APIGetAllNutritionistServices()
        {
            BankPaymentRepositories bankPaymentRepositories = new BankPaymentRepositories();

            return Ok(await bankPaymentRepositories.GetAllNutritionistServices());
        }




        [HttpPost("APIInsertPaidPersonData")]
        public async Task<IActionResult> APIInsertPaidPersonData(
        [FromBody] UserListManagementDTO userListManagement,
        [FromQuery] int typeInsert)
        {
            BankPaymentRepositories bankPaymentRepositories = new BankPaymentRepositories();
            return Ok(await bankPaymentRepositories.InsertPaidPersonData(userListManagement, typeInsert));
        }

    }
}
