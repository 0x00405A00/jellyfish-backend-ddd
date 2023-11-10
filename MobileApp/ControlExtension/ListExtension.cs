using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.ControlExtension
{
    public static class ListExtension
    {

        public static ObservableCollection<T> ToObservableCollection<T>(this IList<T> values)
        {
            var list = new ObservableCollection<T>();

            foreach (var item in values)
            {
                list.Add(item);
            }

            return list;
        }
        public static int IndexOf<T>(this IList<T> list, Predicate<T> predicate)
        {
            if (list == null) throw new ArgumentNullException("list");

            for (int i = 0; i < list.Count; i++)
            {

                if (predicate(list[i])) return i;
            }

            return -1;
        }
        public static int IndexOf<T>(this Collection<T> list, Predicate<T> predicate)
        {
            if (list == null) throw new ArgumentNullException("list");

            for (int i = 0; i < list.Count; i++)
            {

                if (predicate(list[i])) return i;
            }

            return -1;
        }
        public static void ScrollToEnd(this CollectionView collectionView, Type groupItemType = null)
        {
            var sv = collectionView;
            if (sv == null) return;
            bool isLoaded = sv.IsLoaded;
            bool isVisbile = sv.IsVisible;
            bool isFocued = sv.IsFocused;

            if (sv.ItemsSource == null)
                return;
            var data = sv.ItemsSource.Cast<object>().ToList();
            int count = data.Count;
            if (count > 0 && groupItemType != null)
            {
                if (sv.IsGrouped)
                {
                    var subData = data.LastOrDefault();
                    if (subData != null)
                    {
                        bool isMsgGrp = subData.GetType() == groupItemType;
                        if (isMsgGrp)
                        {
                            var d = ((IEnumerable)subData).Cast<object>().ToList();
                            if (d != null && d.Count > 0)
                            {
                                sv.ScrollTo(d.Last(), data.Last(), ScrollToPosition.End, false);
                            }
                        }
                    }
                }
                else
                {

                    sv.ScrollTo(count - 1, -1, ScrollToPosition.End, false);
                }
            }
        }
    }
}
