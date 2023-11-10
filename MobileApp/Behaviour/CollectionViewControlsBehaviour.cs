using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;
using MobileApp.Model;

/* Nicht gemergte Änderung aus Projekt "MobileApp (net7.0-ios)"
Vor:
using System.Collections.ObjectModel;
Nach:
using System.Collections.ObjectModel;
using MobileApp;
using MobileApp.ControlExtension;
*/

/* Nicht gemergte Änderung aus Projekt "MobileApp (net7.0-ios)"
Vor:
using System.Collections.ObjectModel;
Nach:
using System.Collections.ObjectModel;
using MobileApp;
using MobileApp.ControlExtension;
using MobileApp.Behaviour;
*/
using System.Collections.ObjectModel;
using MobileApp.ControlExtension;

namespace MobileApp.Behaviour
{
    public static class CollectionViewControlsBehaviour
    {
        public static readonly BindableProperty ScrollToEndProperty =
            BindableProperty.CreateAttached("ScrollToEnd", typeof(bool), typeof(CollectionViewControlsBehaviour), false, BindingMode.TwoWay, null, OnScrollToEndPropertyChanged);

        public static bool GetScrollToEnd(BindableObject obj)
        {
            return (bool)obj.GetValue(ScrollToEndProperty);
        }

        public static void SetScrollToEnd(BindableObject obj, bool value)
        {
            obj.SetValue(ScrollToEndProperty, value);
        }

        private static void OnScrollToEndPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var sv = bindable as CollectionView;
            if ((bool)newValue)
            {

                sv.ScrollToEnd(typeof(MessageGroup));
            }
        }
    }
}
