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
    public class ExpertPackageController : ControllerBase
    {
        private readonly IExpertPackageRepositories repositories = new ExpertPackageRepositories();
        private readonly IMapper _mapper;

        public ExpertPackageController(IMapper mapper)
        {
            _mapper = mapper;
        }

        //get all package
        [HttpGet("GetAllExpertPackage")]
        public async Task<ActionResult> GetAllExpertPackage()
        {
            List<ExpertPackage> packages = repositories.GetExpertPackages();

            List<ExpertPackageResponse> result = packages.Select(p => _mapper.Map<ExpertPackageResponse>(p)).ToList();

            return Ok(result);
        }
        //get package details
        [HttpGet("GetExpertPackage/{packageId}")]
        public async Task<ActionResult> GetExpertPackage(short packageId)
        {
            ExpertPackage package = repositories.GetExpertPackage(packageId);

            return Ok(_mapper.Map<ExpertPackageResponse>(package));
        }
        //edit package

        //add package

        //delete package
    }
}
