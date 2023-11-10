using MobileApp.Controls;
using MobileApp.Service;
using MobileApp.ViewModel;

namespace MobileApp.View;

public partial class UserSelectionPage : CustomContentPage
{
	public UserSelectionPage(UserSelectionPageViewModel contactsPageViewModel) 
    {
		InitializeComponent();
		this.BindingContext = contactsPageViewModel;	
	}
}