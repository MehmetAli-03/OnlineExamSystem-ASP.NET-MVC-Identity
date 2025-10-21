using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_ex.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_ex.Controllers;

public class AccountController : Controller
{
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;
    private IEmailService _emailService;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
    }

    // Kullanıcı oluşturma
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(AccountCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser { UserName = model.Email, Email = model.Email, FullName = model.FullName,StudentNo=model.StudentNo,ClassInfo=model.Sinifİnfo };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                TempData["Mesaj"] = "Kayıt işlemi başarıyla tamamlandı. Giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    // Giriş
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(AccountLoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.remember, true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                    var timeLeft = lockoutDate.Value.UtcDateTime - DateTime.UtcNow;

                    ModelState.AddModelError("", $"Hesabınız {timeLeft.Minutes} dakika {timeLeft.Seconds} saniye boyunca erişime kapalıdır.");
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı şifre");
                }
            }
            else
            {
                ModelState.AddModelError("", "Hatalı email");
            }
        }
        return View(model);
    }

    // Çıkış
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        TempData["Mesaj"] = "Başarıyla çıkış yaptınız.";
        return RedirectToAction("Login", "Account");
    }

    // Kullanıcı bilgilerini düzenleme
    [Authorize]
    public async Task<ActionResult> EditUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId!);

        if (user == null)
        {
            TempData["Mesaj"] = "Kullanıcı bulunamadı. Lütfen tekrar giriş yapın.";
            return RedirectToAction("Login", "Account");
        }

        return View(new EditUserModel
        {
            FullName = user.FullName,
            Email = user.Email!
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> EditUser(EditUserModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId!);

        if (ModelState.IsValid && user != null)
        {
            user.Email = model.Email;
            user.FullName = model.FullName;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["Mesaj"] = "Bilgileriniz başarıyla güncellendi.";
                return RedirectToAction("EditUser");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(model);
    }

    // Şifre değiştir
    [Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(AccountChangePasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);

            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

                if (result.Succeeded)
                {
                    TempData["Mesaj"] = "Şifreniz başarıyla değiştirildi.";
                    return RedirectToAction("ChangePassword");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return View(model);
    }

    // Şifre unutuldu
    public ActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> ForgotPassword(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            TempData["Mesaj"] = "E-posta adresini giriniz.";
            return View();
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            TempData["Mesaj"] = "Bu e-posta adresi kayıtlı değil.";
            return View();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var url = Url.Action("ResetPassword", "Account", new { userId = user.Id, token });
        var link = $"<a href='http://localhost:5285{url}'>Şifre Yenile</a>";
        await _emailService.SendEmailAsync(user.Email!, "Parola Sıfırlama", link);

        TempData["Mesaj"] = "E-posta adresine gönderilen link ile şifrenizi sıfırlayabilirsiniz.";
        return RedirectToAction("Login");
    }

    // Şifre sıfırlama sayfası
    public async Task<ActionResult> ResetPassword(string userId, string token)
    {
        if (userId == null || token == null)
        {
            TempData["Mesaj"] = "Geçersiz şifre sıfırlama linki.";
            return RedirectToAction("Login");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            TempData["Mesaj"] = "Kullanıcı bulunamadı.";
            return RedirectToAction("Login");
        }

        var model = new AccountResetPasswordModel
        {
            token = token,
            Email = user.Email!
        };
        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> ResetPassword(AccountResetPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Mesaj"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Login");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.token, model.Password);
            if (result.Succeeded)
            {
                TempData["Mesaj"] = "Şifreniz başarıyla değiştirildi.";
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }
}
