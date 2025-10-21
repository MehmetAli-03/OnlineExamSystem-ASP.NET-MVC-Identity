namespace dotnet_ex.Models;

 public class Test
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;              // Test başlığı
        public string? Description { get; set; }                // Kısa açıklama
        public int DurationMinutes { get; set; }         
        

        // Navigation
        public ICollection<Question>? Questions { get; set; }

    }
