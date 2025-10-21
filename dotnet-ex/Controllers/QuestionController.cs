using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using dotnet_ex.Models;
using dotnet_ex.Models.ViewModels;

namespace dotnet_ex.Controllers
{
    public class QuestionController : Controller
    {
        private readonly DataContext _context;

        public QuestionController(DataContext context)
        {
            _context = context;
        }

        // Soru ekleme formu
        public IActionResult Create()
        {
            var model = new QuestionCreateViewModel
            {
                Tests = _context.Tests
                                .Select(t => new SelectListItem
                                {
                                    Value = t.Id.ToString(),
                                    Text = t.Title
                                })
                                .ToList()
            };
            return View(model);
        }

        // Soru ekleme POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(QuestionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = new Question
                {
                    Text = model.Text,
                    OptionA = model.OptionA,
                    OptionB = model.OptionB,
                    OptionC = model.OptionC,
                    OptionD = model.OptionD,
                    CorrectOption = model.CorrectOption,
                    TestId = model.SelectedTestId
                };

                _context.Questions.Add(question);
                _context.SaveChanges();
                return RedirectToAction("Index", "Test"); // Soru eklendikten sonra test listesine yönlendir
            }

            // Dropdown listesini tekrar doldur
            model.Tests = _context.Tests
                                  .Select(t => new SelectListItem
                                  {
                                      Value = t.Id.ToString(),
                                      Text = t.Title
                                  })
                                  .ToList();

            return View(model);
        }
    }
}
