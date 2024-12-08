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


        [HttpGet("APIForgotPassword")]
        public async Task<ActionResult> APIForgotPassword(string emailGoogle)
        {

            if (await repositories.ForgotPassword(emailGoogle))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("APIGoogleAccountVerificationCode")]
        public async Task<ActionResult> APIGoogleAccountVerificationCode(string emailGoogle, string verificationCode)
        {
            //public async Task<bool> SendMail(string emailAccount, string subject, string contentSend)

            return Ok(await repositories.SendMail(emailGoogle, "Mã xác nhận", verificationCode));
        }


        [HttpGet("Login")]
        public async Task<ActionResult<UserLoginRegister>> APIGetUserLogin(string account, string password)
        {
            var dataReturn = await repositories.GetUserLogin(account, password);
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
        public async Task<ActionResult<User>> APIGetUserByAccount(string account, string accGoogle)
        {
            var dataReturn = await repositories.CheckAccountUserNull(account, accGoogle);

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
        public async Task<ActionResult<UserLoginRegister>> GetUserRegister(UserLoginRegister user)
        {
            var dataReturn = await repositories.GetUserRegister(user);
            return Ok(user);
        }


        [HttpPost("RegisterLoginGoogle")]
        public async Task<ActionResult<UserLoginRegister>> APIGetRegisterLoginGoogle(UserLoginRegister user)
        {
            var dataReturn = await repositories.GetRegisterLoginGoogle(user);
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



        [HttpGet("GetUserPhysicalStatisticsDTOByUserIdAsync/{userId}")]
        public async Task<IActionResult> GetUserPhysicalStatisticsDTOByUserIdAsync(int userId)
        {
            var userDetail = await repositories.GetUserPhysicalStatisticsDTOByUserIdAsync(userId);

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
        public async Task<ActionResult<ExpertPackageResponse>> GetNutritionistPackages(short id)
        {
            ExpertPackage packages = repositories.GetNutritionistPackages(id);

            var result = _mapper.Map<ExpertPackageResponse>(packages);

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



        [HttpPost("UpdateUserWeightGoal")]
        public async Task<IActionResult> UpdateUserWeightGoal([FromBody] UserPhysicalStatisticsDTO userDetails)
        {
            if (userDetails == null || userDetails.UserId <= 0)
            {
                return BadRequest(new { message = "Invalid user details data." });
            }

            await _repositories.SaveUserWeightGoal(userDetails);
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

        [HttpPost("UpdateUserInfo")]
        public async Task<ActionResult<string>> UpdateUserInfo([FromBody] UpdateUserInfoRequest request)
        {
            User u = repositories.GetUserById(request.UserId);
            //kiem tra xem user ton tai hay ko
            if (u == null)
            {
                return BadRequest("User not found!");
            }

            u.FirstName = request.FirstName;
            u.LastName = request.LastName;
            u.Dob = request.DOB ?? new DateTime(2000, 01, 01);
            u.Gender = request.Gender;
            u.Address = request.Address;
            u.Phone = request.Phone ?? "";
            repositories.UpdateUser(u);

            return Ok("Update user status successfully!");
        }

        // mới chưa test
        [HttpGet("UpdateUserRole/{userId}/{userRole}")]
        public async Task<ActionResult<string>> UpdateUserRole(int userId, short userRole)
        {
            User u = repositories.GetUserById(userId);
            //kiem tra xem user ton tai hay ko
            if (u == null)
            {
                return BadRequest("User not found!");
            }

            u.Role = userRole;
            repositories.UpdateUser(u);

            return Ok("Update user role successfully!");
        }

        // mới chưa test
        [HttpPost("UpdateUserAvatar")]
        public async Task<ActionResult<string>> UpdateUserAvatar([FromBody]UploadAvatarRequest request)
        {
            User u = repositories.GetUserById(request.UserId);
            //kiem tra xem user ton tai hay ko
            if (u == null)
            {
                return BadRequest("User not found!");
            }

            u.Urlimage = request.ImageURL;
            repositories.UpdateUser(u);

            return Ok("Update user avatar successfully!");
        }

        [HttpPut("ChangePassword")]
        public async Task<ActionResult<string>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            User u = repositories.GetUserById(request.UserId);
            //kiem tra xem user ton tai hay ko
            if (u == null)
            {
                return BadRequest("User not found!");
            }

            if(request.NewPassword.Trim().Length == 0)
            {
                return BadRequest("New password invalid!");
            }

            //kiem tra password cu~
            bool verifyPw = await repositories.VerifyPassword(request.OldPassword, u.Password);
            if (!verifyPw)
            {
                return BadRequest("Old password wrong!");
            }

            //kiem tra confirm password va new password
            if(request.NewPassword.Length < 6 || request.NewPassword.Length > 50)
            {
                return BadRequest("New password must contain 6-50 characters!");
            }

            if (!request.NewPassword.Equals(request.ConfirmPassword))
            {
                return BadRequest("Confirm password not match!");
            }

            u.Password = await repositories.EncryptPassword(request.NewPassword);

            repositories.UpdateUser(u);

            return Ok("Change password successfully!");
        }

        [HttpPost("UpdateUserDetails")]
        public async Task<ActionResult<string>> UpdateUserDetails([FromBody] UpdateUserDetailsRequest request)
        {
            UserDetail u = _repositories.GetUserDetail(request.UserId);
            //kiem tra xem user ton tai hay ko
            if (u == null)
            {
                return BadRequest("User not found!");
            }

            u.DescribeYourself = request.Describe;
            u.Height = request.Height;
            u.Weight = request.Weight;
            u.Age = request.Age;
            u.WantImprove = request.WantImprove;

            _repositories.UpdateUserDetails(u);

            return Ok("Update user successfully!");
        }

        [HttpPost("UpdateNutritionistDetails")]
        public async Task<ActionResult<string>> UpdateNutritionistDetails([FromBody] UpdateNutritionistDetailsRequest request)
        {
            NutritionistDetail u = _repositories.GetNutritionistDetail(request.UserId);

            //kiem tra xem nutritionist ton tai hay ko
            if (u == null)
            {
                return BadRequest("Nutritionist not found!");
            }

            u.DescribeYourself = request.Describe;
            u.Height = request.Height;
            u.Weight = request.Weight;
            u.Age = request.Age;
            _repositories.UpdateNutritionistDetails(u);
            return Ok("Update nutritionist successfully!");
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

        [HttpGet("{userId}/collection-foods")]
        public async Task<IActionResult> GetCollectionFood(int userId, [FromQuery] GetLikeFoodDTO model)
        {
            var paginatedFoods = await repositories.ListCollectionFood(userId, model);

            return Ok(paginatedFoods);
        }

        [HttpPost("{userId}/save-food-collection/{foodId}")]
        public async Task<IActionResult> SaveCollection(int userId, int foodId)
        {
            User u = repositories.GetUserById(userId);
            //kiem tra xem user ton tai hay ko
            if (u == null)
            {
                return BadRequest("User not found!");
            }
            repositories.SaveCollection(userId, foodId);
            return NoContent();
        }
    }
}
