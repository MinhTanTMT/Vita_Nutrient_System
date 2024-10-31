using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class NewsRepositories : INewsRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();


        public NewsRepositories()
        {

        }


        public async Task<IEnumerable<ArticlesNewsDTO>> GetAllArticlesAsync()
        {
            return await _context.ArticlesNews
                .Select(article => new ArticlesNewsDTO
                {
                    Id = article.Id,
                    UserId = article.UserId,
                    NameCreater = article.NameCreater,
                    Title = article.Title,
                    Content = article.Content,
                    IsActive = article.IsActive,
                    DateCreated = article.DateCreated,
                    HeaderImage = article.HeaderImage,
                    Rate = article.Rate,
                    NumberRate = article.NumberRate
                })
                .ToListAsync();
        }

        public async Task<ArticlesNewsDTO> GetArticleByIdAsync(int id)
        {
            var article = await _context.ArticlesNews
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null) return null;

            return new ArticlesNewsDTO
            {
                Id = article.Id,
                UserId = article.UserId,
                NameCreater = article.NameCreater,
                Title = article.Title,
                Content = article.Content,
                IsActive = article.IsActive,
                DateCreated = article.DateCreated,
                HeaderImage = article.HeaderImage,
                Rate = article.Rate,
                NumberRate = article.NumberRate
            };
        }

        public async Task CreateArticleAsync(ArticlesNewsDTO articleDto)
        {
            try
            {
                var article = new ArticlesNews
                {
                    UserId = articleDto.UserId,
                    NameCreater = articleDto.NameCreater,
                    Title = articleDto.Title,
                    Content = articleDto.Content,
                    IsActive = articleDto.IsActive ?? true, // Giá trị mặc định là true nếu không có
                    DateCreated = articleDto.DateCreated ?? DateTime.Now, // Sử dụng ngày hiện tại nếu không có giá trị
                    HeaderImage = articleDto.HeaderImage
                };

                await _context.ArticlesNews.AddAsync(article);
                await _context.SaveChangesAsync();

                // Gán lại ID sau khi lưu thành công để trả về phản hồi chính xác
                articleDto.Id = article.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tạo bài viết: {ex.Message}");
            }
        }


        public async Task UpdateArticleAsync(ArticlesNewsDTO articleDto)
        {
            var article = await _context.ArticlesNews
                .FirstOrDefaultAsync(a => a.Id == articleDto.Id);

            if (article == null)
            {
                throw new KeyNotFoundException("Không tìm thấy bài viết.");
            }

            // Cập nhật các thuộc tính
            article.UserId = articleDto.UserId;
            article.NameCreater = articleDto.NameCreater;
            article.Title = articleDto.Title;
            article.Content = articleDto.Content;
            article.IsActive = articleDto.IsActive;
            article.DateCreated = articleDto.DateCreated;
            // Chỉ cập nhật HeaderImage nếu có đường dẫn mới
            if (!string.IsNullOrEmpty(articleDto.HeaderImage))
            {
                article.HeaderImage = articleDto.HeaderImage;
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(int id)
        {
            var article = await _context.ArticlesNews.FindAsync(id);
            if (article != null)
            {
                _context.ArticlesNews.Remove(article);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddEvaluationAsync(NewsEvaluationDTO evaluationDto)
        {
            var evaluation = new NewsEvaluation
            {
                ArticlesNewsId = evaluationDto.ArticlesNewsId,
                UserId = evaluationDto.UserId,
                Ratting = evaluationDto.Ratting
            };

            await _context.NewsEvaluations.AddAsync(evaluation);
            await _context.SaveChangesAsync();

            // Cập nhật số lượng đánh giá và điểm đánh giá trung bình cho bài viết
            var article = await _context.ArticlesNews.FirstOrDefaultAsync(a => a.Id == evaluationDto.ArticlesNewsId);
            if (article != null)
            {
                // Tính lại số lượng đánh giá và điểm đánh giá trung bình
                article.NumberRate = (article.NumberRate ?? 0) + 1;
                article.Rate = ((article.Rate ?? 0) * (article.NumberRate - 1) + evaluationDto.Ratting) / article.NumberRate;

                // Lưu thay đổi
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<NewsEvaluationDTO>> GetEvaluationsByArticleIdAsync(int articleId)
        {
            return await _context.NewsEvaluations
                .Where(e => e.ArticlesNewsId == articleId)
                .Select(e => new NewsEvaluationDTO
                {
                    Id = e.Id,
                    ArticlesNewsId = e.ArticlesNewsId,
                    UserId = e.UserId,
                    Ratting = e.Ratting
                })
                .ToListAsync();
        }
    }
}
