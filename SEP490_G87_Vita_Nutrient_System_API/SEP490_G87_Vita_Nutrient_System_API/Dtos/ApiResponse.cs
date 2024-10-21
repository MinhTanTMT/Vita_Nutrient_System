namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class ApiResponse
    {
        public int status { get; set; }
        public object error { get; set; }
        public Messages messages { get; set; }
        public Transaction[] transactions { get; set; }
    }

    public class Messages
    {
        public bool success { get; set; }
    }

    public class Transaction
    {
        public string id { get; set; }
        public string bank_brand_name { get; set; }
        public string account_number { get; set; }
        public string transaction_date { get; set; }
        public string amount_out { get; set; }
        public string amount_in { get; set; }
        public string accumulated { get; set; }
        public string transaction_content { get; set; }
        public string reference_number { get; set; }
        public object code { get; set; }
        public object sub_account { get; set; }
        public string bank_account_id { get; set; }
    }
}
