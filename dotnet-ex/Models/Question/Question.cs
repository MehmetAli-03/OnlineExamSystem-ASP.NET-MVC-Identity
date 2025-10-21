namespace dotnet_ex.Models;

 public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;

        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;

        public char CorrectOption { get; set; } // 'A', 'B', 'C', 'D'

        // Foreign key
        public int TestId { get; set; }
        public Test Test { get; set; } = null!;
    }