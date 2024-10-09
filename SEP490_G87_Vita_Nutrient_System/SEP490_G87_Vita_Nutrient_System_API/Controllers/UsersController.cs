using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase, UsersInterface
    {
        private readonly Sep490G87VitaNutrientSystemContext _context;

        public UsersController(Sep490G87VitaNutrientSystemContext context)
        {
            _context = context;
        }


        [HttpGet("Login")]
        public async Task<ActionResult<User>> GetUserLogin(string account, string password)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.Include(u => u.RoleNavigation).FirstOrDefaultAsync(u => u.Account == account && u.Password == password);
            if (user == null)
            {
                return NotFound();
            }
            var UserLogin = new
            {
                FullName = user.FirstName + " " + user.LastName,
                user.Urlimage,
                user.Account,
                user.RoleNavigation.RoleName,
                user.UserId
            };
            return Ok(UserLogin);
        }


        [HttpGet("checkExit")]
        public async Task<ActionResult<User>> GetUserByAccount(string account)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Account.Equals(account));
            if (user == null)
            {
                return NotFound();
            }
            return Ok();
        }



        [HttpPost("Register")]
        public async Task<ActionResult<User>> GetUserRegister(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //var userReturn = await _context.Users.FirstOrDefaultAsync(u => u.Account.Equals(user.Account) && u.Password.Equals(user.Password));
            //if (userReturn == null)
            //{
            //    return NotFound();
            //}

            return Ok(user);
        }


        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

    }
}
