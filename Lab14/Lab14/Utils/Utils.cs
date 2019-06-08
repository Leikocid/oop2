using Lab14.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Lab14.Utils {

    public class Util {

        /// <summary>
        /// Взщавращает описание объекта. 
        /// Использует адаптор ShapeToIDescriprionAdapter
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetDescription(object obj) {
            if (obj == null) return "NULL";

            if (obj is Shape) {
                return new ShapeToIDescriprionAdapter(obj as Shape).GetDescription();
            } else {
                return $"<< неизвестный элемент {obj.ToString()}>>";
            }
        }
    }
}
