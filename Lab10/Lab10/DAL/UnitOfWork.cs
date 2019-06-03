using Lab10.Model;
using System;

namespace Lab10.DAL {
    class UnitOfWork : IDisposable {
        private ComputerDBContext context = new ComputerDBContext();
        private IRepository<Processor> processorRepository;
        private IRepository<Computer> computerRepository;

        public IRepository<Processor> ProcessorRepository {
            get {
                if (this.processorRepository == null) {
                    this.processorRepository = new GenericRepository<Processor>(context);
                }
                return processorRepository;
            }
        }

        public IRepository<Computer> ComputerRepository {
            get {
                if (this.computerRepository == null) {
                    this.computerRepository = new GenericRepository<Computer>(context);
                }
                return computerRepository;
            }
        }

        public void Save() {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
