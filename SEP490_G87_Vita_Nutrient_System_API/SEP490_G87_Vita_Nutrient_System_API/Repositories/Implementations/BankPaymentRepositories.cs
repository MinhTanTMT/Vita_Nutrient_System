using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using System.ComponentModel;
using System.Net.Http.Headers;
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


        public async Task<List<Transaction>> GetAllRecentTransactions()
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
                List<Transaction> listTransaction = apiResponse.transactions.ToList();
                return listTransaction; ;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Transaction>> GetTransactionsFromDateToDate(DateTime startDate, DateTime endDate)
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
                List<Transaction> listTransaction = apiResponse.transactions.ToList();
                return listTransaction; ;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<Transaction>> GetTransactionFromIDBack(int idTransaction)
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
                List<Transaction> listTransaction = apiResponse.transactions.ToList();
                return listTransaction; ;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Transaction>> GetTheLastTransactionsOfBankAccountNumber(string accountNumber, int limit)
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
                List<Transaction> listTransaction = apiResponse.transactions.ToList();
                return listTransaction; ;
            }
            else
            {
                return null;
            }
        }


        public async Task<dynamic> CheckQRPaySuccessfulByContent(string accountNumber, int limit, string content, decimal amountIn)
        {
            List<Transaction> dataTransaction = await GetTheLastTransactionsOfBankAccountNumber(accountNumber, limit);
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
                        //intsertData.AmountIn = decimal.Parse(contentCompare.amount_in),
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

        public async Task<List<Transaction>> GetTransactionsWithTheTransferredAmount(int amountIn)
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
                List<Transaction> listTransaction = apiResponse.transactions.ToList();
                return listTransaction; ;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Transaction>> GetDetailsOfATransaction(int idTransaction)
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
                List<Transaction> listTransaction = apiResponse.transactions.ToList();
                return listTransaction; ;
            }
            else
            {
                return null;
            }
        }
        
        
        public async Task<string> GetQRPayImage(int? idBankInformation, decimal amount, string content)
        {
            if(idBankInformation != null)
            {
                BankInformation bankInformation = _context.BankInformations.Find(idBankInformation);
                if(bankInformation != null)
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
    }
}
