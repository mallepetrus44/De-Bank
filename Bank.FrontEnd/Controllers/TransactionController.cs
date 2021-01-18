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
            if (User.Identity.IsAuthenticated)
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                var Transactions = await Task.Run(() => _context.Transactions.Where(u => u.AccountFrom == currentUserID).ToList());

                if(Transactions != null)
                {
                    return View(Transactions);
                }
                return Redirect("Home/Index");
            }
            return Redirect("Home/Index");

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
            Transaction transaction = new Transaction
            {
                TransactionDate = DateTime.Now
            };
            return View(transaction);
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountTo,TransactionAmount,TransactionDate,IsPeriodic,PeriodicTransactionFrequentyDays,Frequenty,NextPayment")] Transaction transaction)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            IdentityHolder identityHolder = _context.IdentityHolders.Where(i => i.Id == currentUserID).FirstOrDefault();

            //var accounts = _context.Accounts.Where(u => u.IdentityHolder.Id == currentUserID);
            var UserTo = await Task.Run(() => GetUserIDByBankAccount(transaction.AccountTo.ToString()));

            //var IdentityHolder = identityHolder.Id.ToString();
            if (transaction.TransactionDate <= DateTime.Now)
            {
                TempData["shortMessage"] = "De transactiedatum mag niet in het verleden liggen";
                return Redirect("Error");
            }
            if (UserTo != "NotFound")
            {
                var CorrectedTransaction = new Transaction
                {
                    AccountFrom = currentUserID,
                    AccountTo = UserTo,
                    PeriodicTransactionFrequentyDays = transaction.PeriodicTransactionFrequentyDays,
                    Frequenty = transaction.Frequenty - 1, //NextPayment bepaald de eerst volgende betaling
                    IsPeriodic = transaction.IsPeriodic,
                    NextPayment = transaction.TransactionDate.AddDays(-transaction.PeriodicTransactionFrequentyDays), //Bereken de eerst volgende transactiedatum
                    TransactionAmount = transaction.TransactionAmount,
                    TransactionDate = transaction.TransactionDate
                };

                if (ModelState.IsValid)
                {
                    _context.Transactions.Add(CorrectedTransaction);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["shortMessage"] = "Model is not Valid => TransactionController(Create, POST, Error)";
            return Redirect("Error");

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
        public ActionResult Error()
        {
            //now I can populate my ViewBag (if I want to) with the TempData["shortMessage"] content
            ViewBag.Message = TempData["shortMessage"].ToString();
            return View();
        }

        public async Task<string> GetUserIDByBankAccount(string accountNumber)
        {
            var GotAccount = await Task.Run(() => GetAccount(accountNumber));
            var TryFindUser = await Task.Run(()=> GetAccountUserID(GotAccount));
            var AccountID = TryFindUser.ToString();
            if (AccountID != "NotFound")
            {
                return AccountID.ToString();
            }
            return accountNumber.ToString();
        }

        public string GetAccountUserID(Account account)
        {
            if (account != null)
            {
                var UserIdAccount = account.IdentityHolder.Id;
                return UserIdAccount;
            }
            return "NotFound";
        }
        public Account GetAccount(string accountNumber)
        {
            var item = _context.Accounts.Where(a => a.AccountNumber == accountNumber).FirstOrDefault();         

            return item;
        }
    }
}
