using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserFoodActionController : ControllerBase
    {
        private readonly IFoodSelectionRepositories repositories = new FoodSelectionRepositories();

        [HttpGet("GetUserFoodAction")]
        public async Task<ActionResult> GetUserFoodAction(int UserId, int FoodId)
        {
            FoodSelection fs = repositories.Get(UserId, FoodId);

            if(fs is null)
            {
                return Ok();
            }

            FoodSelectionDTO result = new FoodSelectionDTO
            {
                UserId = fs.UserId,
                FoodListId = fs.FoodListId,
                Rate = fs.Rate,
                RecurringId = fs.RecurringId,
                IsCollection = fs.IsCollection,
                IsBlock = fs.IsBlock,
                IsLike = fs.IsLike,
            };

            return Ok(result);
        }

        [HttpPost("UserBlockFood")]
        public async Task<ActionResult> UserBlockFood([FromBody]UserAction userAction)
        {
            repositories.UserBlockFood(userAction.UserId, userAction.FoodId);

            return Ok("Operation successful!");
        }

        [HttpPost("UserLikeOrUnlikeFood")]
        public async Task<ActionResult> UserLikeOrUnlikeFood([FromBody] UserAction userAction)
        {
            repositories.UserLikeOrUnlikeFood(userAction.UserId, userAction.FoodId);

            return Ok("Operation successful!");
        }

        [HttpPost("UserSaveOrUnsaveFood")]
        public async Task<ActionResult> UserSaveOrUnsaveFood([FromBody] UserAction userAction)
        {
            repositories.UserSaveOrUnsaveFood(userAction.UserId, userAction.FoodId);

            return Ok("Operation successful!");
        }

        [HttpPost("UserSetRecurFood")]
        public async Task<ActionResult> UserSetRecurFood([FromBody] UserActionRecur userAction)
        {
            repositories.UserRecurFood(userAction.UserId, userAction.FoodId, userAction.RecurId);

            return Ok("Operation successful!");
        }

        [HttpPost("UserRateFood")]
        public async Task<ActionResult> UserRateFood([FromBody] UserActionRate userAction)
        {
            repositories.UserRateFood(userAction.UserId, userAction.FoodId, userAction.Rate);

            return Ok("Operation successful!");
        }
    }
}
