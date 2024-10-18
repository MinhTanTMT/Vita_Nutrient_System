using Azure;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
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

        private readonly HttpClient clientBank = null;
        private readonly string KeyBank;
        private readonly string ValueBank;

        public BankPaymentRepositories()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            KeyBank = configuration.GetValue<string>("KeyBank");
            ValueBank = configuration.GetValue<string>("ValueBank");
            Uri URIBase = configuration.GetValue<Uri>("myBankUri");
            clientBank = new HttpClient();
            clientBank.BaseAddress = URIBase;
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

        public async Task<List<Transaction>> GetTheLastTransactionsOfBankAccountNumber(int accountNumber, int limit)
        {

            clientBank.DefaultRequestHeaders.Clear();
            clientBank.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(KeyBank, ValueBank);
            clientBank.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage res = await clientBank.GetAsync(clientBank.BaseAddress + $"/userapi/transactions/list?account_number={accountNumber}&limit={limit}");

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


    }
}
