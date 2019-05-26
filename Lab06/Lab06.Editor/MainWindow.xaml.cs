using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Lab06.Editor {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            rtbEditor.AddHandler(RichTextBox.DragOverEvent, new DragEventHandler(RtbEditor_Drag), true);
            rtbEditor.AddHandler(RichTextBox.DropEvent, new DragEventHandler(RtbEditor_Drop), true);
            rtbEditor.Focus();
            Theme.ItemsSource = new List<string> { "Light", "Dark", "CuteDracula" };
            Theme.SelectedItem = "Dark";

            InitializeNew();
        }

        private void InitializeNew() {
            Title = "Текстовый редактор";
            rtbEditor.Document.Blocks.Clear();
        }

        private void SetRussian(object sender, RoutedEventArgs e) {
            try {
                this.Resources = new ResourceDictionary() {
                    Source = new Uri("pack://application:,,,/resources/Language_RU.xaml")
                };
            } catch (Exception ex) {
                MessageBox.Show("RU: " + ex.Message);
            }
        }

        private void SetEnglish(object sender, RoutedEventArgs e) {
            try {
                this.Resources = new ResourceDictionary() {
                    Source = new Uri("pack://application:,,,/resources/Language_EN.xaml")
                };
            } catch (Exception ex) {
                MessageBox.Show("EN: " + ex.Message);
            }
        }


        private void Open_Executed(object sender, ExecutedRoutedEventArgs e) {
            try {
                OpenFileDialog dlg = new OpenFileDialog {
                    FileName = "test.txt"
                };
                dlg.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|XAML Files (*.html)|*.html|All files (*.*)|*.*";
                if (dlg.ShowDialog() == true) {
                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                    using (FileStream fs = new FileStream(dlg.FileName, FileMode.Open)) {
                        switch (System.IO.Path.GetExtension(dlg.FileName).ToLower()) {
                            case ".rtf":
                                range.Load(fs, DataFormats.Rtf);
                                break;
                            case ".html":
                                range.Load(fs, DataFormats.Html);
                                break;
                            default:
                                range.Load(fs, DataFormats.Text);
                                break;
                        }
                        this.Title = "Текстовый редактор (" + dlg.FileName + ")";
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show("Ошибка открытия файла: " + ex.Message);
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e) {
            try {
                SaveFileDialog dlg = new SaveFileDialog {
                    FileName = "test.txt"
                };
                dlg.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|XAML Files (*.html)|*.html|All files (*.*)|*.*";
                if (dlg.ShowDialog() == true) {
                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                    using (FileStream fs = new FileStream(dlg.FileName, FileMode.Create)) {
                        switch (Path.GetExtension(dlg.FileName).ToLower()) {
                            case ".rtf":
                                range.Save(fs, DataFormats.Rtf);
                                break;
                            case ".html":
                                range.Save(fs, DataFormats.Html);
                                break;
                            default:
                                range.Save(fs, DataFormats.Text);
                                break;
                        }
                    }
                    this.Title = "Текстовый редактор (" + dlg.FileName + ")";
                }
            } catch (Exception ex) {
                MessageBox.Show("Ошибка сохранения файла: " + ex.Message);
            }
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e) {
            InitializeNew();
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e) {
            Close();
        }

        private void Color_Click(object sender, RoutedEventArgs e) {
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            var result = cd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK) {
                rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty,
                     new SolidColorBrush(new Color() {
                         A = cd.Color.A,
                         R = cd.Color.R,
                         G = cd.Color.G,
                         B = cd.Color.B
                     }));
            }
        }

        private void Font_Click(object sender, RoutedEventArgs e) {
            System.Windows.Forms.FontDialog fd = new System.Windows.Forms.FontDialog();
            var result = fd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK) {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, new FontFamily(fd.Font.Name));
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fd.Font.Size * 96.0 / 72.0);
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontWeightProperty, fd.Font.Bold ? FontWeights.Bold : FontWeights.Regular);
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontStyleProperty, fd.Font.Italic ? FontStyles.Italic : FontStyles.Normal);

                TextDecorationCollection tdc = new TextDecorationCollection();
                if (fd.Font.Underline) tdc.Add(TextDecorations.Underline);
                if (fd.Font.Strikeout) tdc.Add(TextDecorations.Strikethrough);
                rtbEditor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, tdc);
            }
        }

        private void CmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cmbFontFamily.SelectedItem != null)
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        private void RtbEditor_TextChanged(object sender, TextChangedEventArgs e) {
            this.status1.Content = GetLength1(this.rtbEditor);
            this.status2.Content = GetLength2(this.rtbEditor);

        }

        private string GetLength1(RichTextBox rtb) {
            int count_of_symbols = 0;
            var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            foreach (char c in textRange.Text) {
                if (!c.Equals('\n') && (int)c != 13) {
                    count_of_symbols++;
                }
            }
            return "" + count_of_symbols;
        }

        private string GetLength2(RichTextBox rtb) {
            string textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text;
            return (Regex.Split(textRange, "\\w+").Count()-1).ToString();
        }


        private void SldFontSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (this.sldFontSize.IsFocused && this.sldFontSize.Value > 0) {
                TextRange range;
                if (!this.rtbEditor.Selection.IsEmpty) {
                    range = new TextRange(this.rtbEditor.Selection.Start, this.rtbEditor.Selection.End);
                    range.ApplyPropertyValue(FontSizeProperty, this.sldFontSize.Value);
                }
            }
        }

        private void RtbEditor_Drop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                string[] docPath = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (System.IO.File.Exists(docPath[0])) {
                    try {
                        TextRange range = new TextRange(this.rtbEditor.Document.ContentStart, this.rtbEditor.Document.ContentEnd);
                        FileStream fStream = new FileStream(docPath[0], FileMode.OpenOrCreate);
                        range.Load(fStream, DataFormats.Rtf);
                        fStream.Close();
                        this.Title = "Text Editor (" + docPath[0] + ") ";
                    } catch (Exception) {
                        MessageBox.Show("File could not be opened. Make sure the file is a text file.");
                    }
                }
            }
        }

        private void RtbEditor_Drag(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effects = DragDropEffects.All;
            } else {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = false;
        }

        private void RtbEditor_SelectionChanged(object sender, RoutedEventArgs e) {
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            sldFontSize.Value = Convert.ToInt32(temp);
        }

        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            string style = Theme.SelectedItem as string;
            var uri = new Uri("resources/Theme." + style + ".xaml", UriKind.Relative);
            if (style == "Dark") {
                imgNew.Source = new BitmapImage(new Uri("Resources/icons8-new-50-dark.png", UriKind.Relative));
                imgOpen.Source = new BitmapImage(new Uri("Resources/icons8-open-50-dark.png", UriKind.Relative));
                imgSave.Source = new BitmapImage(new Uri("Resources/icons8-save-50-dark.png", UriKind.Relative));
                imgBold.Source = new BitmapImage(new Uri("Resources/icons8-bold-50-dark.png", UriKind.Relative));
                imgItalic.Source = new BitmapImage(new Uri("Resources/icons8-italic-50-dark.png", UriKind.Relative));
                imgUnderline.Source = new BitmapImage(new Uri("Resources/icons8-underline-50-dark.png", UriKind.Relative));
                imgUndo.Source = new BitmapImage(new Uri("Resources/icons8-undo-50-dark.png", UriKind.Relative));
                imgRedo.Source = new BitmapImage(new Uri("Resources/icons8-redo-50-dark.png", UriKind.Relative));
            } else {
                imgNew.Source = new BitmapImage(new Uri("Resources/icons8-new-50.png", UriKind.Relative));
                imgOpen.Source = new BitmapImage(new Uri("Resources/icons8-open-50.png", UriKind.Relative));
                imgSave.Source = new BitmapImage(new Uri("Resources/icons8-save-50.png", UriKind.Relative));
                imgBold.Source = new BitmapImage(new Uri("Resources/icons8-bold-50.png", UriKind.Relative));
                imgItalic.Source = new BitmapImage(new Uri("Resources/icons8-italic-50.png", UriKind.Relative));
                imgUnderline.Source = new BitmapImage(new Uri("Resources/icons8-underline-50.png", UriKind.Relative));
                imgUndo.Source = new BitmapImage(new Uri("Resources/icons8-undo-50.png", UriKind.Relative));
                imgRedo.Source = new BitmapImage(new Uri("Resources/icons8-redo-50.png", UriKind.Relative));
            }
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}
