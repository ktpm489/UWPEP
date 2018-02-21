using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualExplorer.Framework.Foundation {

    public static class ObservableExtension {

        public static ObservableCollection<T> ToObserCollection<T>(this IEnumerable<T> _LinqResult) {
            return new ObservableCollection<T>(_LinqResult);
        }


        public static int FirstIndex<T>(this ObservableCollection<T> list, Func<T, bool> predicate) {
            int firstIndex = 0;
            var firstObject = list.First(predicate);
            if (firstObject != null) {
                firstIndex = list.IndexOf(firstObject);
            }

            return firstIndex;
        }
    }

}
