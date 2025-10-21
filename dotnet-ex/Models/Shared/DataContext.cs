using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_ex.Models
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Test> Tests { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Testler ---
            modelBuilder.Entity<Test>().HasData(
                new Test { Id = 1, Title = "C# Temelleri", Description = "C# başlangıç testi", DurationMinutes = 20 },
                new Test { Id = 2, Title = "HTML Temelleri", Description = "HTML başlangıç testi", DurationMinutes = 15 },
                new Test { Id = 3, Title = "Matematik", Description = "Temel işlemler", DurationMinutes = 25 }
            );

            // --- Sorular ---
            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 1, TestId = 1, Text = "C# kim tarafından geliştirildi?", OptionA = "Microsoft", OptionB = "Apple", OptionC = "Google", OptionD = "IBM", CorrectOption = 'A' },
                new Question { Id = 2, TestId = 1, Text = "C# dosya uzantısı nedir?", OptionA = ".js", OptionB = ".java", OptionC = ".cs", OptionD = ".cpp", CorrectOption = 'C' },
                new Question { Id = 3, TestId = 1, Text = "Console.WriteLine() ne işe yarar?", OptionA = "Ekrana yazı yazar", OptionB = "Dosya okur", OptionC = "Veri siler", OptionD = "Programı kapatır", CorrectOption = 'A' },
                new Question { Id = 4, TestId = 2, Text = "<p> etiketi ne işe yarar?", OptionA = "Paragraf", OptionB = "Link", OptionC = "Resim", OptionD = "Liste", CorrectOption = 'A' },
                new Question { Id = 5, TestId = 2, Text = "<a> etiketi ne işe yarar?", OptionA = "Bağlantı", OptionB = "Alt başlık", OptionC = "Tablo", OptionD = "Resim", CorrectOption = 'A' },
                new Question { Id = 6, TestId = 3, Text = "2 + 2 = ?", OptionA = "3", OptionB = "4", OptionC = "5", OptionD = "2", CorrectOption = 'B' },
                new Question { Id = 7, TestId = 3, Text = "5 * 5 = ?", OptionA = "15", OptionB = "10", OptionC = "25", OptionD = "20", CorrectOption = 'C' }
            );

        }
    }
}
