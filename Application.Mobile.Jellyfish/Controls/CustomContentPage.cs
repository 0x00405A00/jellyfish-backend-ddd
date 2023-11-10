using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Messaging;
using MobileApp.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp.Controls
{
    public class CustomContentPage : ContentPage
    {
        private readonly NavigationService Navigation;

        public static readonly BindableProperty LoadCommandProperty =
            BindableProperty.CreateAttached("LoadCommand", typeof(ICommand), typeof(CustomContentPage), null, BindingMode.TwoWay, null);

        public ICommand LoadCommand
        {
            get
            {
                return (ICommand)GetValue(LoadCommandProperty);
            }
            set
            {
                SetValue(LoadCommandProperty, value);
            }
        }
        public static readonly BindableProperty LoadCommandParameterProperty =
            BindableProperty.CreateAttached("LoadCommandParameter", typeof(object), typeof(CustomContentPage), null, BindingMode.TwoWay, null);

        public object LoadCommandParameter
        {
            get
            {
                return (object)GetValue(LoadCommandParameterProperty);
            }
            set
            {
                SetValue(LoadCommandParameterProperty, value);
            }
        }
        public static readonly BindableProperty BackButtonCommandProperty =
            BindableProperty.CreateAttached("BackButtonCommand", typeof(ICommand), typeof(CustomContentPage), null, BindingMode.TwoWay, null);

        public ICommand BackButtonCommand
        {
            get
            {
                return (ICommand)GetValue(BackButtonCommandProperty);
            }
            set
            {
                SetValue(BackButtonCommandProperty, value);
            }
        }
        public static readonly BindableProperty BackButtonCommandParameterProperty =
            BindableProperty.CreateAttached("BackButtonCommandParameter", typeof(object), typeof(CustomContentPage), null, BindingMode.TwoWay, null);

        public object BackButtonCommandParameter
        {
            get
            {
                return (object)GetValue(BackButtonCommandParameterProperty);
            }
            set
            {
                SetValue(BackButtonCommandParameterProperty, value);
            }
        }

        public static readonly BindableProperty BlockBackButtonActionProperty =
            BindableProperty.CreateAttached("BlockBackButtonAction", typeof(bool), typeof(CustomContentPage), false, BindingMode.TwoWay, null, OnBlockBackButtonActionPropertyChanged);

        public bool BlockBackButtonAction
        {
            get
            {
                return (bool)GetValue(BlockBackButtonActionProperty);
            }
            set
            {
                SetValue(BlockBackButtonActionProperty, value);
            }
        }

        private static void OnBlockBackButtonActionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        protected override bool OnBackButtonPressed()
        {
            WeakReferenceMessenger.Default.Send(new MessageBus.MessageModel("OnBackButtonPressed", new object[] {true }));

            if (BackButtonCommand != null)
            {
                BackButtonCommand.Execute(BackButtonCommandParameter);
            }
            if (!BlockBackButtonAction)
            {

            }
            else
            {

            }
            return BlockBackButtonAction;
        }
        public CustomContentPage()
        {
            if(LoadCommand != null)
            {
                LoadCommand.Execute(LoadCommandParameter);
            }
        }

    }
}
