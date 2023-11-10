using MobileApp.Controls;
using MobileApp.Service;
using MobileApp.ViewModel;

namespace MobileApp.View;

public partial class SettingsPage : CustomContentPage
{
	public SettingsPage(SettingsPageViewModel settingsPageViewModel)
    {
		InitializeComponent();
		this.BindingContext = settingsPageViewModel;	
	}
}