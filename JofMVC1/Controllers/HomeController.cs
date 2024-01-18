using JofMVC1.DAL;
using JofMVC1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JofMVC1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Fruit> fruit = _context.Fruits.ToList();  
            return View(fruit);
        }

       
    }
}
