using TranMinhDucBTH2.Models;
using Microsoft.EntityFrameworkCore;

namespace TranMinhDucBTH2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext ( DbContextOption<ApplicationDbContext> option) : base(options)
        {
            public DbSet<TranMinhDucBTH2.Models.Student> Student {get; set; }
            public DbSet<TranMinhDucBTH2.Models.Employee> Employee {get; set; }
            public DbSet<TranMinhDucBTH2.Models.Person> Person {get; set; }
            public DbSet<TranMinhDucBTH2.Models.Customer> Customer {get; set; }
            public DbSet<TranMinhDucBTH2.Models.Faculty> Employee {get; set; } = default!;

        }
    }
}