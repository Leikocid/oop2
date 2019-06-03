using Lab10.DAL;
using Lab10.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Lab10 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private UnitOfWork unitOfWork;

        public MainWindow() {
            InitializeComponent();

            unitOfWork = new UnitOfWork();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            RefreshProcessorsGrid();
            RefreshComputersGrid();
        }

        private void RefreshProcessorsGrid() {
            IEnumerable<Processor> processors = unitOfWork.ProcessorRepository.Find(includeProperties: "Computer");
            dgProcessors.ItemsSource = processors;
        }

        private void RefreshComputersGrid() {
            if (dgProcessors.SelectedItem != null) {
                if (dgProcessors.SelectedItem is Processor selectedProcessor) {
                    IEnumerable<Computer> computers = unitOfWork.ComputerRepository.Find(filter: c => c.Processor.Id == selectedProcessor.Id, orderBy: q => q.OrderByDescending(c => c.MemorySize));
                    dgComputers.ItemsSource = computers;
                } else {
                    dgComputers.ItemsSource = null;
                }
            } else {
                dgComputers.ItemsSource = null;
            }
        }

        private void Save() {
            try {
                unitOfWork.Save();

                RefreshProcessorsGrid();
                RefreshComputersGrid();
            } catch (DbEntityValidationException ex) {
                MessageBox.Show(ex.EntityValidationErrors.SelectMany(ee => ee.ValidationErrors).Select(ee => ee.ErrorMessage).Aggregate((a, b) => a + ", " + b));
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (unitOfWork != null) {
                unitOfWork.Dispose();
            }
        }

        private void DgProcessors_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            RefreshComputersGrid();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            Save();
        }

        private void DgProcessors_InitializingNewItem(object sender, InitializingNewItemEventArgs e) {
            unitOfWork.ProcessorRepository.Create(e.NewItem as Processor);
        }

        private void DgComputers_InitializingNewItem(object sender, InitializingNewItemEventArgs e) {
            Computer computer = e.NewItem as Computer;
            unitOfWork.ComputerRepository.Create(computer);
            if (dgProcessors.SelectedItem != null) {
                Processor selectedProcessor = dgProcessors.SelectedItem as Processor;
                computer.Processor = selectedProcessor;
            }
        }

        private void BtnDeleteProc_Click(object sender, RoutedEventArgs e) {
            if (dgProcessors.SelectedItem != null) {
                if (dgProcessors.SelectedItem is Processor selectedProcessor) {

                    unitOfWork.ProcessorRepository.Delete(selectedProcessor);

                    Save();
                }
            }
        }

        private void BtnDeleteComp_Click(object sender, RoutedEventArgs e) {
            if (dgComputers.SelectedItem != null) {
                if (dgComputers.SelectedItem is Computer selectedComputer) {

                    unitOfWork.ComputerRepository.Delete(selectedComputer);

                    Save();
                }
            }
        }
    }
}
