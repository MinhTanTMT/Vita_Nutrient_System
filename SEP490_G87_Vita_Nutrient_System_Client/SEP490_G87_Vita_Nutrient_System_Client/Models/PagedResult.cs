namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
