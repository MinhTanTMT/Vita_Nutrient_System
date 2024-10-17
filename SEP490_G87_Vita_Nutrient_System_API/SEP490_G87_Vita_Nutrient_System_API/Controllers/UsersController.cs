using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    FirstName = u.FirstName,
                    LastName = u.LastName,
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
    }
}
