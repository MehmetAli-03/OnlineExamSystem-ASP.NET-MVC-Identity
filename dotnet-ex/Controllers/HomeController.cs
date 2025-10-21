using dotnet_ex.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_ex.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}