using MobileApp.Controls;
using MobileApp.Service;
using MobileApp.ViewModel;

namespace MobileApp
{
    public partial class MainPage : CustomContentPage
    {


        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            this.BindingContext = mainPageViewModel;
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {

        }

    }
}