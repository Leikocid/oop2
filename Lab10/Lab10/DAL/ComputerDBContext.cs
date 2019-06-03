using Lab10.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.DAL {
    public class ComputerDBContext: DbContext {
        public ComputerDBContext() : base("name = ComputerDbContext") { }
        public virtual DbSet<Computer> Computers { get; set; }
        public virtual DbSet<Processor> Processors { get; set; }
    }
}
