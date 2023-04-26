using Artsofte.Models;
using Microsoft.EntityFrameworkCore;

namespace Artsofte.Data
{
    public class ArtsoftDbContext : DbContext
    {
        public ArtsoftDbContext( DbContextOptions<ArtsoftDbContext> options ) : base( options )
        {

        }

        public DbSet<Employee> Employees {
            get; set;
        }

        public DbSet<Department> Departments {
            get; set;
        }

        public DbSet<ProgrammingLanguage> ProgramingLanguages {
            get; set;
        }
    }
}
