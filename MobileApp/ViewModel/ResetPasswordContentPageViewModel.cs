using MobileApp.Controls;
using MobileApp.Handler.Backend.Communication.WebApi;
using MobileApp.Service;
using MobileApp.Validation;
using Shared.DataTransferObject;
using System.Windows.Input;

namespace MobileApp.ViewModel
{
    public class ResetPasswordContentPageViewModel : BaseViewModel
    {
        private readonly CancellationTokenSource _webApiActionCancelationToken = new CancellationTokenSource();
        public ValidatableObject<string> Phone { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Email { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> SecureCodeEntry1 { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> SecureCodeEntry2 { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> SecureCodeEntry3 { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> SecureCodeEntry4 { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> SecureCodeEntry5 { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> SecureCodeEntry6 { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> PasswordRepeat { get; private set; } = new ValidatableObject<string>();
        /// <summary>
        /// Indicates the visibile state of the activity indicator 
        /// </summary>
        public bool IsLoading
        {
            get; private set;
        }
        private bool _isRequestSent = false;
        /// <summary>
        /// The UI state for requesting a password reset (First action)
        /// </summary>
        public bool IsRequestSent
        {
            get { return _isRequestSent; }
            set
            {
                _isRequestSent = value;
                OnPropertyChanged();
            }
        }
        private bool _isCodeSentToBackendForVerify = false;
        /// <summary>
        /// The UI state for send state of the secury code that previously sent to backend
        /// </summary>
        public bool IsCodeSentToBackendForVerify
        {
            get { return _isCodeSentToBackendForVerify; }
            set
            {
                _isCodeSentToBackendForVerify = value;
                OnPropertyChanged();
            }
        }
        private bool _showPassword = false;
        public bool ShowPassword
        {
            get { return _showPassword; }
            set
            {
                _showPassword = value;
                OnPropertyChanged();
            }
        }
        private bool _blockBackSwitch;
        public bool BlockBackSwitch
        {
            get { return _blockBackSwitch; }
            set
            {
                _blockBackSwitch = value;
                OnPropertyChanged();
            }
        }
        public bool IsSecureCodeValid
        {
            get
            {
                return SecureCodeEntry1.IsValid && SecureCodeEntry2.IsValid && SecureCodeEntry3.IsValid
                && SecureCodeEntry4.IsValid && SecureCodeEntry5.IsValid && SecureCodeEntry6.IsValid;
            }
        }
        public string SecureCodeConcat
        {
            get
            {
                string tmp = string.Format("{0}{1}{2}{3}{4}{5}", SecureCodeEntry1.Value , SecureCodeEntry2.Value , SecureCodeEntry3.Value
                ,SecureCodeEntry4.Value , SecureCodeEntry5.Value , SecureCodeEntry6.Value);
                return (tmp);
            }
        }
        public ICommand BackButtonCommand { get; private set; }
        public ICommand SubmitCodeActionCommand { get; private set; }
        public ICommand SubmitActionCommand { get; private set; }
        public ICommand SubmitPasswordActionCommand { get; private set; }
        public ICommand ValidateByValueChangeCommand { get; private set; }
        private readonly NavigationService _navigationService;
        private readonly JellyfishWebApiRestClient _jellyfishWebApiRestClient;

        public ResetPasswordContentPageViewModel(JellyfishWebApiRestClient jellyfishWebApiRestClient,NavigationService navigationService)
        {
            _navigationService = navigationService;
            _jellyfishWebApiRestClient = jellyfishWebApiRestClient;
            SubmitActionCommand = new RelayCommand(SubmitAction);//1. Action
            SubmitCodeActionCommand = new RelayCommand(SubmitCodeAction);//2. Action
            SubmitPasswordActionCommand = new RelayCommand(SubmitPasswordAction);//3. Action
            ValidateByValueChangeCommand = new RelayCommand<string>(ValidateUiVoid);//Validate Validation Entries by TextChange events
            BackButtonCommand = new RelayCommand(BackButtonAction);
            AddValidations();
        }
        public async void BackButtonAction()
        {
            if(IsCodeSentToBackendForVerify)
            {
                IsCodeSentToBackendForVerify = false;
                SecureCodeEntry1.Value = String.Empty;
                SecureCodeEntry2.Value = String.Empty;
                SecureCodeEntry3.Value = String.Empty;
                SecureCodeEntry4.Value = String.Empty;
                SecureCodeEntry5.Value = String.Empty;
                SecureCodeEntry6.Value = String.Empty;
                BlockBackSwitch = true;
            }
            else if(IsRequestSent)
            {
                IsRequestSent = false;

            }
            else
            {
                BlockBackSwitch = false;
            }
            
        }
        /// <summary>
        /// (1.) First Password reset action, request for password reset. Security code is send to users email 
        /// </summary>
        public async void SubmitAction()
        {
            IsLoading = true;
            bool validEntries = ValidateUi();
            if (validEntries)
            {
                BlockBackSwitch = true;
                var response = await _jellyfishWebApiRestClient.ResetPasswordRequest(new PasswordResetRequestDataTransferModel { EMail = this.Email.Value }, _webApiActionCancelationToken.Token);
                if(!response.IsSuccess)
                {
                    NotificationHandler.ToastNotify("Error: The request is not okay.");
                }
                else
                {
                    IsRequestSent = true;

                }

            }
            IsLoading = false;
        }
        /// <summary>
        /// (2.) Second Action is the entering of security code that is previously sent to users email
        /// </summary>
        public async void SubmitCodeAction()
        {
            IsLoading = true;
            if (IsSecureCodeValid)
            {
                //send to backend and processing the result
                var response = await _jellyfishWebApiRestClient.ResetPasswordConfirmation(new PasswordResetConfirmationCodeDataTransferModel { PasswordResetCode = this.SecureCodeConcat }, _webApiActionCancelationToken.Token);
                if(!response.IsSuccess)
                {

                    NotificationHandler.ToastNotify("Error: The request is not okay.");
                }
                else
                {
                    IsCodeSentToBackendForVerify = true;
                }
                //when codes invalid show common errors in ui
            }
            IsLoading = false;
        }
        /// <summary>
        /// (3.) Third and last action is the setting up of a new password
        /// </summary>
        public async void SubmitPasswordAction()
        {
            IsLoading = true;
            bool validPasswordEntries = Password.IsValid && PasswordRepeat.IsValid;
            if (validPasswordEntries)
            {
                //send to backend and processing the result
                var response = await _jellyfishWebApiRestClient.ResetPassword(new PasswordResetDataTransferModel { Password = Password.Value, PasswordResetCode = SecureCodeConcat },_webApiActionCancelationToken.Token);
                if(!response.IsSuccess)
                {
                    NotificationHandler.ToastNotify("Error: The request is not okay.");
                }
                else
                {
                    _navigationService.CloseCurrentPage();
                }
            }
            IsLoading = false;
        }
        private bool ValidateUi(int checkUiElemt = -1)
        {
            bool isValidEmail = false;
            bool isValidPhone = false;
            bool isValidPassword = false;
            bool isValidPasswordRepeat = false;
            switch (checkUiElemt)
            {
                case 1:
                    isValidEmail = ValidateEmail();
                    break;
                case 2:
                    isValidPhone = ValidatePhone();
                    break;
                case 3:
                    isValidPassword = ValidatePassword();
                    break;
                case 4:
                    isValidPasswordRepeat = ValidatePasswordRepeat();
                    break;
                default:
                    isValidEmail = ValidateEmail();
                    isValidPhone = ValidatePhone();
                    break;
            }
            return (isValidEmail || isValidPhone) || (isValidPassword&& isValidPasswordRepeat);
        }
        private void ValidateUiVoid(string checkUiElemt)
        {
            if (int.TryParse(checkUiElemt, out int value))
            {
                ValidateUi(value);

            }

        }
        private bool ValidatePassword()
        {
            return Password.Validate();
        }
        private bool ValidatePasswordRepeat()
        {
            return PasswordRepeat.Validate();
        }
        private bool ValidateEmail()
        {
            return Email.Validate();
        }

        private bool ValidatePhone()
        {
            return Phone.Validate();
        }
        private void AddValidations()
        {
            Email.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Email entry is required."
            });
            Email.Validations.Add(new EmailRule
            {
                ValidationMessage = "Email is invalid."
            });

            Phone.Validations.Add(new PhoneNumberRule
            {
                ValidationMessage = "Phone entry is not valid."
            });
            Phone.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Phone entry is required."
            });

            SecureCodeEntry1.Validations.Add(new StringIsNumerRule
            {
                ValidationMessage = "Code in Block 1 is NaN."
            });
            SecureCodeEntry2.Validations.Add(new StringIsNumerRule
            {
                ValidationMessage = "Code in Block 2 is NaN."
            });
            SecureCodeEntry3.Validations.Add(new StringIsNumerRule
            {
                ValidationMessage = "Code in Block 3 is NaN."
            });
            SecureCodeEntry4.Validations.Add(new StringIsNumerRule
            {
                ValidationMessage = "Code in Block 4 is NaN."
            });
            SecureCodeEntry5.Validations.Add(new StringIsNumerRule
            {
                ValidationMessage = "Code in Block 5 is NaN."
            });
            SecureCodeEntry6.Validations.Add(new StringIsNumerRule
            {
                ValidationMessage = "Code in Block 6 is NaN."
            });

            Password.Validations.Add(new IsNotNullOrEmptyRule
            {
                ValidationMessage = "Password entry is required."
            });
            Password.Validations.Add(new ValueMustBeEqualRule<string>(PasswordRepeat)
            {
                ValidationMessage = "Password entry is not equal than Password repeat."
            });
            PasswordRepeat.Validations.Add(new IsNotNullOrEmptyRule()
            {
                ValidationMessage = "Password entry is required."
            });
            PasswordRepeat.Validations.Add(new ValueMustBeEqualRule<string>(Password)
            {
                ValidationMessage = "Password repeat entry is not equal than Password."
            });

        }
    }
}
