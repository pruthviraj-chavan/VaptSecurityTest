using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VaptSecurityTest.Models;

namespace VaptSecurityTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
