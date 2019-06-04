using Lab11.Commands;
using Lab11.DAL;
using Lab11.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;

namespace Lab11.ViewModels {

    class MainWindowViewModel : INotifyPropertyChanged, IDisposable {

        // свойства и привязка
        private int filterId;
        private string filterDev;
        private int filterCores;
        private ObservableCollection<Processor> processors;
        private ObservableCollection<Processor> editProcessor;
        private string status;

        // конструктор
        public MainWindowViewModel() {
            editProcessor = new ObservableCollection<Processor>();
            processors = new ObservableCollection<Processor>();
        }

        public int FilterId {
            get { return filterId; }
            set {
                filterId = value;
                OnPropertyChanged("FilterId"); // уведомление View
            }
        }

        public string FilterDev {
            get { return filterDev; }
            set {
                filterDev = value;
                OnPropertyChanged("FilterDev"); // уведомление View
            }
        }

        public int FilterCores {
            get { return filterCores; }
            set {
                filterCores = value;
                OnPropertyChanged("FilterCores"); // уведомление View
            }
        }

        public ObservableCollection<Processor> Processors {
            get { return processors; }
            set {
                processors = value;
                OnPropertyChanged("Processors"); // уведомление View
            }
        }

        public ObservableCollection<Processor> EditProcessor {
            get { return editProcessor; }
            set {
                editProcessor = value;
                OnPropertyChanged("EditProcessor"); // уведомление View
            }
        }

        public string Status {
            get { return status; }
            set {
                status = value;
                OnPropertyChanged("Status"); // уведомление View
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // команды
        UnitOfWork unitOfWork = new UnitOfWork();

        private Command findByIdCommand;
        public Command FindByIdCommand {
            get {
                return findByIdCommand ??
                  (findByIdCommand = new Command(obj => {
                      Processors = new ObservableCollection<Processor> { unitOfWork.ProcessorRepository.FindById(FilterId) };
                  }, canExecute: obj => FilterId > 0));
            }
        }

        private Command findByDevCommand;
        public Command FindByDevСommand {
            get {
                return findByDevCommand ??
                  (findByDevCommand = new Command(obj => {
                      Processors = new ObservableCollection<Processor>(
                          unitOfWork.ProcessorRepository.Find(filter: p => p.Developer.ToLower().StartsWith(filterDev.ToLower()) && p.CoresCount == FilterCores)
                      );
                  }, canExecute: obj => (FilterDev != null && FilterDev.Length > 0 && FilterCores > 0)));
            }
        }

        private Command newCommand;
        public Command NewСommand {
            get {
                return newCommand ??
                  (newCommand = new Command(obj => {
                      EditProcessor = new ObservableCollection<Processor> { new Processor() };
                  }));
            }
        }

        private Processor originalProcessor;

        private Command editCommand;
        public Command EditСommand {
            get {
                return editCommand ??
                  (editCommand = new Command(selectedItem => {
                      originalProcessor = selectedItem as Processor;
                      EditProcessor = new ObservableCollection<Processor> { Processor.Create(originalProcessor) };
                  }, canExecute: selectedItem => selectedItem != null));
            }
        }

        private Command saveCommand;
        public Command SaveСommand {
            get {
                return saveCommand ??
                  (saveCommand = new Command(obj => {
                      try {
                          Processor processor = EditProcessor[0];
                          if (processor.Id == 0) {
                              unitOfWork.ProcessorRepository.Create(processor);
                              unitOfWork.Save();

                              EditProcessor = new ObservableCollection<Processor> { processor };
                              Status = $"Новый процессор сохранен в БД (id = {processor.Id})";
                          } else {
                              if (originalProcessor != null) {
                                  Processor.CopyProperties(processor, originalProcessor);
                                  unitOfWork.ProcessorRepository.Update(originalProcessor);
                                  unitOfWork.Save();

                                  Processors = Processors;
                              } else {
                                  unitOfWork.ProcessorRepository.Update(processor);
                                  unitOfWork.Save();
                              }

                              Status = $"Информация о процессоре успешно сохранена в БД (id = {processor.Id})";
                          }
                      } catch (DbEntityValidationException ex) {
                          Status = "Error: " + ex.EntityValidationErrors.SelectMany(ee => ee.ValidationErrors).Select(ee => ee.ErrorMessage).Aggregate((a, b) => a + ", " + b);
                      } catch (Exception ex) {
                          Status = "Error: " + ex.Message;
                      }
                  }, canExecute: obj => EditProcessor != null && EditProcessor.Count > 0));
            }
        }

        private Command deleteCommand;
        public Command DeleteСommand {
            get {
                return deleteCommand ??
                  (deleteCommand = new Command(selectedItem => {
                      try {
                          Processor processor = selectedItem as Processor;
                          unitOfWork.ProcessorRepository.Delete(processor);
                          unitOfWork.Save();
                          Processors.Remove(processor);

                          Processors = Processors;
                          Status = $"Информация о процессоре успешно удалена из БД (id = {processor.Id})";

                          if (EditProcessor != null && EditProcessor[0].Id == processor.Id) {
                              EditProcessor = new ObservableCollection<Processor>();
                          }
                      } catch (Exception e) {
                          Status = "Error: " + e.Message;
                      }
                  }, canExecute: selectedItem => selectedItem != null));
            }
        }

        public void Dispose() {
            unitOfWork?.Dispose();
        }
    }
}
