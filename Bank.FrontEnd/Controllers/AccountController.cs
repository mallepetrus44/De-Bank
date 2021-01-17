using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank.DAL.Data;
using Bank.DAL.Models;
using De_Bank.Logic;
using Bank.FrontEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Syncfusion.EJ2.Navigations;

namespace Bank.FrontEnd.Controllers
{
    public class AccountController : Controller
    {


        private readonly ApplicationDbContext _context;

        BankLogic _banklogic = new BankLogic();

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<IActionResult> Dashboard(string selectedStatus = "")
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var vm = new ListAndSearchVM
            {
                Accounts = await Task.Run(() => _context.Accounts.Where(u => u.IdentityHolder.Id == currentUserID).ToList()),
                Transactions = await Task.Run(()=> _context.Transactions.Where(u => u.IdentityHolder.Id == currentUserID).ToList()),
            };

            vm.Selection = new List<SelectListItem>
            {
                        new SelectListItem { Value="Accounts", Text="Account overzicht"},
                        new SelectListItem { Value="Transactions", Text="Transactie overzicht"},
                        new SelectListItem { Value="Details", Text="Persoonlijke gegevens"},
            };

            return View(vm);
   
        }


        public ActionResult CreateNewActions(string actionid)
        {
            ViewBag.DivActionID = actionid;
            return PartialView("~/Views/Interp/_AddActions.cshtml");
        }


        // GET: Account
        public async Task<IActionResult> Index()
        {
            var x = await _context.Accounts.Where(u => u.IdentityHolder.UserName == User.Identity.Name).ToListAsync();
                ViewData["Id"] = new SelectList(x,"Id", "AccountNumber");
                return View();

            /*return View(await _context.Accounts.Where(u => u.IdentityHolder.UserName == User.Identity.Name).ToListAsync());  */// CODE ALLE ACCOUNTS VAN GEBRUIKER (INGELOGD)
            //return View(await _context.Accounts.ToListAsync()); // ALLE ACCOUNTS laten zien
        }

        //public IActionResult Accounts()
        //{
        //    var model = new Account();
        //    model.AccountNumber.ToList();
        //    return View(model);
        //}
        // GET: Account/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Account/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountMinimum,AccountType,AccountLimiet")] Account account)
        {

            var id = _context.Accounts.Count();

            Account NewAccount = new Account
            {
                AccountBalance = 0,
                AccountType = account.AccountType,
                AccountNumber = account.AccountNumber = await Task.Run(() => _banklogic.GetNextAccountNumber(id)),
                AccountLimiet = account.AccountLimiet,
                AccountMinimum = 0 - account.AccountMinimum,
                AccountLock = false,
                IdentityHolder = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name)
            };

            if (ModelState.IsValid)
            {
                _context.Add(NewAccount);
                _context.Accounts.Add(NewAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }


        //int i = _context.Accounts.Count();
        //    if (ModelState.IsValid)
        //    {
        //        account.AccountNumber = await Task.Run(()=> bl.GetNextAccountNumber(account, i));
        //account.IdentityHolder = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
        //        _context.Add(account);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));

        // GET: Account/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountLock,AccountBalance,AccountMinimum,AccountType, AccountNumber")]Account account)
        {
            {
                if (id != account.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
            }
            return View(account);
        }

        // GET: Account/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            account.AccountLock = true;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }


        //public string onSelectedIndexChanged(int value)
        //{
        //    var textValue = value.options[value.selectedIndex].text;
        //    document.getElementById('Period').value = textValue;

        //    // if you want to submit the form, uncomment this line below
        //    // document.getElementById('yourformId').submit();
        //}
    }
}


//        // GET: Account
//        public async Task<IActionResult> Index()
//        {
//            var result = await _context.Accounts.Where(u => u.IdentityHolder.Email == User.Identity.Name).ToListAsync(); 

//            return View(result);
//        }

//        // GET: Account/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var account = await _context.Accounts
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (account == null)
//            {
//                return NotFound();
//            }

//            return View(account);
//        }

//        // GET: Account/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Account/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create (Account account)
//        {
//            var id = _context.Accounts.Count();

//            Account NewAccount = new Account
//            {        
//                AccountBalance = 0,
//                AccountType = account.AccountType,
//                AccountNumber = account.AccountNumber = await Task.Run(() => _banklogic.GetNextAccountNumber(id)),
//                AccountLimiet = account.AccountLimiet,
//                AccountMinimum = 0 - account.AccountMinimum,
//                AccountLock = false,
//                IdentityHolder = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name)
//            };


//            if (ModelState.IsValid)
//            {
//                _context.Add(NewAccount);
//                _context.Accounts.Add(NewAccount);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(account);
//        }

//// GET: Account/Edit/5
//public async Task<IActionResult> Edit(int? id)
//        {

//            if (id == null)
//            {
//                return NotFound();
//            }

//            var account = await _context.Accounts.FindAsync(id);          
//            if (account == null)
//            {
//                return NotFound();
//            }
//            return View(account);
//        }

//        // POST: Account/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNumber,AccountLock,AccountBalance,AccountMinimum,AccountLimiet,AccountType")]IdentityHolder identityHolder, Account account)
//        {

//            var result = await _context.Accounts.Where(i => i.IdentityHolder.Email == User.Identity.Name).ToListAsync();
//            foreach (var item in result)
//            {
//                if (item.Id != account.Id)
//                {
//                    return NotFound();
//                }

//                if (ModelState.IsValid)
//                {
//                    try
//                    {
//                        _context.Entry(item).State = EntityState.Modified;
//                        //_context.Update(item);
//                        await _context.SaveChangesAsync();
//                    }
//                    catch (DbUpdateConcurrencyException)
//                    {
//                        if (!AccountExists(account.Id))
//                        {
//                            return NotFound();
//                        }
//                        else
//                        {
//                            throw;
//                        }
//                    }
//                    return RedirectToAction(nameof(Index));
//                }
//            }
//            return RedirectToAction(nameof(Index));
//        }

//        // GET: Account/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var account = await _context.Accounts
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (account == null)
//            {
//                return NotFound();
//            }

//            return View(account);
//        }

//        // POST: Account/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var account = await _context.Accounts.FindAsync(id);
//            _context.Accounts.Remove(account);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool AccountExists(int id)
//        {
//            return _context.Accounts.Any(e => e.Id == id);
//        }
//    }
//}
