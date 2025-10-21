using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_ex.Models
{
    public class AccountChangePasswordModel
    {
        [Required]
        [DisplayName(" Mevcut Şifre:")]
        public string OldPassword { get; set; } = null!;

        [Required]
        [DisplayName(" Yeni Şifre:")]
        public string Password { get; set; } = null!;

    }
    
}
