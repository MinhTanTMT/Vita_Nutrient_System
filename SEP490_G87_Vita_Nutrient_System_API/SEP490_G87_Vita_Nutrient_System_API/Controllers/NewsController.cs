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
        private readonly IMapper _mapper;


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
            if (articleDto == null)
            {
                return BadRequest("Article data is required.");
            }

            await _newsRepositories.CreateArticleAsync(articleDto);
            return CreatedAtAction(nameof(GetArticleById), new { id = articleDto.Id }, articleDto);
        }

        // PUT: api/news/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateArticle(int id, [FromBody] ArticlesNewsDTO articleDto)
        {
            if (id != articleDto.Id)
            {
                return BadRequest("Article ID mismatch.");
            }

            await _newsRepositories.UpdateArticleAsync(articleDto);
            return NoContent();
        }

        // DELETE: api/news/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            await _newsRepositories.DeleteArticleAsync(id);
            return NoContent();
        }
    }
}
