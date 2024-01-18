using JofMVC1.Areas.Admin.ViewModels;
using JofMVC1.DAL;
using JofMVC1.Helpers;
using JofMVC1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace JofMVC1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FruitController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FruitController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Fruit> fruits = _context.Fruits.ToList();
            return View(fruits);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVM createVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(!createVM.ImageFile.CheckContent("image/"))
            {
                ModelState.AddModelError("ImageFile", "Enter the correct format.");
                return View();
            }

            if (!createVM.ImageFile.CheckLength(2097152))
            {
                ModelState.AddModelError("ImageFile", "Maximum 2Mb.");
                return View();
            }

            Fruit fruit = new()
            {
                Name = createVM.Name,
                Description = createVM.Description,
                ImgUrl = createVM.ImageFile.Upload(_env.WebRootPath, @"\Upload\Fruits\")
            };

            await _context.Fruits.AddAsync(fruit);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Fruit fruit = _context.Fruits.Find(id);

            UpdateVM update = new UpdateVM() 
            { 
                Name=fruit.Name,
                Description=fruit.Description,
                ImageFile = fruit.ImageFile
            };
          
            return View(update);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateVM updateVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            updateVM.ImgUrl = updateVM.ImageFile.Upload(_env.WebRootPath, @"/Upload/Fruits/");

            Fruit oldFruit = _context.Fruits.Find(id);
            oldFruit.Name = updateVM.Name;
            oldFruit.Description = updateVM.Description;
            oldFruit.ImgUrl = updateVM.ImgUrl;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Fruit fruit = _context.Fruits.Find(id);

            _context.Fruits.Remove(fruit);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
