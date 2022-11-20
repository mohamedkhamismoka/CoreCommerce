using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication28.BL.VM;
using WebApplication28.DAL.Entities;


namespace WebApplication28.Areas.Admin.Controllers
{
    [Area("Admin")]
 
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public AdminController(UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signInManager,IMapper mapper)
        {
            this.usermanager = usermanager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> login(SigninVM login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                

                    var result = await signInManager.PasswordSignInAsync(login.userName, login.Password,false,false);
                    if (result.Succeeded)
                    {
                        var user = await usermanager.FindByNameAsync(login.userName);
                        if (user != null)


                        {
                            var superAdmins = await usermanager.GetUsersInRoleAsync("Super Admin");
                            var Admins = await usermanager.GetUsersInRoleAsync("Admin");

                            if (superAdmins.Contains(user) || Admins.Contains(user))
                            {
                                
                                    return RedirectToAction("Index");

                                
                            }
                            else
                            {
                                ModelState.AddModelError("", "Invalid UserName Or Password Attempt");

                                return View(login);

                            }


                       

                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid UserName Or Password Attempt");

                            return View(login);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid UserName Or Password Attempt");

                        return View(login);
                    }
                   
                }
                return View(login);
            }
           catch(Exception e)
            {
                return View(login);
            }
        }
        
      public IActionResult Index()
        {

           
           
            return View(usermanager.Users);
        }

        public async Task<IActionResult> Profile(string name)
        {
            var user = await usermanager.FindByNameAsync(name);
            var result = mapper.Map<ApplictionUserVM>(user);
            ViewBag.role = (await usermanager.GetRolesAsync(user));

            return View(result);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login", "Admin");

        }



    }
}
