using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels;
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
            
            List<ExpertPackageResponse1> result1 = new List<ExpertPackageResponse1>();
            
            for (int i = 0; i < result.Count; i++)
            {
                ExpertPackageResponse elm = result.ElementAt(i);
                ExpertPackageResponse1 e = new();
                e.Package = elm;
                List<User> nutritionists = repositories.GetUsersByPackage(elm.Id);
                e.Nutrititonists = nutritionists.Select(n => new ExpertPackageResponse1.User
                {
                    Id = n.UserId,
                    Name = n.FirstName + " " + n.LastName,
                    Account = n.Account
                }).ToList();

                result1.Add(e);
            }

            return Ok(result1);
        }

        //get package details
        [HttpGet("GetExpertPackage/{packageId}")]
        public async Task<ActionResult> GetExpertPackage(short packageId)
        {
            ExpertPackage package = repositories.GetExpertPackage(packageId);

            List<User> nutritionists = repositories.GetUsersByPackage(packageId);

            ExpertPackageResponse1 result = new ExpertPackageResponse1();
            result.Package = _mapper.Map<ExpertPackageResponse>(package);
            result.Nutrititonists = nutritionists.Select(n => new ExpertPackageResponse1.User
            {
                Id = n.UserId,
                Name = n.FirstName + " " + n.LastName,
                Account = n.Account
            }).ToList();

            return Ok(result);
        }


        //add package
        [HttpPost("AddExpertPackage")]
        public async Task<ActionResult> AddExpertPackage([FromBody] AddUpdatePackageRequest package)
        {
            ExpertPackage p = new ExpertPackage
            {
                Name = package.Name,
                Describe = package.Describe,
                Price = package.Price,
                Duration = package.Duration,
            };

            repositories.AddExpertPackage(p);

            return Ok("Add package successful!");
        }

        //edit package
        [HttpPut("UpdateExpertPackage")]
        public async Task<ActionResult> UpdateExpertPackage([FromBody] AddUpdatePackageRequest package)
        {
            ExpertPackage p = repositories.GetExpertPackage(package.Id);

            if(p is null)
            {
                return Ok("Package not found!");
            }
            else
            {
                p.Name = package.Name;
                p.Describe = package.Describe;
                p.Price = package.Price;
                p.Duration = package.Duration;

                repositories.UpdateExpertPackage(p);
                return Ok("Update package successful!");
            }
        }

        //delete package
        [HttpDelete("DeleteExpertPackage/{packageId}")]
        public async Task<ActionResult> DeleteExpertPackage(short packageId)
        {
            ExpertPackage p = repositories.GetExpertPackage(packageId);

            if (p is null)
            {
                return Ok("Package not found!");
            }
            else
            {
                try
                {
                    repositories.RemoveExpertPackage(packageId);
                    return Ok("Delete package successful!");
                }
                catch (Exception ex)
                {
                    return BadRequest("Cannot delete because there are nutritionists that have the package!");
                }
            }
        }
    }
}
