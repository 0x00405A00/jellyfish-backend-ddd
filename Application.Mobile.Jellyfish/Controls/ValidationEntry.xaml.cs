using Microsoft.Maui.Controls.Shapes;
using MobileApp.Validation;
using System.Windows.Input;

namespace MobileApp.Controls;

public partial class ValidationEntry : ContentView
{
    public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.CreateAttached("Placeholder", typeof(string), typeof(ValidationEntry), null, BindingMode.TwoWay, null, OnPlaceholderPropertyChanged);

    public string Placeholder
    {
        get
        {
            return (string)GetValue(PlaceholderProperty);
        }
        set
        {
            SetValue(PlaceholderProperty, value);
        }
    }

    private static void OnPlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
    }

    public static readonly BindableProperty IconSvgProperty =
            BindableProperty.CreateAttached("IconSvg", typeof(PathGeometry), typeof(ValidationEntry), null, BindingMode.TwoWay, null, OnIconSvgPropertyChanged);

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

    /*public static readonly BindableProperty ValidatableObjectProperty =
            BindableProperty.Create("ValidatableObject", typeof(object), typeof(ValidationEntry), null);

    public object ValidatableObject
    {
        get
        {
            return (object)GetValue(ValidatableObjectProperty);
        }
        set
        {
            SetValue(ValidatableObjectProperty, value);
        }
    }*/

    public static readonly BindableProperty ValidateByValueChangeCommandProperty =
            BindableProperty.Create("ValidateByValueChangeCommand", typeof(ICommand), typeof(ValidationEntry), null, BindingMode.TwoWay, null, ValidateByValueChangeCommandPropertyChanged);


    public ICommand ValidateByValueChangeCommand
    {
        get
        {
            return (ICommand)GetValue(ValidateByValueChangeCommandProperty);
        }
        set
        {
            SetValue(ValidateByValueChangeCommandProperty, value);
        }
    }

    private static void ValidateByValueChangeCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
    }


    public static readonly BindableProperty ValidateByValueChangeCommandParametersProperty =
            BindableProperty.Create("ValidateByValueChangeCommandParameters", typeof(object), typeof(ValidationEntry), null, BindingMode.TwoWay, null, ValidateByValueChangeCommandParametersChanged);


    public object ValidateByValueChangeCommandParameters
    {
        get
        {
            return (object)GetValue(ValidateByValueChangeCommandParametersProperty);
        }
        set
        {
            SetValue(ValidateByValueChangeCommandParametersProperty, value);
        }
    }
    private static void ValidateByValueChangeCommandParametersChanged(BindableObject bindable, object oldValue, object newValue)
    {
    }

    public static readonly BindableProperty ValidatableObjectProperty =
            BindableProperty.Create(nameof(ValidatableObject), typeof(object), typeof(ValidationEntry), true);

    public object ValidatableObject
    {
        get { return (object)GetValue(ValidatableObjectProperty); }
        set { SetValue(ValidatableObjectProperty, value); }
    }
    public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(ValidationEntry), false);

    public object IsPassword
    {
        get { return (object)GetValue(IsPasswordProperty); }
        set { SetValue(IsPasswordProperty, value); }
    }

    public ValidationEntry()
	{
		InitializeComponent();
	}
}