using System.Threading.Tasks;
using dotnet_ex.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_ex.Controllers;

 [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new AppRole { Name = model.RoleName });

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null) return RedirectToAction("Index");

            var entity = await _roleManager.FindByIdAsync(id);
            if (entity == null) return RedirectToAction("Index");

            return View(entity);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var entity = await _roleManager.FindByIdAsync(id);

            if (entity != null)
            {
                await _roleManager.DeleteAsync(entity);
            }

            return RedirectToAction("Index");
        }
    }

