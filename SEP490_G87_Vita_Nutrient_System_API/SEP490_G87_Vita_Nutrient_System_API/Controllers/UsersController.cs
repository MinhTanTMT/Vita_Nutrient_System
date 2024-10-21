using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Domain.Enums;
using SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IUserRepositories repositories = new UsersRepositories();


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
        public async Task<ActionResult<List<dynamic>>> GetAllUsers()
        {

            var users = repositories.GetAllUsers();

            var result = users.Select(
                u => new 
                {
                    Id = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Urlimage = u.Urlimage,
                    Dob = u.Dob,
                    Gender = u.Gender,
                    Address = u.Address,
                    Phone = u.Phone,
                    Role = new {RoleId = u.Role, RoleName = u.RoleNavigation.RoleName},
                    IsActive = u.IsActive,
                    Account = u.Account
                }).ToList();

            return Ok(result);
        }

        [HttpGet("GetUserByRole/{roleId}")]
        public async Task<ActionResult<dynamic>> GetUsersByRole(int roleId)
        {
            //kiem tra xem roleId duoc truyen vao co hop le hay khong
            if(!Enum.IsDefined(typeof(UserRole), roleId))
            {
                return BadRequest("Invalid role id!");
            }

            var users = repositories.GetUsersByRole(roleId);

            var result = users.Select(
                u => new
                {
                    Id = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Urlimage = u.Urlimage,
                    Dob = u.Dob,
                    Gender = u.Gender,
                    Address = u.Address,
                    Phone = u.Phone,
                    Role = new { RoleId = u.Role, RoleName = u.RoleNavigation.RoleName },
                    IsActive = u.IsActive,
                    Account = u.Account
                }).ToList();

            return Ok(result);
        }

        [HttpGet("GetUserDetailInfo/{id}")]
        public async Task<ActionResult<dynamic>> GetUserDetailInfo(int id)
        {

            var user = repositories.GetUserDetailsInfo(id);

            if(user == null)
            {
                return BadRequest("User not found!");
            }

            bool? nullableBool = null;
            short? nullableInt = null;

            var result = new
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Urlimage = user.Urlimage,
                Dob = user.Dob,
                Gender = user.Gender,
                Address = user.Address,
                Phone = user.Phone,
                Role = new { RoleId = user.Role, RoleName = user.RoleNavigation.RoleName },
                IsActive = user.IsActive,
                Account = user.Account,
                DetailsInformation = user.UserDetail is null ?
                new{
                    Description = String.Empty,
                    Height = nullableInt,
                    Weight = nullableInt,
                    Age = nullableInt,
                    WantImprove = String.Empty,
                    UnderlyingDisease = String.Empty,
                    InforConfirmGood = String.Empty,
                    InforConfirmBad = String.Empty,
                    IsPremium = nullableBool
                } 
                : 
                new{
                    Description = user.UserDetail.DescribeYourself,
                    Height = user.UserDetail.Height,
                    Weight = user.UserDetail.Weight,
                    Age = user.UserDetail.Age,
                    WantImprove = user.UserDetail.WantImprove,
                    UnderlyingDisease = user.UserDetail.UnderlyingDisease,
                    InforConfirmGood = user.UserDetail.InforConfirmGood,
                    InforConfirmBad = user.UserDetail.InforConfirmBad,
                    IsPremium = user.UserDetail.IsPremium
                }
            };

            return Ok(result);
        }

        [HttpGet("GetNutritionistDetailInfo/{id}")]
        public async Task<ActionResult<dynamic>> GetNutritionistDetailInfo(int id)
        {

            var nutritionist = repositories.GetNutritionistDetailsInfo(id);

            if (nutritionist == null)
            {
                return BadRequest("Nutritionist not found!");
            }

            bool? nullableBool = null;
            short? nullableShort = null;
            int? nullableInt = null;
            double? nullableDouble = null;

            var result = new
            {
                Id = nutritionist.UserId,
                FirstName = nutritionist.FirstName,
                LastName = nutritionist.LastName,
                Urlimage = nutritionist.Urlimage,
                Dob = nutritionist.Dob,
                Gender = nutritionist.Gender,
                Address = nutritionist.Address,
                Phone = nutritionist.Phone,
                Role = new { RoleId = nutritionist.Role, RoleName = nutritionist.RoleNavigation.RoleName },
                IsActive = nutritionist.IsActive,
                Account = nutritionist.Account,
                DetailsInformation = nutritionist.UserDetail is null ?
                new
                {
                    Description = String.Empty,
                    Height = nullableShort,
                    Weight = nullableShort,
                    Age = nullableShort,
                    Rate = nullableDouble,
                    NumberRate = nullableInt
                }
                :
                new
                {
                    Description = nutritionist.NutritionistDetail.DescribeYourself,
                    Height = nutritionist.NutritionistDetail.Height,
                    Weight = nutritionist.NutritionistDetail.Weight,
                    Age = nutritionist.NutritionistDetail.Age,
                    Rate = nutritionist.NutritionistDetail.Rate,
                    NumberRate = nutritionist.NutritionistDetail.NumberRate
                }
            };

            return Ok(result);
        }

        [HttpPost("UpdateUserStatus")]
        public async Task<ActionResult<dynamic>> UpdateUserStatus([FromBody] UpdateUserStatusRequest request)
        {
            User u = repositories.GetUserById(request.UserId);
            //kiem tra xem user ton tai hay ko
            if(u == null)
            {
                return BadRequest("User not found!");
            }

            u.IsActive = request.Status;
            repositories.UpdateUser(u);

            return Ok("Update user status successfully!");
        }
    }
}
