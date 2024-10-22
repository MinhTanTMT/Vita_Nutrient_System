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
                    HeaderImage = article.HeaderImage
                    /*ArticleImages = article.ArticleImages.Select(img => new ArticleImageDTO
                    {
                        Id = img.Id,
                        ArticleId = img.ArticleId,
                        ImagePath = img.ImagePath
                    }).ToList()*/
                })
                .ToListAsync();
        }

        public async Task<ArticlesNewsDTO> GetArticleByIdAsync(int id)
        {
            var article = await _context.ArticlesNews
                .Include(a => a.ArticleImages)
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
                HeaderImage = article.HeaderImage
                /*ArticleImages = article.ArticleImages.Select(img => new ArticleImageDTO
                {
                    Id = img.Id,
                    ArticleId = img.ArticleId,
                    ImagePath = img.ImagePath
                }).ToList()*/
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
            var article = await _context.ArticlesNews.Include(a => a.ArticleImages)
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
            article.HeaderImage = articleDto.HeaderImage;

            /*// Cập nhật danh sách hình ảnh
            article.ArticleImages.Clear();
            foreach (var imgDto in articleDto.ArticleImages)
            {
                article.ArticleImages.Add(new ArticleImage
                {
                    ImagePath = imgDto.ImagePath,
                    ArticleId = articleDto.Id
                });
            }*/

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

    }
}
