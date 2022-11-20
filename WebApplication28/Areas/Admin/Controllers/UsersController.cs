using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using WebApplication28.BL.VM;
using WebApplication28.DAL.Entities;

namespace WebApplication28.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {

        private readonly UserManager<ApplicationUser> usermanager;
        private readonly RoleManager<IdentityRole> rolemanager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;
        public UsersController(IMapper mapper, UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> Rolemanager, SignInManager<ApplicationUser> signInManager)
        {
            this.usermanager = usermanager;
            rolemanager = Rolemanager;
            this.signInManager = signInManager;
            this.mapper = mapper;

        }
        public IActionResult Index()
        {
            var data = usermanager.Users;
            ViewBag.roles = rolemanager.Roles;
            return View(data);
        }





        public async Task<IActionResult> StateChange(string id)
        {
            ApplicationUser appuser = await usermanager.FindByIdAsync(id);
            appuser.isActive = !appuser.isActive;
            await usermanager.UpdateAsync(appuser);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> details(string id)
        {
            var data = await usermanager.FindByIdAsync(id);
            ViewBag.roles = rolemanager.Roles;
            return View(data);
        }


        public IActionResult create()
        {

            ViewBag.roles = new SelectList(rolemanager.Roles, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(ApplictionUserVM Appuser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newuser = new ApplicationUser
                    {
                        Email = Appuser.Email,
                        UserName = Appuser.userName,
                        PhoneNumber = Appuser.PhoneNumber,
                        address = Appuser.Address,


                    };
                    var data = await usermanager.CreateAsync(newuser, Appuser.password);

                    if (data.Succeeded)
                    {
                        var rolename = await rolemanager.FindByIdAsync(Appuser.RoleID);

                        await usermanager.AddToRoleAsync(newuser, rolename.Name);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in data.Errors)
                        {
                            ModelState.AddModelError("", item.Description);

                        }

                        ViewBag.roles = new SelectList(rolemanager.Roles, "Id", "Name", ViewBag.role);
                        return View(Appuser);
                    }

                }
                else
                {

                    ViewBag.roles = new SelectList(rolemanager.Roles, "Id", "Name", ViewBag.role);
                    return View(Appuser);
                }
            }
            catch (Exception e)
            {

                if (Appuser.password == null)
                {
                    ModelState.AddModelError("", "please Enter password");
                }
                ViewBag.roles = new SelectList(rolemanager.Roles, "Id", "Name", ViewBag.role);
                return View(Appuser);
            }



        }



        public async Task<IActionResult> Delete(string id)
        {
            var data = await usermanager.FindByIdAsync(id);

            var result = mapper.Map<ApplictionUserVM>(data);
            ViewBag.role = "";
            foreach (var item in rolemanager.Roles)
            {
                if (await usermanager.IsInRoleAsync(data, item.Name))
                {
                    ViewBag.role = item.Id;
                }

            }
            if (ViewBag.role == "")
            {
                ViewBag.status = "Client";

            }
            else
            {
                ViewBag.roles = new SelectList(rolemanager.Roles, "Id", "Name", ViewBag.role);
            }

            return View(result);

        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var data = await usermanager.FindByIdAsync(id);

            await usermanager.DeleteAsync(data);


            return RedirectToAction("Index");

        }

    }
}
