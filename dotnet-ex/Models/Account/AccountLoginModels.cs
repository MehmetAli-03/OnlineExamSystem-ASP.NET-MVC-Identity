using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_ex.Models
{
    public class AccountLoginModel
    {
        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [DisplayName("E-Posta:")]
        public string Email { get; set; } = null!;

        [DisplayName("Şifre:")]
        public string Password { get; set; } = null!;

        public bool remember { get; set; } = true;
    }
    
}
