using Bank.DAL.Data;
using De_Bank.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.FrontEnd.Controllers
{
    public class IdentityHolderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankLogic _banklogic = new BankLogic();
        public IdentityHolderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account
        public async Task<IActionResult> Index()
        {
            var result = await _context.IdentityHolders.ToListAsync();
            return View(result);
            //return View(await _context.Accounts.Where(u => u.IdentityHolder.UserName == User.Identity.Name).ToListAsync());  // CODE ALLE ACCOUNTS VAN GEBRUIKER (INGELOGD)
            //return View(await _context.Accounts.ToListAsync()); // ALLE ACCOUNTS laten zien
        }
    }
}
