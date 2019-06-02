using Lab09.Model.Computer;
using Lab09.Service;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Lab09 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            using (var context = new ComputerDBContext()) {
                Processor processor = new Processor {
                    Model = "Core i7",
                    Developer = "Intel",
                    CoresCount = 8
                };
                context.Processors.Add(processor);
                context.SaveChanges();
            }
        }

        private void BtnFindById_Click(object sender, RoutedEventArgs e) {
            if (Int32.TryParse(txtId.Text, out int id)) {
                grdProcessors.ItemsSource = null;
                grdProcessors.Items.Clear();
                Processor processor = ProcessorDAO.Find(id);
                if (processor != null) {
                    List<Processor> processors = new List<Processor>() {
                        processor
                    };
                    grdProcessors.ItemsSource = processors;
                }
            }
        }

        private async void BtnFindByDev_Click(object sender, RoutedEventArgs e) {
            if (txtDev.Text.Length > 0 && Int32.TryParse(txtCores.Text, out int cores)) {

                btnFindById.IsEnabled = false;
                btnFindByDev.IsEnabled = false;
                bFT.Visibility = Visibility.Collapsed;
                bFI.Visibility = Visibility.Visible;

                string developerMask = txtDev.Text;
                List<Processor> processors = await Task.Run(() => {
                    Thread.Sleep(5000);
                    return ProcessorDAO.Find(developerMask, cores);
                });

                grdProcessors.ItemsSource = null;
                grdProcessors.ItemsSource = processors;

                bFI.Visibility = Visibility.Collapsed;
                bFT.Visibility = Visibility.Visible;
                btnFindById.IsEnabled = true;
                btnFindByDev.IsEnabled = true;
            }
        }

        private async Task<List<Processor>> LoadProcssorsAsync(string developer, int cores) {
            var task = Task.Run(() => {
                Thread.Sleep(5000); // эмуляция какой-то долгой предолгой загрузки
                return ProcessorDAO.Find(developer, cores);
            });
            return await task;
        }

        private Processor originalProcessor;
        private void BtnNew_Click(object sender, RoutedEventArgs e) {
            originalProcessor = null;

            List<Processor> items = new List<Processor> {
                new Processor()
            };
            grdProcessor.ItemsSource = items;
        }

        private void BtnModify_Click(object sender, RoutedEventArgs e) {
            if (grdProcessors.SelectedItem != null) {
                originalProcessor = grdProcessors.SelectedItem as Processor;
                Processor processor = new Processor();
                Processor.CopyProperties(originalProcessor, processor);
                List<Processor> items = new List<Processor> {
                    processor
                };
                grdProcessor.ItemsSource = items;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            if (grdProcessor.Items.Count > 0) {
                Processor processor = grdProcessor.Items[0] as Processor;
                if (processor.Id  == 0) {                   
                    ProcessorDAO.Create(grdProcessor.Items[0] as Processor);

                    grdProcessor.Items.Refresh();
                } else {
                    ProcessorDAO.Update(processor);

                    if (originalProcessor != null) {
                        Processor.CopyProperties(processor, originalProcessor);
                        grdProcessors.Items.Refresh();
                    }
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            if (grdProcessors.SelectedItem != null) {
                Processor p = grdProcessors.SelectedItem as Processor;
                ProcessorDAO.Delete(p);

                List<Processor> items = grdProcessors.ItemsSource as List<Processor>;
                items.Remove(p);
                grdProcessors.Items.Refresh();
            }
        }

        private void BtnSQL_Click(object sender, RoutedEventArgs e) {
            originalProcessor = null;

            Processor processor = ProcessorDAO.ModifyFirstProcessor();
            if (processor != null) {
                List<Processor> items = new List<Processor> {
                    processor
                };
                grdProcessor.ItemsSource = items;
            }
        }
    }
}
