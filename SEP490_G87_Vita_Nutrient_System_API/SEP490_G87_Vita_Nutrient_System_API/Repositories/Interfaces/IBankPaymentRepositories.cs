using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IBankPaymentRepositories
    {
        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///
        Task<IEnumerable<Transaction>> GetAllRecentTransactions();
        Task<IEnumerable<Transaction>> GetTransactionsFromDateToDate(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Transaction>> GetTransactionFromIDBack(int idTransaction);
        Task<IEnumerable<Transaction>> GetTheLastTransactionsOfBankAccountNumber(string accountNumber, int limit);
        Task<IEnumerable<Transaction>> GetTransactionsWithTheTransferredAmount(int amountIn);
        Task<IEnumerable<Transaction>> GetDetailsOfATransaction(int idTransaction);
        Task<string> GetQRPayImage(int? idBankInformation, decimal amount, string content);
        Task<dynamic> CheckQRPaySuccessfulByContent(string accountNumber, int limit, string content, decimal amountIn);
        Task<IEnumerable<TransactionsSystem>> GetAllTransactionsSystemOfMonth(int month, int year, int userMainId);
        Task<Decimal[][]> GetAllTransactionsSystemForGraphData(int year, int userMainId);

    }
}
