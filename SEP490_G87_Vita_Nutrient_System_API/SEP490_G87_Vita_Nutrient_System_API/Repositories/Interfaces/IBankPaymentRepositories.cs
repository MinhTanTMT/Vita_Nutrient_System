using SEP490_G87_Vita_Nutrient_System_API.Dtos;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IBankPaymentRepositories
    {

        Task<List<Transaction>> GetAllRecentTransactions();

        Task<List<Transaction>> GetTransactionsFromDateToDate(DateTime startDate, DateTime endDate);

    }
}
