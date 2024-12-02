using System.ComponentModel.DataAnnotations;

namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class ArticlesNews
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Tên tác giả là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên tác giả không được vượt quá 100 ký tự.")]
        public string? NameCreater { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được quá 200 ký tự.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Nội dung là bắt buộc.")]
        public string? Content { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? DateCreated { get; set; }

        [Url(ErrorMessage = "Hình ảnh tiêu đề phải là một URL hợp lệ.")]
        public string? HeaderImage { get; set; }

        public double? Rate { get; set; }

        public int? NumberRate { get; set; }
        public double? UserRate { get; set; }

    }
}
