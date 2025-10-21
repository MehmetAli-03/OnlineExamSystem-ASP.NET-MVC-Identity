namespace dotnet_ex.Models
{
    public class TestResult
    {
        public int Id { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;

        public int TestId { get; set; }
        public Test Test { get; set; } = null!;

        public int TotalScore { get; set; }
        public int TotalQuestions { get; set; }
    }
}
