using MobileApp.Controls;
using MobileApp.Service;
using MobileApp.ViewModel;

namespace MobileApp.View;

public partial class ResetPasswordContentPage : CustomContentPage
{
	public ResetPasswordContentPage(ResetPasswordContentPageViewModel resetPasswordContentPageViewModel)
    {
		InitializeComponent();
		this.BindingContext = resetPasswordContentPageViewModel;
	}

    private void Code1_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(e.NewTextValue.Length >= 1) 
        { 
            Code2.Focus();
        }
        else if(e.NewTextValue.Length ==0)
        {

        }
    }
    private void Code2_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (e.NewTextValue.Length >= 1)
        {
            Code3.Focus();
        }
        else if (e.NewTextValue.Length == 0)
        {

            Code1.Focus();
        }
    }
    private void Code3_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (e.NewTextValue.Length >= 1)
        {
            Code4.Focus();
        }
        else if (e.NewTextValue.Length == 0)
        {

            Code2.Focus();
        }
    }
    private void Code4_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (e.NewTextValue.Length >= 1)
        {
            Code5.Focus();
        }
        else if (e.NewTextValue.Length == 0)
        {

            Code3.Focus();
        }
    }
    private void Code5_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (e.NewTextValue.Length >= 1)
        {
            Code6.Focus();
        }
        else if (e.NewTextValue.Length == 0)
        {

            Code4.Focus();
        }
    }
    private void Code6_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (e.NewTextValue.Length >= 1)
        {
        }
        else if (e.NewTextValue.Length == 0)
        {

            Code5.Focus();
        }
    }

}