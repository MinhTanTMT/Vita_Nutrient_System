using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("API is working!");
        }

        [HttpGet("get-request-info")]
        public IActionResult GetRequestInfo()
        {
            var request = HttpContext.Request;

            var info = new
            {
                Host = request.Host.Value,
                IP = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Protocol = request.Protocol,
                IsHttps = request.IsHttps,
                Headers = request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())
            };

            return Ok(info);
        }
    }
}