using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab09.Model.Computer {
    public class ComputerDBContext: DbContext {
        public ComputerDBContext() : base("name = ComputerDbContext") { }
        public virtual DbSet<Computer> Computers { get; set; }
        public virtual DbSet<Processor> Processors { get; set; }
    }
}
