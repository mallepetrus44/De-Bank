using Bank.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.FrontEnd.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityHolder> userManager;

        public AdminController(UserManager<IdentityHolder> usrMgr)
        {
            userManager = usrMgr;
        }

        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(IdentityHolder user)
        {
            if (ModelState.IsValid)
            {
                IdentityHolder idUser = new IdentityHolder
                {
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,                  
                    Email = user.Email,
                    Password = user.Password

                };

                IdentityResult result = await userManager.CreateAsync(idUser, user.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
    }
}
