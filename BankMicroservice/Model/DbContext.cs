using BankMicroservice.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankMicroservice.Model
{
  public class Context : DbContext
  {
    public DbSet<BankTransaction> BankTransactions { get; set; }
  }
}
