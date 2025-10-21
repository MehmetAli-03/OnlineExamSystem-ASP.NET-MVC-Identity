using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_ex.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        [DisplayName("Ad Soyad:")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [DisplayName("E-Posta:")]
        public string Email { get; set; } = null!;


    }
    
}
