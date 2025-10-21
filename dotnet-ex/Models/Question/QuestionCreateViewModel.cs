using dotnet_ex.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotnet_ex.Models.ViewModels
{
    public class QuestionCreateViewModel
    {
        public string Text { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;
        public char CorrectOption { get; set; }

        public int SelectedTestId { get; set; }  // Hangi teste ekleneceği
        public List<SelectListItem> Tests { get; set; } = new(); // Dropdown için
    }
}
