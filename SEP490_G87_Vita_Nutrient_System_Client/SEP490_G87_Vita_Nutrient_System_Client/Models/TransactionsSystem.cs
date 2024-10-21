using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_Client.Models;

public partial class TransactionsSystem
{
    public int Id { get; set; }

    public int UserPayId { get; set; }

    public int PayeeId { get; set; }

    public int? Apitransactions { get; set; }

    public string? BankBrandName { get; set; }

    public string? AccountNumber { get; set; }

    public DateTime? TransactionDate { get; set; }

    public decimal? AmountOut { get; set; }

    public decimal? AmountIn { get; set; }

    public decimal? Accumulated { get; set; }

    public string? TransactionContent { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? Code { get; set; }

    public string? SubAccount { get; set; }

    public int? BankAccountId { get; set; }

    public bool? Status { get; set; }
}
