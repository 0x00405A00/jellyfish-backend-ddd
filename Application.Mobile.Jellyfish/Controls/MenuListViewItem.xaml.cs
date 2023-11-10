using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;

namespace MobileApp.Controls;

public partial class MenuListViewItem : ContentView
{

    public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create("TextColor", typeof(Color), typeof(MenuListViewItem), Color.FromRgb(0, 0, 0), BindingMode.TwoWay, null);


    public Color TextColor
    {
        get
        {
            return (Color)GetValue(TextColorProperty);
        }
        set
        {
            SetValue(TextColorProperty, value);
        }
    }
    public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(MenuListViewItem), null, BindingMode.TwoWay, null, OnTextPropertyChanged);


    public string Text
    {
        get
        {
            return (string)GetValue(TextProperty);
        }
        set
        {
            SetValue(TextProperty, value);
        }
    }



    private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
    }
    public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create("TapCommand", typeof(ICommand), typeof(MenuListViewItem), null, BindingMode.TwoWay, null, OnTapCommandPropertyChanged);


    public ICommand TapCommand
    {
        get
        {
            return (ICommand)GetValue(TapCommandProperty);
        }
        set
        {
            SetValue(TapCommandProperty, value);
        }
    }



    private static void OnTapCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
    }


    public static readonly BindableProperty TapCommandParametersProperty =
            BindableProperty.Create("TapCommandParameters", typeof(object), typeof(MenuListViewItem), null, BindingMode.TwoWay, null, OnTapCommandParametersPropertyChanged);


    public object TapCommandParameters
    {
        get
        {
            return (ICommand)GetValue(TapCommandParametersProperty);
        }
        set
        {
            SetValue(TapCommandParametersProperty, value);
        }
    }



    private static void OnTapCommandParametersPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
    }
    public MenuListViewItem()
	{
		InitializeComponent();
	}
}