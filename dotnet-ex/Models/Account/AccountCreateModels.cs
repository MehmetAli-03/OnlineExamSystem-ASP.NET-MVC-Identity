using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_ex.Models
{
    public class AccountCreateModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        [DisplayName("Ad Soyad:")]
        public string FullName { get; set; } = null!;

       [Required(ErrorMessage = "Öğrenci No zorunludur.")]
        [DisplayName("Öğrenci No:")]
        public string StudentNo { get; set; } = null!;
        [Required(ErrorMessage = "Sınıf  zorunludur.")]
        [DisplayName("Sınıf:")]
        public string Sinifİnfo { get; set; } = null!;
        
        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [DisplayName("E-Posta:")]
        public string Email { get; set; } = null!;

        [DisplayName("Şifre:")]
        public string Password { get; set; } = null!;

    }
    
}
