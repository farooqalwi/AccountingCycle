using AccountingCycle.DataBaseContext;
using AccountingCycle.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingCycle.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly AccountingCycleContext _context;

        public DashboardController(AccountingCycleContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GeneralJournal()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GeneralJournal(JournalEntriesModel model)
        {
            int generalJournalEntryId = 1;
            int journalEntrId = 0;

            if (_context.GeneralJournalEntries.Count() > 0)
            {
                generalJournalEntryId += _context.GeneralJournalEntries.ToList().LastOrDefault().Id;
            }

            if (_context.JournalEntries.Count() > 0)
            {
                journalEntrId = _context.JournalEntries.ToList().LastOrDefault().Id;
            }

            var GeneralJournalEntry = new GeneralJournalEntries
            {
                Id = generalJournalEntryId,
                UserId = Int32.Parse(User.FindFirst("UserID").Value),
                TransactionDate = model.TransactionDate
            };

            _context.GeneralJournalEntries.Add(GeneralJournalEntry);

            var DebitList = new List<TransactionEntry>();

            if (model.DebitList != null && model.DebitList.Any())
            {
                DebitList = JsonConvert.DeserializeObject<List<TransactionEntry>>(model.DebitList);
            }

            var CreditList = new List<TransactionEntry>();

            if (model.CreditList != null && model.DebitList.Any())
            {
                CreditList = JsonConvert.DeserializeObject<List<TransactionEntry>>(model.CreditList);
            }

            if (DebitList.Count > 0 && CreditList.Count > 0)
            {
                foreach (var item in DebitList)
                {
                    journalEntrId += 1;

                    var JournalEntry = new JournalEntries
                    {
                        Id = journalEntrId,
                        JournalId = generalJournalEntryId,
                        ElementTypeId = item.ElementTypeId,
                        TransactionTypeId = item.TransactionTypeId,
                        Amount = item.Amount,
                        AccountTitle = item.AccountTitle

                    };

                    _context.JournalEntries.Add(JournalEntry);

                }

                foreach (var item in CreditList)
                {
                    journalEntrId += 1;

                    var JournalEntry = new JournalEntries
                    {
                        Id = journalEntrId,
                        JournalId = generalJournalEntryId,
                        ElementTypeId = item.ElementTypeId,
                        TransactionTypeId = item.TransactionTypeId,
                        Amount = item.Amount,
                        AccountTitle = item.AccountTitle
                    };

                    _context.JournalEntries.Add(JournalEntry);
                }
            }




            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EntryDelete(int? Id)
        {
            if (Id != null)
            {
                var generalJournalEntry = await _context.GeneralJournalEntries.FindAsync(Id);
                

                if (generalJournalEntry != null)
                {
                    var journalEntries = await _context.JournalEntries.ToListAsync();
                    if (journalEntries.Count > 0)
                    {
                        foreach (var journalEntry in journalEntries)
                        {
                            if (journalEntry.JournalId == generalJournalEntry.Id)
                            {
                                _context.JournalEntries.Remove(journalEntry);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }

                _context.GeneralJournalEntries.Remove(generalJournalEntry);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("GeneralJournal", "Dashboard");
        }

        public IActionResult TAccount()
        {
            return View();
        }

        public IActionResult TrialBalance()
        {
            return View();
        }

        public IActionResult IncomeStatement()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IncomeStatement(DateTime date)
        {
            var GeneralJournalEntries = _context.GeneralJournalEntries.ToList().Where(x => x.UserId == Int32.Parse(User.FindFirst("UserId").Value) && x.TransactionDate.Year == date.Year);

            return View(GeneralJournalEntries);
        }

        public IActionResult OwnerEquity()
        {
            return View();
        }

        public IActionResult BalanceSheet()
        {
            return View();
        }
    }
}
