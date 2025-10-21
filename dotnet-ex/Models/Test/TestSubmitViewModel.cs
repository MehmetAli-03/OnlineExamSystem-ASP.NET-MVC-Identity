namespace dotnet_ex.Models.ViewModels
{
    public class AnswerViewModel
    {
        public int QuestionId { get; set; }
        public char SelectedOption { get; set; }
    }

    public class TestSubmitViewModel
    {
        public int TestId { get; set; }
        public List<AnswerViewModel> Answers { get; set; } = new();
    }
}
