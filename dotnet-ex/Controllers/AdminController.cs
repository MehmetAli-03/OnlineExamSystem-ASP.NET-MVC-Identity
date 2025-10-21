using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_ex.Models;

namespace dotnet_ex.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }

        // Admin panel ana sayfası
        public IActionResult Index()
        {
            return View();
        }

        // Test sonuçlarını listele
        public IActionResult TestResults()
        {
            var results = _context.TestResults
                                  .Include(r => r.Test)
                                  .Include(r => r.AppUser)
                                  .OrderByDescending(r => r.Id)
                                  .ToList();

            return View(results);
        }
    }
}
