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
    public class DiseaseController : ControllerBase
    {
        private IDiseaseRepositories repositories = new DiseaseRepositories();
        private readonly IMapper _mapper;

        public DiseaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("GetAllDiseases")]
        public async Task<ActionResult> GetAllDiseases()
        {
            List<ListOfDisease> diseases = repositories.GetAllDiseases();

            List<DiseaseResponse> result = diseases.Select(d => _mapper.Map<DiseaseResponse>(d)).ToList();

            return Ok(result);
        }

        [HttpGet("GetDisease/{Id}")]
        public async Task<ActionResult> GetDisease(int Id)
        {
            ListOfDisease disease = repositories.GetDisease(Id);
            DiseaseResponse result = _mapper.Map<DiseaseResponse>(disease);
            return Ok(result);
        }
    }
}
