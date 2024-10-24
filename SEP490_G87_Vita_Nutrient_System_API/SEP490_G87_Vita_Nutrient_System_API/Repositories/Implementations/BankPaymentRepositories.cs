﻿using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using System.ComponentModel;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class BankPaymentRepositories : IBankPaymentRepositories
    {
        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();
        private readonly HttpClient clientBank = null;
        private readonly HttpClient clientQRBank = null;
        private readonly string KeyBank;
        private readonly string ValueBank;
        private readonly int QRPayDefaultSystem;

        public BankPaymentRepositories()
        {

            try
            {
                var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                QRPayDefaultSystem = configuration.GetValue<int>("QRPayDefaultSystem");
                KeyBank = configuration.GetValue<string>("KeyBank");
                ValueBank = configuration.GetValue<string>("ValueBank");
                Uri URIBase = configuration.GetValue<Uri>("myBankUri");
                Uri URIQRBase = configuration.GetValue<Uri>("myBankQRUri");
                clientBank = new HttpClient();
                clientQRBank = new HttpClient();
                clientBank.BaseAddress = URIBase;
                clientQRBank.BaseAddress = URIQRBase;
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                clientBank.DefaultRequestHeaders.Accept.Add(contentType);
            }
            catch (Exception ex)
            {

            }
        }


        public async Task<IEnumerable<Transaction>> GetAllRecentTransactions()
        {

            try
            {
                clientBank.DefaultRequestHeaders.Clear();
                clientBank.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(KeyBank, ValueBank);
                clientBank.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await clientBank.GetAsync(clientBank.BaseAddress + "/userapi/transactions/list");

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(data);
                    IEnumerable<Transaction> listTransaction = apiResponse.transactions.ToList();
                    return listTransaction; ;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }



        }

        public async Task<IEnumerable<Transaction>> GetTransactionsFromDateToDate(DateTime startDate, DateTime endDate)
        {


            try
            {
                clientBank.DefaultRequestHeaders.Clear();
                clientBank.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(KeyBank, ValueBank);
                clientBank.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await clientBank.GetAsync(clientBank.BaseAddress + $"/userapi/transactions/list?transaction_date_min={startDate.ToString("yyyy-MM-ddTHH:mm:ss")}&transaction_date_max={endDate.ToString("yyyy-MM-ddTHH:mm:ss")}");

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(data);
                    IEnumerable<Transaction> listTransaction = apiResponse.transactions.ToList();
                    return listTransaction; ;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }



        }
        public async Task<IEnumerable<Transaction>> GetTransactionFromIDBack(int idTransaction)
        {


            try
            {
                clientBank.DefaultRequestHeaders.Clear();
                clientBank.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(KeyBank, ValueBank);
                clientBank.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await clientBank.GetAsync(clientBank.BaseAddress + $"/userapi/transactions/list?since_id={idTransaction}");

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(data);
                    IEnumerable<Transaction> listTransaction = apiResponse.transactions.ToList();
                    return listTransaction; ;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public async Task<IEnumerable<Transaction>> GetTheLastTransactionsOfBankAccountNumber(string accountNumber, int limit)
        {


            try
            {
                clientBank.DefaultRequestHeaders.Clear();
                clientBank.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(KeyBank, ValueBank);
                clientBank.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await clientBank.GetAsync(clientBank.BaseAddress + $"/userapi/transactions/list?account_number={accountNumber}&limit={limit.ToString()}");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(data);
                    IEnumerable<Transaction> listTransaction = apiResponse.transactions.ToList();
                    return listTransaction; ;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        public async Task<dynamic> CheckQRPaySuccessfulByContent(string accountNumber, int limit, string content, decimal amountIn)
        {
            try
            {

                IEnumerable<Transaction> dataTransaction = await GetTheLastTransactionsOfBankAccountNumber(accountNumber, limit);
                TransactionsSystem dataTransactionsSystem = await _context.TransactionsSystems.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.TransactionContent.Equals(content) && x.AmountIn == amountIn);
                if (dataTransactionsSystem != null && dataTransaction != null)
                {
                    Transaction contentCompare = dataTransaction.FirstOrDefault(x => x.transaction_content.Equals(dataTransactionsSystem.TransactionContent) && Decimal.Parse(x.amount_in) == dataTransactionsSystem.AmountIn);
                    if (contentCompare != null)
                    {
                        TransactionsSystem intsertData = await _context.TransactionsSystems.FindAsync(dataTransactionsSystem.Id);
                        if (intsertData != null)
                        {
                            intsertData.Apitransactions = Int32.Parse(contentCompare.id);
                            intsertData.BankBrandName = contentCompare.bank_brand_name;
                            intsertData.AccountNumber = contentCompare.account_number;
                            intsertData.TransactionDate = DateTime.Parse(contentCompare.transaction_date);
                            intsertData.AmountOut = decimal.Parse(contentCompare.amount_out);
                            intsertData.Accumulated = decimal.Parse(contentCompare.accumulated);
                            intsertData.ReferenceNumber = contentCompare.reference_number;
                            intsertData.Code = Convert.ToString(contentCompare.code);
                            intsertData.SubAccount = Convert.ToString(contentCompare.sub_account);
                            intsertData.BankAccountId = Int32.Parse(contentCompare.bank_account_id);
                            intsertData.Status = true;
                        }
                        else
                        {
                            return false;
                        }
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Transaction>> GetTransactionsWithTheTransferredAmount(int amountIn)
        {

            try
            {
                clientBank.DefaultRequestHeaders.Clear();
                clientBank.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(KeyBank, ValueBank);
                clientBank.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await clientBank.GetAsync(clientBank.BaseAddress + $"/userapi/transactions/list?amount_in={amountIn}");

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(data);
                    IEnumerable<Transaction> listTransaction = apiResponse.transactions.ToList();
                    return listTransaction; ;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public async Task<IEnumerable<Transaction>> GetDetailsOfATransaction(int idTransaction)
        {


            try
            {
                clientBank.DefaultRequestHeaders.Clear();
                clientBank.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(KeyBank, ValueBank);
                clientBank.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await clientBank.GetAsync(clientBank.BaseAddress + $"/userapi/transactions/details/{idTransaction}");

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(data);
                    IEnumerable<Transaction> listTransaction = apiResponse.transactions.ToList();
                    return listTransaction; ;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }



        }
        
        
        public async Task<string> GetQRPayImage(int? idBankInformation, decimal amount, string content)
        {


            try
            {
                if (idBankInformation != null)
                {
                    BankInformation bankInformation = _context.BankInformations.Find(idBankInformation);
                    if (bankInformation != null)
                    {
                        string linkQRPayDefaultSystem = clientQRBank.BaseAddress.ToString() + $"img?acc={bankInformation.BankAccount.ToString()}&bank={bankInformation.TypeBank.ToString()}&amount={amount.ToString()}&des={content.ToString()}";
                        return linkQRPayDefaultSystem;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    BankInformation bankInformation = _context.BankInformations.Find(QRPayDefaultSystem);
                    string linkQRPayDefaultSystem = clientQRBank.BaseAddress.ToString() + $"img?acc={bankInformation.BankAccount.ToString()}&bank={bankInformation.TypeBank.ToString()}&amount={amount.ToString()}&des={content.ToString()}";
                    return linkQRPayDefaultSystem;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<IEnumerable<TransactionsSystem>> GetAllTransactionsSystemOfMonth(int month, int year, int userMainId)
        {

            try
            {
                IEnumerable<TransactionsSystem> transactionsSystem = _context.TransactionsSystems.Where(x => ((x.PayeeId == userMainId) || (x.UserPayId == userMainId)) && x.TransactionDate.Value.Month == month && x.TransactionDate.Value.Year == year).ToList();
                return transactionsSystem;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<Decimal[][]> GetAllTransactionsSystemForGraphData(int year, int userMainId)
        {
            Decimal[][] graphData = new Decimal[3][];
            graphData[0] = new Decimal[12];
            graphData[1] = new Decimal[12];
            graphData[2] = new Decimal[12];

            try
            {
                IEnumerable<TransactionsSystem> transactionsSystem = _context.TransactionsSystems.Where(x => ((x.PayeeId == userMainId) || (x.UserPayId == userMainId)) && x.TransactionDate.Value.Year == year).ToList();

                for (int getMonth = 0; getMonth < 12; getMonth++)
                {
                    IEnumerable<TransactionsSystem> dataSelectFromRoot = transactionsSystem.Where(x => x.TransactionDate.Value.Month == (getMonth + 1));

                    if (dataSelectFromRoot.Count() > 0)
                    {
                        Decimal MoneyInThisMonth = dataSelectFromRoot.Sum(t => t.AmountIn ?? 0);
                        Decimal MoneyOutThisMonth = dataSelectFromRoot.Sum(x => x.AmountOut ?? 0);
                        Decimal BalanceThisMonth = MoneyInThisMonth - MoneyOutThisMonth;

                        graphData[0][getMonth] = MoneyInThisMonth;
                        graphData[1][getMonth] = MoneyOutThisMonth;
                        graphData[2][getMonth] = BalanceThisMonth;
                    }
                    else
                    {
                        graphData[0][getMonth] = 0;
                        graphData[1][getMonth] = 0;
                        graphData[2][getMonth] = 0;
                    }
                }
                return graphData;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<bool> SendMail()
        {
            Random random = new Random();
            int otp;
            otp = random.Next(100000, 1000000);
            var fromAddress = new MailAddress(File.ReadAllText(@"C:\Users\msi\Desktop\SEP490_G87\SEP490_G87\Email.txt"));//mail dung de gui ma otp
            var tpAddress = new MailAddress("minhtantmt2k2@gmail.com"); //mail dung dc nhan ma otp
            string frompass = File.ReadAllText(@"C:\Users\msi\Desktop\SEP490_G87\SEP490_G87\PassCode.txt");
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
            return true;
        }
    }
}
