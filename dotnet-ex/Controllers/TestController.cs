using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_ex.Models;
using dotnet_ex.Models.ViewModels;

namespace dotnet_ex.Controllers
{
    [Authorize] // Kullanıcı giriş yapmadan test gönderemez
    public class TestController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TestController(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Testleri listele
        public IActionResult Index()
        {
            var tests = _context.Tests
                                .Include(t => t.Questions)
                                .ToList();
            return View(tests);
        }

        public IActionResult List()
        {
            var tests = _context.Tests
                                .Include(t => t.Questions)
                                .ToList();
            return View(tests);
        }

        // Test detayları
        public IActionResult Details(int id)
        {
            var test = _context.Tests
                               .Include(t => t.Questions)
                               .FirstOrDefault(t => t.Id == id);

            if (test == null)
                return NotFound();

            return View(test);
        }

        // Test ekleme formu
        public IActionResult Create()
        {
            return View();
        }

        // Test ekleme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Test model)
        {
            if (ModelState.IsValid)
            {
                _context.Tests.Add(model);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View(model);
        }

        // Test silme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var test = _context.Tests.Find(id);
            if (test == null)
                return NotFound();

            _context.Tests.Remove(test);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        // Test cevaplarını gönder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitAnswers(TestSubmitViewModel model)
        {
            if (model == null || model.TestId == 0)
                return BadRequest();

            // Test ve sorularını getir
            var test = _context.Tests
                               .Include(t => t.Questions)
                               .FirstOrDefault(t => t.Id == model.TestId);

            if (test == null)
                return NotFound("Test bulunamadı.");

            if (model.Answers == null || !model.Answers.Any())
                return BadRequest("Cevap yok.");

            // Giriş yapmış kullanıcıyı al
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge(); // login sayfasına yönlendir

            int correct = 0;
            int total = test.Questions!.Count;

            // Kullanıcının cevaplarını kaydet
            foreach (var answer in model.Answers)
            {
                var question = test.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                if (question == null)
                    continue;

                if (question.CorrectOption == answer.SelectedOption)
                    correct++;

                var userAnswer = new UserAnswer
                {
                    AppUserId = user.Id,
                    QuestionId = question.Id,
                    SelectedOption = answer.SelectedOption
                };
                _context.UserAnswers.Add(userAnswer);
            }

            // Test sonucu oluştur
            var result = new TestResult
            {
                AppUserId = user.Id,
                TestId = test.Id,
                TotalQuestions = total,
                TotalScore = correct
            };

            _context.TestResults.Add(result);
            _context.SaveChanges();

            return RedirectToAction("Result", new { id = result.Id });
        }

        // Test sonucu göster
        public IActionResult Result(int id)
        {
            var result = _context.TestResults
                                 .Include(r => r.Test)
                                 .FirstOrDefault(r => r.Id == id);

            if (result == null)
                return NotFound();

            return View(result);
        }
             public async Task<IActionResult> MyResults()
    {
        var user = await _userManager.GetUserAsync(User);

        var results = _context.TestResults
                              .Include(r => r.Test)
                              .Where(r => r.AppUserId == user!.Id)
                              .OrderByDescending(r => r.Id)
                              .ToList();

        return View(results);
    }
        
    }
}
