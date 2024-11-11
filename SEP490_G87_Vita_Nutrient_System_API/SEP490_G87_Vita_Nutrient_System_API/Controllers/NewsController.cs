using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {

        private readonly INewsRepositories _newsRepositories = new NewsRepositories();

        // GET: api/news
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticlesNewsDTO>>> GetAllArticles()
        {
            var articles = await _newsRepositories.GetAllArticlesAsync();
            return Ok(articles);
        }

        // GET: api/news/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticlesNewsDTO>> GetArticleById(int id)
        {
            var article = await _newsRepositories.GetArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        // POST: api/news
        [HttpPost]
        public async Task<ActionResult> CreateArticle([FromBody] ArticlesNewsDTO articleDto)
        {
            if (articleDto == null || !ModelState.IsValid)
            {
                return BadRequest("Dữ liệu bài viết không hợp lệ.");
            }

            if (articleDto.UserId == 0)
            {
                return BadRequest("UserId không hợp lệ.");
            }

            await _newsRepositories.CreateArticleAsync(articleDto);
            return Ok();
        }



        // PUT: api/news/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateArticle(int id, [FromBody] ArticlesNewsDTO articleDto)
        {
            if (id != articleDto.Id)
            {
                return BadRequest("Article ID mismatch.");
            }

            try
            {
                await _newsRepositories.UpdateArticleAsync(articleDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Không tìm thấy bài viết.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật bài viết: {ex.Message}");
            }

            return Ok();
        }


        // DELETE: api/news/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            await _newsRepositories.DeleteArticleAsync(id);
            return Ok();
        }

        [HttpPost("{articleId}/evaluations")]
        public async Task<ActionResult> AddOrUpdateEvaluation(int articleId, [FromBody] NewsEvaluationDTO evaluationDto)
        {
            if (evaluationDto == null || evaluationDto.ArticlesNewsId != articleId || !ModelState.IsValid)
            {
                return BadRequest("Dữ liệu đánh giá không hợp lệ.");
            }

            try
            {
                await _newsRepositories.AddOrUpdateEvaluationAsync(evaluationDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xử lý đánh giá: {ex.Message}");
            }
        }



        [HttpGet("{articleId}/evaluations")]
        public async Task<ActionResult<IEnumerable<NewsEvaluationDTO>>> GetEvaluations(int articleId)
        {
            var evaluations = await _newsRepositories.GetEvaluationsByArticleIdAsync(articleId);
            return Ok(evaluations);
        }
    }
}
