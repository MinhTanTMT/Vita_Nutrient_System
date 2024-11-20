using SEP490_G87_Vita_Nutrient_System_Client.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class NutritionRoute
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CreateById { get; set; }

        [StringLength(100, ErrorMessage = "Tên lộ trình không được vượt quá 100 ký tự.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string? Describe { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc.")]
        [DataType(DataType.Date, ErrorMessage = "Ngày bắt đầu không hợp lệ.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc.")]
        [DataType(DataType.Date, ErrorMessage = "Ngày kết thúc không hợp lệ.")]
        [CompareDates("StartDate", ErrorMessage = "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.")]
        public DateTime? EndDate { get; set; }
        public bool? IsDone { get; set; }
        public string? FullName { get; set; }       
        public string? CreateByName { get; set; }
        public string? UrlImage { get; set; }
    }
}
