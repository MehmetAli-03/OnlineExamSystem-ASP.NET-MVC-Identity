namespace dotnet_ex.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public char SelectedOption { get; set; } // 'A', 'B', 'C', 'D'
    }
}
