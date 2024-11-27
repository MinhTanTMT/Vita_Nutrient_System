using System;
using System.ComponentModel.DataAnnotations;

namespace SEP490_G87_Vita_Nutrient_System_Client.Domain.Attributes
{
    public class CompareDates : ValidationAttribute
    {
        private readonly string _startDateNutritionRoute;

        public CompareDates(string startDateNutritionRoute)
        {
            _startDateNutritionRoute = startDateNutritionRoute;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Lấy giá trị EndDate
            var endDate = value as DateTime?;

            // Tìm thuộc tính StartDate trong đối tượng
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDateNutritionRoute);
            if (startDateProperty == null)
            {
                return new ValidationResult($"Không tìm thấy thuộc tính '{_startDateNutritionRoute}'.");
            }

            // Lấy giá trị StartDate từ đối tượng
            var startDate = startDateProperty.GetValue(validationContext.ObjectInstance) as DateTime?;

            // So sánh EndDate và StartDate
            if (startDate.HasValue && endDate.HasValue && endDate < startDate)
            {
                // Chỉ gắn lỗi cho trường EndDate
                return new ValidationResult(ErrorMessage ?? "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.", new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
