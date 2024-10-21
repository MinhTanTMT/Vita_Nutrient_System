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
                    ArticleImages = article.ArticleImages.Select(img => new ArticleImageDTO
                    {
                        Id = img.Id,
                        ArticleId = img.ArticleId,
                        ImagePath = img.ImagePath
                    }).ToList()
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
                HeaderImage = article.HeaderImage,
                ArticleImages = article.ArticleImages.Select(img => new ArticleImageDTO
                {
                    Id = img.Id,
                    ArticleId = img.ArticleId,
                    ImagePath = img.ImagePath
                }).ToList()
            };
        }

        public async Task CreateArticleAsync(ArticlesNewsDTO articleDto)
        {
            var article = new ArticlesNews // Giả sử bạn có mô hình Article
            {
                UserId = articleDto.UserId,
                NameCreater = articleDto.NameCreater,
                Title = articleDto.Title,
                Content = articleDto.Content,
                IsActive = articleDto.IsActive,
                DateCreated = articleDto.DateCreated,
                HeaderImage = articleDto.HeaderImage,
                ArticleImages = articleDto.ArticleImages.Select(img => new ArticleImage
                {
                    ImagePath = img.ImagePath
                }).ToList()
            };

            await _context.ArticlesNews.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateArticleAsync(ArticlesNewsDTO articleDto)
        {
            var article = await _context.ArticlesNews.Include(a => a.ArticleImages)
                .FirstOrDefaultAsync(a => a.Id == articleDto.Id);

            if (article != null)
            {
                article.UserId = articleDto.UserId;
                article.NameCreater = articleDto.NameCreater;
                article.Title = articleDto.Title;
                article.Content = articleDto.Content;
                article.IsActive = articleDto.IsActive;
                article.DateCreated = articleDto.DateCreated;
                article.HeaderImage = articleDto.HeaderImage;

                // Cập nhật hình ảnh
                article.ArticleImages = articleDto.ArticleImages.Select(img => new ArticleImage
                {
                    ImagePath = img.ImagePath,
                    ArticleId = articleDto.Id
                }).ToList();

                await _context.SaveChangesAsync();
            }
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

        public Task AddArticleAsync(ArticlesNewsDTO article)
        {
            throw new NotImplementedException();
        }
    }
}
