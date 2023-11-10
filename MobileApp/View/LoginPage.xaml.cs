using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;
using MobileApp.Controls;
using MobileApp.Service;
using MobileApp.ViewModel;
using SkiaSharp.Extended.UI.Controls;
using System;


namespace MobileApp.View;

public partial class LoginPage : CustomContentPage
{
	public LoginPage(LoginPageViewModel loginPageViewModel) 
    {
	    InitializeComponent();
		this.BindingContext= loginPageViewModel;	
	}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        /*double maxScale =1.0;
        var displayWidth = DeviceDisplay.Current.MainDisplayInfo.Width;
        var displayHeight = DeviceDisplay.Current.MainDisplayInfo.Height;

        ValueTuple<double,double> curBoxPos = new ValueTuple<double, double>(LoginBox.X, LoginBox.Y);
        var content = LoginBox.Content;
        LoginBox.WidthRequest = 300;
        LoginBox.HeightRequest = 300;
        LoginBox.Opacity = 0.0;
        LoginBox.Content = null;
        LoginBox.IsVisible = true;
        await LoginBox.FadeTo(1.0, 250);
        LoginBox.ScaleX = 0.3;
        LoginBox.ScaleY = 0.3;
        for(int i = 1; i <= 6;i++)
        {
            if(i%2==0)
            {
                await LoginBox.TranslateTo(curBoxPos.Item1, curBoxPos.Item2+100, 350, Easing.SpringIn);
            }
            else
            {

                await LoginBox.TranslateTo(curBoxPos.Item1, curBoxPos.Item2 - 100, 350, Easing.SpringIn);
            }
        }
        var t1 = LoginBox.ScaleXTo(maxScale, 500, Easing.Linear);
        var t2 = LoginBox.ScaleYTo(maxScale, 500, Easing.Linear);
        await Task.WhenAll(t1, t2);
        await LoginBox.TranslateTo(curBoxPos.Item1, curBoxPos.Item2, 1, Easing.SpringIn);

        LoginBox.WidthRequest = double.NaN;
        LoginBox.HeightRequest = double.NaN;

        content.Opacity = 0.0;
        LoginBox.Content = content;
        await LoginBox.Content.FadeTo(1.0,500,Easing.Linear); */
    }

    private async void LoginBox_SwipedUp(object sender, SwipedEventArgs e)
    {
        await LoginBox.RotateYTo(360, 500);
        LoginBox.Rotation = 0;
        SignInButton.Command.Execute(SignInButton.CommandParameter);
    }

    private void SKLottieView_AnimationFailed(object sender, EventArgs e)
    {

    }

    private void SKLottieView_AnimationLoaded(object sender, EventArgs e)
    {

    }
}