using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Lab13.Utils {
    class Logger {

        public static Run Log(string text) {
            FlowDocument document = MainWindow.log;
            if (document != null) {
                Run r = new Run(text);
                Paragraph p = new Paragraph(r);
                document.Blocks.Add(p);
                ((document.Parent as FlowDocumentScrollViewer).Parent as ScrollViewer).ScrollToEnd();
                return r;
            }
            return null;
        }

        public static Run Log(string text, string foreground) {
            Run r = Log(text);
            if (r != null) {
                r.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(foreground));
            }
            return r;
        }
    }
}
