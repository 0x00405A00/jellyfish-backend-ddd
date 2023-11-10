using CommunityToolkit.Mvvm.Input;
using MobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp.Validation
{
    public class ValidatableObject : BaseViewModel
    {
        protected object _value;
        protected object _initialValue = null;
        protected IEnumerable<string> _errors = Enumerable.Empty<string>();
        protected bool _isValid = true;
        public ICommand ValidateByValueChangeCommand { get; private set; }
        public delegate void ChangedData(ValidatableObject validatableObject,object oldValue, object newValue);
        public ChangedData ChangedDataHandler { get; set; }
        public IEnumerable<string> Errors
        {
            get => _errors;
            protected set
            {
                _errors = value;
            }
        }
        public virtual object Value
        {
            get
            {
                return _value;
            }
            set
            {
                SetValue(ref _value, value);
            }
        }
        public bool IsValueChanged
        {
            get
            {
                return _initialValue != null && _initialValue != this.Value;
            }
        }

        public bool IsValid
        {
            get => _isValid;
            protected set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }
        public virtual bool Validate()
        {
            IsValid = !Errors.Any();

            return IsValid;
        }
        protected void SetValue<T>(ref T targetVal,T newValue)
        {

            if (_initialValue == null)
            {
                _initialValue = targetVal;
            }
            object oldVal = targetVal;
            targetVal = newValue;

            if (newValue != null && oldVal != null)
            {

                if (ChangedDataHandler != null && IsValueChanged && newValue.ToString() != oldVal.ToString())
                {
                    ChangedDataHandler(this,oldVal, targetVal);
                }

            }

            OnPropertyChanged(nameof(Value));
        }
        public ValidatableObject()
        {
            ValidateByValueChangeCommand = new MobileApp.ViewModel.RelayCommand(ValidateByValueChangeAction);
        }
        public void ValidateByValueChangeAction()
        {
            Validate();
        }

    }
    public class ValidatableObject<T> : ValidatableObject
    {
        private new T _value;
        private new T _initialValue;
        public List<IValidationRule<T>> Validations { get; } = new();

        public new T Value
        {
            get
            {
                return _value;
            }
            set
            {
                SetValue(ref _value, value);
                if (_value != null)
                {
                    Validate();
                }
            }
        }
        public ValidatableObject() : base()
        {

        }
        public override bool Validate()
        {
            Errors = Validations
                ?.Where(v => !v.Check(Value))
                ?.Select(v => v.ValidationMessage)
                ?.ToArray()
                ?? Enumerable.Empty<string>();
            IsValid = !Errors.Any();
            OnPropertyChanged(nameof(Errors));
            OnPropertyChanged(nameof(IsValid));
            return IsValid;
        }
    }
    public class ValidatableObject<T,T2> : ValidatableObject<T>
    {
        public T2 ValueConverted
        {
            get
            {
                try
                {

                    var converter = TypeDescriptor.GetConverter(Value.GetType());
                    var convertedValue = converter.ConvertTo(Value,typeof(T2));
                    return (T2)(convertedValue is T2?convertedValue:null);
                }
                catch(Exception ex)
                {

                }
                return default;
            }
        }
        public ValidatableObject() : base() 
        {

        }
    }
}
