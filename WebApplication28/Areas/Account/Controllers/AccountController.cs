using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication28.BL.VM;
using WebApplication28.DAL.Entities;

namespace WebApplication28.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> usermanager,SignInManager<ApplicationUser> signInManager)
        {
            this.usermanager = usermanager;
            this.signInManager = signInManager;
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> SignIn(SigninVM sign)
        {
            try
            {
                if (ModelState.IsValid )
                {
                    var result = await signInManager.PasswordSignInAsync(sign.userName, sign.Password, false, false);
                    var user=await usermanager.FindByNameAsync(sign.userName);

                    if(user.isActive)
                    {
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Client", new { area = "Client", cartid = user.CartId });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid UserName Or Password Attempt ");
                            return View(sign);
                        }
                    }
                   
                    else
                    {


                        ModelState.AddModelError("", "Your account is not Active Contact With Admins");
                        return View(sign);

                    }
                }
                return View(sign);
                
            }
            catch(Exception e)
            {
                return View(sign);
            }
           
         
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupVM signup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        Email = signup.mail,
                        UserName = signup.userName,
                        address = signup.address,

                        isActive = true,
                        PhoneNumber = signup.Phone,
                    };
                    var result=await usermanager.CreateAsync(user, signup.password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                return View(signup);
            }catch(Exception e) { return View(signup); }
        }



        public IActionResult logout()
        {
            signInManager.SignOutAsync();
          
            return RedirectToAction("SignIn","Account");
        }


    }
}
