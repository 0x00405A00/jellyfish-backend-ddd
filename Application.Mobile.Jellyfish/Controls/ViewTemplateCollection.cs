using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp.Controls
{
    public class ViewTemplateCollection : CollectionView
    {
        public static readonly BindableProperty BindingContextChangedCommandProperty =
            BindableProperty.CreateAttached("BindingContextChangedCommand", typeof(ICommand), typeof(ViewTemplateCollection), null, BindingMode.TwoWay, null);

        public ICommand BindingContextChangedCommand
        {
            get
            {
                return (ICommand)GetValue(BindingContextChangedCommandProperty);
            }
            set
            {
                SetValue(BindingContextChangedCommandProperty, value);
            }
        }
        public static readonly BindableProperty BindingContextChangedCommandParameterProperty =
            BindableProperty.CreateAttached("BindingContextChangedCommandParameter", typeof(object), typeof(ViewTemplateCollection), null, BindingMode.TwoWay, null);

        public object BindingContextChangedCommandParameter
        {
            get
            {
                return (object)GetValue(BindingContextChangedCommandParameterProperty);
            }
            set
            {
                SetValue(BindingContextChangedCommandParameterProperty, value);
            }
        }
        protected override void OnBindingContextChanged()
        {

            if (BindingContextChangedCommand != null)
            {
                var items = ItemsSource;
                BindingContextChangedCommand.Execute(BindingContextChangedCommandParameter);
            }
            base.OnBindingContextChanged();
        }
        public ViewTemplateCollection()
        {
        }
    }
}
