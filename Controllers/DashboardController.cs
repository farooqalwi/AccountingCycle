using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingCycle.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GeneralJournal()
        {
            return View();
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
