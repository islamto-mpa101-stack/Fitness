using Fitness.Context;
using Fitness.Helpers;
using Fitness.Models;
using Fitness.ViewModel.TeacherViwModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fitness.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _folderPath;

        public TrainerController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var trainer = await _context.Trainers.FirstOrDefaultAsync(x=>x.Id == id);

            TrainerUpdateVm vm = new()
            {
                Id = id,
                Name = trainer.Name,
                Profession = trainer.Profession,
            };

            if(trainer == null)
                return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TrainerUpdateVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (!vm.Image.CheckType())
            {
                ModelState.AddModelError("", "Ancaq image tipinde yukleye bilersiniz.");
            }

            var trainer = await _context.Trainers.FirstOrDefaultAsync(x=>x.Id == vm.Id);

            if(trainer == null) 
                return NotFound();

            trainer.Profession = vm.Profession;
            trainer.Name = vm.Name;

            if(vm.Image is { })
            {
                string path = Path.Combine(_folderPath, trainer.ImagePath);

                if(System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                string uniqueFileName = await vm.Image.SaveAsync(_folderPath);

                trainer.ImagePath = uniqueFileName;
            }

            _context.Trainers.Update(trainer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Delete(int id)
        {
            var trainer = await _context.Trainers
                                        .FirstOrDefaultAsync(x => x.Id == id);

            if (trainer == null) 
                return NotFound();

            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();

            string filePath = Path.Combine(_folderPath,trainer.ImagePath);

            if(System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers.Select(x => new TrainerGetVm
            {
                Id = x.Id,
                Name = x.Name,
                Profession = x.Profession,
                ImagePath = x.ImagePath,
            }).ToListAsync();

            return View(trainers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrainerCreateVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (!vm.Image.CheckType())
            {
                ModelState.AddModelError("", "Ancaq image tipinde yukleye bilersiniz.");
            }

            string uniqueFileName = await vm.Image.SaveAsync(_folderPath);

            Trainer trainer = new()
            {
                Name = vm.Name,
                Profession = vm.Profession,
                ImagePath = uniqueFileName,
            };

            await _context.Trainers.AddAsync(trainer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }


    }
}
