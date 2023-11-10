using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;

namespace MobileApp.Controls;

public partial class MediaShareMenuItem : ContentView
{

    public static readonly BindableProperty TextProperty =
            BindableProperty.CreateAttached("Text", typeof(string), typeof(MediaShareMenuItem), null, BindingMode.TwoWay, null);

    public string Text
    {
        get
        {
            return (string)GetValue(TextProperty);
        }
        set
        {
            SetValue(TextProperty, value);
            HasText = !String.IsNullOrEmpty(value);
        }
    }
    public static readonly BindableProperty HasTextProperty =
            BindableProperty.CreateAttached("HasText", typeof(bool), typeof(MediaShareMenuItem), false, BindingMode.OneWay, null);

    public bool HasText
    {
        get
        {
            return (bool)GetValue(HasTextProperty);
        }
        private set
        {
            SetValue(HasTextProperty, value);
        }
    }


    public static readonly BindableProperty IconSvgProperty =
            BindableProperty.CreateAttached("IconSvg", typeof(PathGeometry), typeof(MediaShareMenuItem), null, BindingMode.TwoWay, null, OnIconSvgPropertyChanged);

    public PathGeometry IconSvg
    {
        get
        {
            return (PathGeometry)GetValue(IconSvgProperty); 
        }
        set
        {
            SetValue(IconSvgProperty, value);   
        }
    }

    private static void OnIconSvgPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
    }

    public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create("TapCommand", typeof(ICommand), typeof(MediaShareMenuItem), null, BindingMode.TwoWay, null, OnTapCommandPropertyChanged);


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
            BindableProperty.Create("TapCommandParameters", typeof(object), typeof(MediaShareMenuItem), null, BindingMode.TwoWay, null, OnTapCommandParametersPropertyChanged);


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

    public MediaShareMenuItem()
	{
		InitializeComponent();
	}


}