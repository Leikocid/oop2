using Lab09.Model.Computer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace Lab09.Service {
    class ProcessorDAO {

        public static Processor Create(string model, string developer, int coresCount) {
            return Create(new Processor() {
                Model = model,
                Developer = developer,
                CoresCount = coresCount
            });
        }

        public static Processor Create(Processor processor) {
            try {
                using (var context = new ComputerDBContext()) {
                    context.Processors.Add(processor);
                    context.SaveChanges();
                    return processor;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static Processor Find(int id) {
            try {
                using (var context = new ComputerDBContext()) {
                    return context.Processors.FirstOrDefault(p => p.Id == id);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static Processor Update(int id, string model, string developer, int coresCount) {
            try {
                using (var context = new ComputerDBContext()) {
                    Processor processor = Find(id);
                    if (processor != null) {
                        processor.Model = model;
                        processor.Developer = developer;
                        processor.CoresCount = coresCount;
                        context.SaveChanges();
                    }
                    return processor;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static Processor Update(Processor processor) {
            try {
                using (var context = new ComputerDBContext()) {
                    context.Processors.Attach(processor);
                    context.Entry(processor).State = EntityState.Modified;
                    context.SaveChanges();
                    return processor;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static List<Processor> Find(string developer, int coresCount) {
            try {
                using (var context = new ComputerDBContext()) {
                    return (from p in context.Processors
                            where p.Developer.ToLower().StartsWith(developer.ToLower()) && p.CoresCount == coresCount
                            orderby p.Model ascending
                            select p).ToList();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static Processor Delete(Processor processor) {
            try {
                using (var context = new ComputerDBContext()) {
                    context.Processors.Attach(processor);
                    context.Processors.Remove(processor);
                    context.SaveChanges();
                    return processor;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static Processor ModifyFirstProcessor() {
            try {
                using (var context = new ComputerDBContext()) {

                    using (var transaction = context.Database.BeginTransaction()) {
                        try {
                            Processor processor = context.Database.SqlQuery<Processor>("SELECT * FROM Processors WHERE Id = (SELECT MIN(id) FROM Processors)").FirstOrDefault();
                            if (processor != null) {
                                Processor newProcessor = new Processor();
                                Processor.CopyProperties(processor, newProcessor);
                                newProcessor.Id = 0;
                                context.Processors.Add(newProcessor);
                                context.SaveChanges();

                                SqlParameter param = new SqlParameter("Id", processor.Id);
                                context.Database.ExecuteSqlCommand(@"UPDATE Processors SET CoresCount = @Cores, Model = CONCAT(Model, @Suffix) WHERE Id = @Id", new object[] {
                                    new SqlParameter("@Id", processor.Id),
                                    new SqlParameter("@Cores", processor.CoresCount * 2),
                                    new SqlParameter("@Suffix", " -> (" + newProcessor.Id + ")")
                                });
                            }
                            transaction.Commit();
                            return Find(processor.Id);
                        } catch (Exception ex) {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
    }
}
