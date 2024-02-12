using CoreFirstProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreFirstProject.Database
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> Options) : base(Options)
        {

        }
        public DbSet<EmployeeModel> Employees{ get; set; }
        public DbSet<UserModel> Users { get; set; }


    }
}
