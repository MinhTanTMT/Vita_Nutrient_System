using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class NutritionRouteRepositories : INutritionRouteRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        // Get all nutrition routes
        public async Task<IEnumerable<NutritionRouteDTO>> GetAllNutritionRoutesByCreateByIdAsync(int createById)
        {
            return await _context.NutritionRoutes
                .Where(route => route.CreateById == createById) // Lọc theo createById
                .Include(route => route.CreateBy)
                .Include(route => route.User)
                .Select(route => new NutritionRouteDTO
                {
                    Id = route.Id,
                    UserId = route.UserId,
                    CreateById = route.CreateById,
                    UserName = route.User.FirstName + " " + route.User.LastName,
                    CreateByName = route.CreateBy.FirstName + " " + route.CreateBy.LastName,
                    Name = route.Name,
                    Describe = route.Describe,
                    StartDate = route.StartDate,
                    EndDate = route.EndDate,
                    IsDone = route.IsDone
                }).ToListAsync();
        }



        // Get nutrition route by ID
        public async Task<NutritionRouteDTO> GetNutritionRouteByIdAsync(int id)
        {
            var route = await _context.NutritionRoutes.FindAsync(id);
           

            return new NutritionRouteDTO
            {
                Id = route.Id,
                UserId = route.UserId,
                CreateById = route.CreateById,
                Name = route.Name,
                Describe = route.Describe,
                StartDate = route.StartDate,
                EndDate = route.EndDate,
                IsDone = route.IsDone
            };
        }

        // Create a new nutrition route
        public async Task CreateNutritionRouteAsync(NutritionRouteDTO nutritionRouteDto)
        {
            var route = new NutritionRoute
            {
                UserId = nutritionRouteDto.UserId,
                CreateById = nutritionRouteDto.CreateById,
                Name = nutritionRouteDto.Name,
                Describe = nutritionRouteDto.Describe,
                StartDate = nutritionRouteDto.StartDate,
                EndDate = nutritionRouteDto.EndDate,
                IsDone = nutritionRouteDto.IsDone
            };

            _context.NutritionRoutes.Add(route);
            await _context.SaveChangesAsync();
        }

        // Update an existing nutrition route
        public async Task UpdateNutritionRouteAsync(NutritionRouteDTO nutritionRouteDto)
        {
            var route = await _context.NutritionRoutes.FindAsync(nutritionRouteDto.Id);
            if (route == null) return;

            route.Name = nutritionRouteDto.Name;
            route.Describe = nutritionRouteDto.Describe;
            route.StartDate = nutritionRouteDto.StartDate;
            route.EndDate = nutritionRouteDto.EndDate;
            route.IsDone = nutritionRouteDto.IsDone;

            _context.NutritionRoutes.Update(route);
            await _context.SaveChangesAsync();
        }

        // Delete a nutrition route
        public async Task DeleteNutritionRouteAsync(int id)
        {
            var route = await _context.NutritionRoutes.FindAsync(id);
            if (route == null) return;

            _context.NutritionRoutes.Remove(route);
            await _context.SaveChangesAsync();
        }
    }
}
