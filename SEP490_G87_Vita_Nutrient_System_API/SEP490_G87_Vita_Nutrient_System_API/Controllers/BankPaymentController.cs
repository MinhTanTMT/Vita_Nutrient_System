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
            if (await repositories.CheckQRPaySuccessfulByContent(accountNumber, limit, content, amountIn))
            {

                return Ok("Successful");
            }
            else
            {

                return BadRequest("Error");
            }
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
                return BadRequest("Error");
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
            Random random = new Random();
            int otp;

            otp = random.Next(100000, 1000000);

            var fromAddress = new MailAddress("minhtantmt2k2@gmail.com");//mail dung de gui ma otp
            var tpAddress = new MailAddress("tantmhe161872@fpt.edu.vn"); //mail dung dc nhan ma otp
            const string frompass = "wwkp yrds qxvc pcvt";
            const string subject = "OPT code";
            string body = otp.ToString();

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, frompass),
                Timeout = 290009
            };
            using (var message = new MailMessage(fromAddress, tpAddress)
            {
                Subject = subject,
                Body = body,
            })
            {
                smtp.Send(message);
            }
            return Ok();
        }




    }
}
