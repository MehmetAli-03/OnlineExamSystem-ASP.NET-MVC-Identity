using Microsoft.AspNetCore.Identity;

namespace dotnet_ex.Models;

 public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; } = null!;
        public string StudentNo { get; set; } = null!;
        public string ClassInfo { get; set; } = null!;
        
        public ICollection<UserAnswer>? Answers { get; set; }
    }
