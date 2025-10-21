using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_ex.Models
{
    public class AccountResetPasswordModel
    {
        public string token { get; set; } = null!;
        public string Email { get; set; } = null!;

        
        [Required]
        [DisplayName("Yeni Şifre:")]
        public string Password { get; set; } = null!;

        [Required]
        [DisplayName(" yeni Şifre Tekrar:")]
        [Compare("Password",ErrorMessage ="Parola eşleşmiyor")]
        public string ConfirmPassword { get; set; } = null!;

    }
    
}
