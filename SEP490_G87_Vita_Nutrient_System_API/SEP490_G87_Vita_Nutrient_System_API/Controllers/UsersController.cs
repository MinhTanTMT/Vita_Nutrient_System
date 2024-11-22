using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels;
using SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using UserRole = SEP490_G87_Vita_Nutrient_System_API.Domain.Enums.UserRole;
namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IUserDetailsRepository _repositories = new UserDetailsRepository();
        private IUserRepositories repositories = new UsersRepositories();
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("Login")]
        public async Task<ActionResult<User>> APIGetUserLogin(string account, string password)
        {
            var dataReturn = repositories.GetUserLogin(account, password);
            if (dataReturn == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dataReturn);
            }
        }



        [HttpGet("checkExit")]
        public async Task<ActionResult<User>> APIGetUserByAccount(string account)
        {
            var dataReturn = repositories.CheckExitAccountUser(account);

            if (dataReturn)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }


        [HttpPost("Register")]
        public async Task<ActionResult<User>> GetUserRegister(User user)
        {
            var dataReturn = repositories.GetUserRegister(user);
            return Ok(user);
        }


        [HttpPost("RegisterLoginGoogle")]
        public async Task<ActionResult<User>> APIGetRegisterLoginGoogle(User user)
        {
            var dataReturn = repositories.GetRegisterLoginGoogle(user);
            return Ok(dataReturn);
        }



        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {

            var dataReturn = repositories.GetUserById(id);

            if (dataReturn == null)
            {
                return NotFound();
            }

            return Ok(dataReturn);
        }

        [HttpGet("GetAllUser")]
        public async Task<ActionResult<List<CommonUserResponse>>> GetAllUsers()
        {
            var users = repositories.GetAllUsers();

            var result = users.Select(
                u => _mapper.Map<CommonUserResponse>(u)).ToList();

            return Ok(result);
        }

        [HttpGet("GetUserByRole/{roleId}")]
        public async Task<ActionResult<List<CommonUserResponse>>> GetUsersByRole(int roleId)
        {
            //kiem tra xem roleId duoc truyen vao co hop le hay khong
            if (!Enum.IsDefined(typeof(UserRole), roleId))
            {
                return BadRequest("Invalid role id!");
            }

            var users = repositories.GetUsersByRole(roleId);

            var result = users.Select(
                u => _mapper.Map<CommonUserResponse>(u)).ToList();

            return Ok(result);
        }

        [HttpGet("GetUserDetail/{id}")]
        public async Task<ActionResult<dynamic>> GetUserDetail(int id)
        {
            User user = repositories.GetUserDetailsInfo(id);

            if (user == null)
            {
                return BadRequest("User not found!");
            }

            UserDetailResponse result = _mapper.Map<UserDetailResponse>(user);

            return Ok(result);
        }
        [HttpGet("GetOnlyUserDetail/{userId}")]
        public async Task<IActionResult> GetOnlyUserDetail(int userId)
        {
            var userDetail = await repositories.GetUserDetailByUserIdAsync(userId);

            if (userDetail == null)
            {
                return NotFound(new { message = "UserDetail not found" });
            }

            return Ok(userDetail);
        }
        [HttpGet("GetNutritionistDetail/{id}")]
        public async Task<ActionResult<dynamic>> GetNutritionistDetail(int id)
        {
            var nutritionist = repositories.GetNutritionistDetailsInfo(id);

            if (nutritionist == null)
            {
                return BadRequest("Nutritionist not found!");
            }

            var result = _mapper.Map<NutritionistDetailResponse>(nutritionist);

            return Ok(result);
        }

        [HttpGet("GetNutritionistPackages/{id}")]
        public async Task<ActionResult<List<ExpertPackageResponse>>> GetNutritionistPackages(int id)
        {
            List<ExpertPackage> packages = repositories.GetNutritionistPackages(id).ToList();

            var result = packages
                .Select(p => _mapper.Map<ExpertPackageResponse>(p))
                .ToList();

            return Ok(result);
        }
        [HttpPost("UpdateUserPhysicalStatistics")]
        public async Task<IActionResult> UpdateUserPhysicalStatistics([FromBody] UserPhysicalStatisticsDTO userDetails)
        {
            if (userDetails == null || userDetails.UserId <= 0)
            {
                return BadRequest(new { message = "Invalid user details data." });
            }

            await _repositories.SaveUserDetails(userDetails);
            return Ok(new { message = "User details updated successfully." });
        }

        [HttpPost("UpdateUserStatus")]
        public async Task<ActionResult<string>> UpdateUserStatus([FromBody] UpdateUserStatusRequest request)
        {
            User u = repositories.GetUserById(request.UserId);
            //kiem tra xem user ton tai hay ko
            if (u == null)
            {
                return BadRequest("User not found!");
            }

            u.IsActive = request.Status;
            repositories.UpdateUser(u);

            return Ok("Update user status successfully!");
        }

        [HttpGet("{userId}/liked-foods")]
        public async Task<ActionResult<dynamic>> GetLikedFoods(int userId, [FromQuery] GetLikeFoodDTO model)
        {

            return Ok(await repositories.GetLikedFoods(userId, model));
        }

        [HttpPost("{userId}/unlike-food/{foodId}")]
        public async Task<IActionResult> UnlikeFood(int userId, int foodId)
        {
            User u = repositories.GetUserById(userId);
            // Check if user exists
            if (u == null)
            {
                return BadRequest("User not found!");
            }
            // Await the result of LikeOrUnlikeFood
            var result = await repositories.LikeOrUnlikeFood(userId, foodId);
            return Ok(result);
        }

        [HttpPost("{userId}/unblock-food/{foodId}")]
        public async Task<IActionResult> UnblockFood(int userId, int foodId)
        {
            User u = repositories.GetUserById(userId);
            //kiem tra xem user ton tai hay ko
            if (u == null)
            {
                return BadRequest("User not found!");
            }
            repositories.UnblockFood(userId, foodId);
            return NoContent();
        }

        [HttpGet("{userId}/blocked-foods")]
        public async Task<IActionResult> GetBlockedFoods(int userId, [FromQuery] GetLikeFoodDTO model)
        {
            var paginatedFoods = await repositories.GetBlockedFoods(userId, model);

            return Ok(paginatedFoods);
        }
    }
}
