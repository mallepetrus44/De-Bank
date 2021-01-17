using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank.DAL.Data;
using Bank.DAL.Models;
using System.Security.Claims;
using Bank.FrontEnd.ViewModels;
using De_Bank.Logic;

namespace Bank.FrontEnd.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BankLogic _banklogic = new BankLogic();


        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {

            var result = await _context.Transactions.Where(u => u.IdentityHolder.Email == User.Identity.Name).ToListAsync();

            return View(result);
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transaction/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransactionAmount,TransactionDate,IsPeriodic,PeriodicTransactionFrequentyDays,Frequenty,NextPayment")] Transaction transaction)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            IdentityHolder identityHolder = _context.IdentityHolders.Where(i => i.Id == currentUserID).FirstOrDefault();

            var accounts = _context.Accounts.Where(u => u.IdentityHolder.Id == currentUserID);
            var transactions = _banklogic.GetAccountTransactions();
            var UserTo = await Task.Run(() => GetUserIDByBankAccount(transaction.AccountTo));

            if (UserTo !="NotFound")
            {
                transaction.AccountTo = UserTo;
            }

            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);

        }
        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TransactionAmount,TransactionDate,IsPeriodic,PeriodicTransactionFrequentyDays,Frequenty,NextPayment")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }

        public async Task<string> GetUserIDByBankAccount(string accountNumber)
        {
            var AllAccounts = await Task.Run(() => GetAllAccounts());
            var TryFindUser = await Task.Run(()=> GetAccountID(AllAccounts, accountNumber));
            var AccountID = TryFindUser;
            if (AccountID != "NotFound")
            {
                return AccountID;
            }
            return "NotFound";
        }

        public string GetAccountID(List<Account> accounts, string accountNumber)
        {
            var AllUsers = _context.IdentityHolders.ToList();
            string id = "";
            int count = 0;

            foreach (Account account in accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    id = account.IdentityHolder.Id;
                }
                return "NotFound";
            }
            return "NotFound";
        }
        public List<Account> GetAllAccounts()
        {
            var AllAccounts = new List<Account>();

            foreach (var item in _context.Accounts)
            {
                AllAccounts.Add(item);
            }

            return AllAccounts.ToList();
        }
    }
}
