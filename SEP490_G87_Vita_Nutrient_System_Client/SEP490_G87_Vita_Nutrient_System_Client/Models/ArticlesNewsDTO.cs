using System.ComponentModel.DataAnnotations;

namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class ArticlesNewsDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Người tạo là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên người tạo không được quá 50 ký tự")]
        public string? NameCreater { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Nội dung là bắt buộc")]
        [MinLength(20, ErrorMessage = "Nội dung phải có ít nhất 20 ký tự")]
        public string? Content { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? DateCreated { get; set; }

        public string? HeaderImage { get; set; }
       
    }
}
