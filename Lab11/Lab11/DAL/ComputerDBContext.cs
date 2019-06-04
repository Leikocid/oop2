using Lab11.Models;
using System.Data.Entity;

namespace Lab11.DAL {
    public class ComputerDBContext : DbContext {
        public ComputerDBContext() : base("name = ComputerDbContext") { }
        public virtual DbSet<Computer> Computers { get; set; }
        public virtual DbSet<Processor> Processors { get; set; }
    }
}
