using MobileApp.Controls;
using MobileApp.Service;
using MobileApp.ViewModel;

namespace MobileApp.View;

public partial class RegisterContentPage : CustomContentPage
{
	public RegisterContentPage(RegisterContentPageViewModel registerContentPageViewModel)
	{
		InitializeComponent();
		this.BindingContext = registerContentPageViewModel;
	}

    private void ActivationCodePart1Entry_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (ActivationCodePart1Entry.Text != null && ActivationCodePart1Entry.Text.Length > 0)
        {
            ActivationCodePart2Entry.Focus();
        }
    }

    private void ActivationCodePart2Entry_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (ActivationCodePart2Entry.Text != null && ActivationCodePart2Entry.Text.Length == 0)
        {
            ActivationCodePart1Entry.Focus();
        }
        else if (ActivationCodePart2Entry.Text != null && ActivationCodePart2Entry.Text.Length > 0)
        {
            ActivationCodePart3Entry.Focus();
        }
    }

    private void ActivationCodePart3Entry_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (ActivationCodePart3Entry.Text != null && ActivationCodePart3Entry.Text.Length == 0)
        {
            ActivationCodePart2Entry.Focus();
        }
        else if (ActivationCodePart3Entry.Text != null && ActivationCodePart3Entry.Text.Length > 0)
        {
            ActivationCodePart4Entry.Focus();
        }
    }

    private void ActivationCodePart4Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(ActivationCodePart4Entry.Text!=null&& ActivationCodePart4Entry.Text.Length==0)
        {
            ActivationCodePart3Entry.Focus();
        }
        else if (ActivationCodePart4Entry.Text != null && ActivationCodePart4Entry.Text.Length > 0)
        {
            ActivateAccountButton.Focus();
        }

    }
}