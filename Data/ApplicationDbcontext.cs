using TranMinhDucBTH2.Models;
using Microsoft.EntityFrameworkCore;

namespace TranMinhDucBTH2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext ( DbContextOption<ApplicationDbContext> option) : base(options)
        {

        }

        public DbSet<Employee> Employee {get; set; }
        public DbSet<Person> Person {get; set; }
        public DbSet<Customer> Customer {get; set; }
    }
}