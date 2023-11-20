using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using MVCStudentsApp.PeopleModels;
using MVCStudentsApp.Models;

namespace StudentsAppMVC.Data
{
    public class MVCDbContext : DbContext
    {
        public MVCDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; internal set; }
    }
}
