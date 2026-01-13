using System.Diagnostics;
using System.Threading.Tasks;
using Fitness.Context;
using Fitness.Models;
using Fitness.ViewModel.TeacherViwModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers.Select(x=> new TrainerGetVm
            {
                Id = x.Id,
                Name = x.Name,
                Profession = x.Profession,
                ImagePath = x.ImagePath
            })
                .ToListAsync();

            return View(trainers);
        }



    }
}
