using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyNoteController : ControllerBase
    {
        private IFoodRepositories repositories = new FoodRepositories();
        private readonly IMapper _mapper;

        public KeyNoteController(IMapper mapper) => _mapper = mapper;

        [HttpGet("GetKeyNotes")]
        public async Task<ActionResult<List<KeyNoteResponse>>> GetKeyNotes()
        {
            return repositories.GetKeynotes().Select(k => _mapper.Map<KeyNoteResponse>(k)).ToList();
        }

        [HttpGet("GetKeyNote/{id}")]
        public async Task<ActionResult<KeyNoteResponse>> GetKeyNotes(int id)
        {
            var keynote = repositories.GetKeynote(id);
            if (keynote is null) 
            {
                return NotFound("Keynote not found!");
            }
            else
            {
                return _mapper.Map<KeyNoteResponse>(keynote);
            }
        }
    }
}
