using System.Threading.Tasks;
using dotnet_ex.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace dotnet_ex.Controllers;
[Authorize(Roles ="Admin")]
public class UserController : Controller
{
    private UserManager<AppUser> _userManager;
    private RoleManager<AppRole> _rolemanager;
    public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> rolemanager)
    {
        _userManager = userManager;
        _rolemanager = rolemanager;
    }

    public ActionResult Index()
    {
        return View(_userManager.Users);
    }

    public ActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<ActionResult> Create(UserCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    public async Task<ActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Roles = await _rolemanager.Roles.Select(i => i.Name).ToListAsync();

        return View
        (
            new UserEditModel
            {
                FullName = user.FullName,
                Email = user.Email!,
                SelectedRole = await _userManager.GetRolesAsync(user)
            }
        );
    }

    [HttpPost]

    public async Task<ActionResult> Edit(string id, UserEditModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = model.Email;
                user.FullName = model.FullName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, model.Password);
                }

                if (result.Succeeded)
                {
                    await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                    if (model.SelectedRole != null)
                    {
                        await _userManager.AddToRolesAsync(user, model.SelectedRole);
                    }
                    return RedirectToAction("Index");
                }
            }
        }
        return View(model);
    }
}