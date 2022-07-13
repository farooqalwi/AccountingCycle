using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingCycle.DataBaseContext
{
    public class FinancialElementType
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
