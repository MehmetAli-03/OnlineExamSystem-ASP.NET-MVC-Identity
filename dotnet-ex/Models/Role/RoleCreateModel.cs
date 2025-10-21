using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_ex.Models
{
    public class RoleCreateModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        [DisplayName("Ad Soyad:")]
        public string RoleName { get; set; } = null!;
    }
    
}
