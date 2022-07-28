using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingCycle.Models
{
    public class TransactionEntry
    {
        public int id { get; set; }
        public int ElementTypeId { get; set; }
        public int TransactionTypeId { get; set; }
        public int Amount { get; set; }
        public string AccountTitle { get; set; }
    }
}
