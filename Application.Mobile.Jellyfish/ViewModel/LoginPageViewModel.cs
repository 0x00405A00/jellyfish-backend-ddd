using MobileApp.Controls;
using MobileApp.Handler.AppConfig;
using MobileApp.Handler.Backend.Communication.WebApi;
using MobileApp.Service;
using MobileApp.Validation;
using MobileApp.View;
using System.Windows.Input;

namespace MobileApp.ViewModel
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly ResetPasswordContentPageViewModel _resetPasswordContentPageViewModel;
        private readonly RegisterContentPageViewModel _registerContentPageViewModel;
        private readonly JellyfishWebApiRestClient _webApiRestClient;
        private readonly ApplicationConfigHandler _applicationConfigHandler;
        private readonly NavigationService _navigationService;
        private readonly MainPageViewModel _mainPageViewModel;

        public ValidatableObject<string> Email { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; private set; } = new ValidatableObject<string>();
        public bool IsLoading
        {
            get; private set;   
        }
        public bool ShowPassword
        {
            get; private set;   
        }
        private bool _rememberMe=false;
        public bool RememberMe
        {
            get
            {
                return _rememberMe;
            }
            set
            {
                _rememberMe = value;
                OnPropertyChanged(nameof(RememberMe));
                RememberMeChanged();
            }
        }
        private CancellationTokenSource _webApiActionCancelationToken = new CancellationTokenSource();
        public ICommand OpenRegisterPageCommand { get; private set; }
        public ICommand SubmitCommand { get; private set; }
        public ICommand ValidateByValueChangeCommand { get; private set; }
        public ICommand OpenForgotPasswordPageCommand { get; private set; } 

        public LoginPageViewModel(
            ResetPasswordContentPageViewModel resetPasswordContentPageViewModel,
            RegisterContentPageViewModel registerContentPageViewModel,
            MainPageViewModel mainPageViewModel,
            NavigationService navigationService,
            ApplicationConfigHandler applicationConfigHandler,
            JellyfishWebApiRestClient webApiRestClient)
        {
            _resetPasswordContentPageViewModel = resetPasswordContentPageViewModel;
            _registerContentPageViewModel = registerContentPageViewModel;   
            _webApiRestClient = webApiRestClient;
            _applicationConfigHandler = applicationConfigHandler;   
            _navigationService = navigationService;
            _mainPageViewModel = mainPageViewModel; 
            OpenRegisterPageCommand = new RelayCommand(OpenRegisterPageAction);
            ValidateByValueChangeCommand = new RelayCommand<string>(ValidateUiVoid);
            SubmitCommand = new RelayCommand(SubmitAction);
            OpenForgotPasswordPageCommand = new RelayCommand(OpenForgotPasswordPageAction);
            AddValidations();
            _rememberMe = _applicationConfigHandler.ApplicationConfig.AccountConfig.RemeberMe;
            if(_rememberMe)
            {
                Email.Value = _applicationConfigHandler.ApplicationConfig.AccountConfig.UserName;
                Password.Value = _applicationConfigHandler.ApplicationConfig.AccountConfig.Password;
            }
            SetAccountCredentialsFromConfig();
            CheckBackendConnection();

        }
        private async void CheckBackendConnection()
        {
            bool resp = await _webApiRestClient.ConnectionTest(_webApiActionCancelationToken.Token);
            if (!resp)
            {
                _applicationConfigHandler.ApplicationConfig.NetworkConfig.SetDefaults();    
            }
        }
        private void SetAccountCredentialsFromConfig()
        {
            if (_applicationConfigHandler.ApplicationConfig.AccountConfig.RemeberMe)
            {
                if (_applicationConfigHandler.ApplicationConfig.AccountConfig.UserName != null && _applicationConfigHandler.ApplicationConfig.AccountConfig.Password != null)
                {

                    Email.Value = _applicationConfigHandler.ApplicationConfig.AccountConfig.UserName;
                    Password.Value = _applicationConfigHandler.ApplicationConfig.AccountConfig.Password;
                }
            }
        }
        private async void RememberMeChanged()
        {
            await Task.Delay(1000);
            if(RememberMe)
            {
                _applicationConfigHandler.ApplicationConfig.AccountConfig.UserName = Email.Value;
                _applicationConfigHandler.ApplicationConfig.AccountConfig.Password = Password.Value;
            }
            else
            {
                _applicationConfigHandler.ApplicationConfig.AccountConfig.UserName = null;
                _applicationConfigHandler.ApplicationConfig.AccountConfig.Password = null;
            }
            _applicationConfigHandler.ApplicationConfig.AccountConfig.RemeberMe = RememberMe;
            _applicationConfigHandler.Safe();
        }
        private void OpenForgotPasswordPageAction()
        {
            _navigationService.PushAsync(new ResetPasswordContentPage(_resetPasswordContentPageViewModel));
        }
        private async void SubmitAction()
        {
            bool validEntries = ValidateUi();
            if (!validEntries) {
                return;
            }

            IsLoading = true;
            var response = await _webApiRestClient.Authentificate(Email.Value, Password.Value, _webApiActionCancelationToken.Token);
            IsLoading = false;
            if (response == null)
            {
                NotificationHandler.ToastNotify("No internet: check your connection.");
                return;
            }


            _applicationConfigHandler.ApplicationConfig.AccountConfig.UserSession = new AuthModel(response);
            _applicationConfigHandler.Safe();
            LoggedIn();
        }
        private async void LoggedIn()
        {

            await _navigationService.PushAsync(new MainPage(_mainPageViewModel));
            _mainPageViewModel.SwipeRightAction(_mainPageViewModel.ViewTemplates);
        }
        private bool ValidateUi(int checkUiElemt = -1)
        {
            bool isValidEmail = false;
            bool isValidPassword = false;
            switch(checkUiElemt)
            {
                case 1:
                    isValidEmail = ValidateEmail();    
                    break;
                case 2:
                    isValidPassword= ValidatePassword();  
                    break;
                default:
                    isValidEmail = ValidateEmail();
                    isValidPassword = ValidatePassword();
                    break;
            }
            return isValidEmail && isValidPassword;  
        }
        private void ValidateUiVoid(string checkUiElemt)
        {
            if(int.TryParse(checkUiElemt, out int value))
            {
                ValidateUi(value);

            }

        }
        private bool ValidateEmail()
        {
            return Email.Validate();
        }

        private bool ValidatePassword()
        {
            return Password.Validate();
        }
        private void AddValidations()
        {
            Email.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Email entry is required."
            });
            Email.Validations.Add(new EmailRule { 
                ValidationMessage = "Email is invalid."
            });

            Password.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Password entry is required."
            });
        }
        private void OpenRegisterPageAction()
        {
            _navigationService.PushAsync(new RegisterContentPage(_registerContentPageViewModel));
        }
    }
}
