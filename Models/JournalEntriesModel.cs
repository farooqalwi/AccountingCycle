using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingCycle.Models
{
    public class JournalEntriesModel
    {
        public string DebitList { get; set; }
        public string CreditList { get; set; }

        //Insert into GeneralJournalEntries table
        public DateTime TransactionDate { get; set; }
    }
}
