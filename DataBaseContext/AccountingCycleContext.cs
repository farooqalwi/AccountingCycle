using Microsoft.EntityFrameworkCore;

namespace AccountingCycle.DataBaseContext
{
    public class AccountingCycleContext : DbContext
    {
        private readonly DbContextOptions _options;

        public AccountingCycleContext(DbContextOptions<AccountingCycleContext> options) : base(options)
        {
            _options = options;
        }

        public DbSet<FinancialElementType> FinancialElementType { get; set; }
        public DbSet<GeneralJournalEntries> GeneralJournalEntries { get; set; }
        public DbSet<JournalEntries> JournalEntries { get; set; }
        public DbSet<Transactiontype> Transactiontype { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
