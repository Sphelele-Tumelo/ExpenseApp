using Microsoft.EntityFrameworkCore;

namespace Banking_App._2.Models
{
    public class ExpenseDbContxt : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public ExpenseDbContxt(DbContextOptions<ExpenseDbContxt> options)
            :base(options)
        {
                
        } 
    }
}
