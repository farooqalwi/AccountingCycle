using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingCycle.DataBaseContext
{
    public class JournalEntries
    {
        [Key]
        public int Id { get; set; }
        public int JournalId { get; set; }
        public int ElementTypeId { get; set; }
        public int TransactionTypeId { get; set; }
        public int Amount { get; set; }
    }
}
