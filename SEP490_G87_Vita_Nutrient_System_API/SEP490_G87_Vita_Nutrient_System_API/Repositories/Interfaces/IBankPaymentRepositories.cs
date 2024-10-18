using SEP490_G87_Vita_Nutrient_System_API.Dtos;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IBankPaymentRepositories
    {
        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///
        Task<List<Transaction>> GetAllRecentTransactions();
        Task<List<Transaction>> GetTransactionsFromDateToDate(DateTime startDate, DateTime endDate);
        Task<List<Transaction>> GetTransactionFromIDBack(int idTransaction);
        Task<List<Transaction>> GetTheLastTransactionsOfBankAccountNumber(string accountNumber, int limit);
        Task<List<Transaction>> GetTransactionsWithTheTransferredAmount(int amountIn);
        Task<List<Transaction>> GetDetailsOfATransaction(int idTransaction);
        Task<string> GetQRPayImage(int? idBankInformation, decimal amount, string content);
        Task<dynamic> CheckQRPaySuccessfulByContent(string accountNumber, int limit, string content, decimal amountIn);

    }
}
