using Bank.DAL.Data;
using Bank.DAL.Models;
using Bank.FrontEnd.ViewModels;
using De_Bank.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        
        //public ViewResult AltIndex()
        //{
        //    IdentityAccountDetailsViewModel identityDetailsViewModel = new IdentityAccountDetailsViewModel()
        //    {
        //        Accounts = _context.Accounts.ToList(),
        //        IdentityHolders = _context.IdentityHolders.ToList(),
        //        Transactions = _context.Transactions.ToList()
        //    };
        //    IdentityAccountDetailsViewModel model = identityDetailsViewModel;
        //    return View(model);
        //}
        // GET: Account
        public async Task<IActionResult> Clients()
        {

            var result = await _context.IdentityHolders.ToListAsync();
            //var result = await _context.IdentityHolders.ToListAsync();


            return View(result);
            //return View(await _context.Accounts.Where(u => u.IdentityHolder.UserName == User.Identity.Name).ToListAsync());  // CODE ALLE ACCOUNTS VAN GEBRUIKER (INGELOGD)
            //return View(await _context.Accounts.ToListAsync()); // ALLE ACCOUNTS laten zien
        }
        public async Task<IActionResult> Accounts()
        {

            var result = await _context.Accounts.ToListAsync();
            //var result = await _context.IdentityHolders.ToListAsync();


            return View(result);
            //return View(await _context.Accounts.Where(u => u.IdentityHolder.UserName == User.Identity.Name).ToListAsync());  // CODE ALLE ACCOUNTS VAN GEBRUIKER (INGELOGD)
            //return View(await _context.Accounts.ToListAsync()); // ALLE ACCOUNTS laten zien
        }

        public async Task<IActionResult> Transactions()
        {

            var result = await _context.Transactions.ToListAsync();
            //var result = await _context.IdentityHolders.ToListAsync();


            return View(result);
            //return View(await _context.Accounts.Where(u => u.IdentityHolder.UserName == User.Identity.Name).ToListAsync());  // CODE ALLE ACCOUNTS VAN GEBRUIKER (INGELOGD)
            //return View(await _context.Accounts.ToListAsync()); // ALLE ACCOUNTS laten zien
        }



        public async Task<IActionResult> BankAccountList()
        {
            //ClaimsPrincipal currentUser = this.User;
            //var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            //IdentityHolder identityHolder = _context.IdentityHolders.Where(i => i.Id == currentUserID).FirstOrDefault();         
            ListAndSearchVM listAndSearchVM = new ListAndSearchVM();
            listAndSearchVM.Accounts = GetAccounts();
            listAndSearchVM.Transactions = GetTransactions();
            listAndSearchVM.IdentityHolders = GetIdentityHolders();

            //var result = await _context.IdentityHolders.ToListAsync();


            return View(listAndSearchVM);
            //return View(await _context.Accounts.Where(u => u.IdentityHolder.UserName == User.Identity.Name).ToListAsync());  // CODE ALLE ACCOUNTS VAN GEBRUIKER (INGELOGD)
            //return View(await _context.Accounts.ToListAsync()); // ALLE ACCOUNTS laten zien
        }

        private List<IdentityHolder> GetIdentityHolders()
        {
            var holders = _context.IdentityHolders.ToList();
            return holders;
        }

        private List<Transaction> GetTransactions()
        {
            var transactions = _context.Transactions.ToList();
            return transactions;
        }

        private List<Account> GetAccounts()
        {
            var accounts = _context.Accounts.ToList();
            return accounts;
        }
    }
}
