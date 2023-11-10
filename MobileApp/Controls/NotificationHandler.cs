using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Controls
{
    public static class NotificationHandler

    {
        public static async void ToastNotify(string message, double fontSize = 14.0, ToastDuration toastDuration = ToastDuration.Short)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            string text = message;
            var toast = Toast.Make(text, toastDuration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        public static async Task<bool> DisplayAlert(string title, string message,string accept, string cancel)
        {
            return await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(title, message,accept, cancel);
        }
        public static async Task<string> DisplayPrompt(string title, string message, string accept = "Ok", string cancel = "Cancel", string placeHolder = null, int maxLen = -1, Keyboard keyboard = null, string initValue = "")
        {
            return await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel, placeHolder, maxLen, keyboard, initValue);
        }

    }
}
