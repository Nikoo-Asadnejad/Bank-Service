using BankMicroservice.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankMicroservice.Model
{
  public class Context : DbContext
  {
    public Context(DbContextOptions options) : base(options)
    {

    }
    public DbSet<BankTransactionModel> BankTransactions { get; set; }
  }
}
