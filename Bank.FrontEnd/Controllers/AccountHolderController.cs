using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank.FrontEnd.Data;
using De_Bank.Models;

namespace Bank.FrontEnd.Controllers
{
    public class AccountHolderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountHolderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AccountHolder
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccountHolders.ToListAsync());
        }

        // GET: AccountHolder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountHolder = await _context.AccountHolders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountHolder == null)
            {
                return NotFound();
            }

            return View(accountHolder);
        }

        // GET: AccountHolder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountHolder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName")] AccountHolder accountHolder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountHolder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountHolder);
        }

        // GET: AccountHolder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountHolder = await _context.AccountHolders.FindAsync(id);
            if (accountHolder == null)
            {
                return NotFound();
            }
            return View(accountHolder);
        }

        // POST: AccountHolder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName")] AccountHolder accountHolder)
        {
            if (id != accountHolder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountHolder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountHolderExists(accountHolder.Id))
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
            return View(accountHolder);
        }

        // GET: AccountHolder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountHolder = await _context.AccountHolders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountHolder == null)
            {
                return NotFound();
            }

            return View(accountHolder);
        }

        // POST: AccountHolder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountHolder = await _context.AccountHolders.FindAsync(id);
            _context.AccountHolders.Remove(accountHolder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountHolderExists(int id)
        {
            return _context.AccountHolders.Any(e => e.Id == id);
        }
    }
}
