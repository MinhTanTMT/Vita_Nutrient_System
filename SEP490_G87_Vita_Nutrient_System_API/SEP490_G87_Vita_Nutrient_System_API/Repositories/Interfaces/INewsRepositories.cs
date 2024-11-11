using System.Collections.Generic;
using System.Threading.Tasks;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories
{
    public interface INewsRepositories
    {
        Task<IEnumerable<ArticlesNewsDTO>> GetAllArticlesAsync();
        Task<ArticlesNewsDTO> GetArticleByIdAsync(int id);
        Task CreateArticleAsync(ArticlesNewsDTO article);
        Task UpdateArticleAsync(ArticlesNewsDTO article);
        Task DeleteArticleAsync(int id);
        Task AddOrUpdateEvaluationAsync(NewsEvaluationDTO evaluation);
        Task<IEnumerable<NewsEvaluationDTO>> GetEvaluationsByArticleIdAsync(int articleId);
    }
}
